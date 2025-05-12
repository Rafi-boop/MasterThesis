using System;

public class EdDsaSignatureScheme : ISignatureScheme<EdDsaPublicKey, EdDsaPrivateKey>
{
    public string Name => "EdDSA";

    static EdDsaSignatureScheme()
    {
        if (SodiumInterop.sodium_init() < 0)
            throw new InvalidOperationException("libsodium initialization failed.");
    }

    public (EdDsaPublicKey, EdDsaPrivateKey) GenerateKeys()
    {
        var pk = new byte[SodiumInterop.PublicKeyBytes];
        var sk = new byte[SodiumInterop.SecretKeyBytes];
        SodiumInterop.crypto_sign_keypair(pk, sk);

        return (new EdDsaPublicKey(pk), new EdDsaPrivateKey(sk));
    }

    public byte[] Sign(byte[] message, EdDsaPrivateKey privateKey)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        var signature = new byte[SodiumInterop.SignatureBytes];
        SodiumInterop.crypto_sign_detached(signature, out long sigLen, message, message.LongLength, privateKey.Export());

        if (sigLen != SodiumInterop.SignatureBytes)
            throw new Exception("Unexpected signature length.");

        return signature;
    }

    public bool Verify(byte[] message, byte[] signature, EdDsaPublicKey publicKey)
    {
        if (message == null || signature == null || publicKey == null)
            return false;

        if (signature.Length != SodiumInterop.SignatureBytes)
            return false;

        return SodiumInterop.crypto_sign_verify_detached(signature, message, message.LongLength, publicKey.Export()) == 0;
    }
}
