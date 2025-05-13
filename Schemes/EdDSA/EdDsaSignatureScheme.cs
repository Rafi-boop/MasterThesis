using System;

public class EdDsaSignatureScheme : ISignatureScheme<EdDsaPublicKey, EdDsaPrivateKey>
{
    public string Name => "EdDSA";

    static EdDsaSignatureScheme()
    {
        if (Interop.Libsodium.sodium_init() < 0)
            throw new InvalidOperationException("libsodium initialization failed.");
    }

    public (EdDsaPublicKey, EdDsaPrivateKey) GenerateKeys()
    {
        var pk = new byte[Interop.Libsodium.CRYPTO_SIGN_PUBLICKEYBYTES];
        var sk = new byte[Interop.Libsodium.CRYPTO_SIGN_SECRETKEYBYTES];
        if (Interop.Libsodium.crypto_sign_ed25519_keypair(pk, sk) != 0)
            throw new Exception("Keypair generation failed.");

        return (new EdDsaPublicKey(pk), new EdDsaPrivateKey(sk));
    }

    public byte[] Sign(byte[] message, EdDsaPrivateKey privateKey)
    {
        if (message == null) throw new ArgumentNullException(nameof(message));
        if (privateKey == null) throw new ArgumentNullException(nameof(privateKey));

        var signature = new byte[Interop.Libsodium.CRYPTO_SIGN_BYTES];
        ulong sigLen = 0;

        int result = Interop.Libsodium.crypto_sign_ed25519_detached(
            signature, ref sigLen,
            message, (ulong)message.Length,
            privateKey.Export()
        );

        if (result != 0 || sigLen != Interop.Libsodium.CRYPTO_SIGN_BYTES)
            throw new Exception("Signing failed or returned invalid signature length.");

        return signature;
    }

    public bool Verify(byte[] message, byte[] signature, EdDsaPublicKey publicKey)
    {
        if (message == null || signature == null || publicKey == null)
            return false;

        if (signature.Length != Interop.Libsodium.CRYPTO_SIGN_BYTES)
            return false;

        int result = Interop.Libsodium.crypto_sign_ed25519_verify_detached(
            signature,
            message, (ulong)message.Length,
            publicKey.Export()
        );

        return result == 0;
    }
}
