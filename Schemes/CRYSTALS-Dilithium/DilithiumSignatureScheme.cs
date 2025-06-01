using System;

public class DilithiumSignatureScheme : ISignatureScheme<DilithiumPublicKey, DilithiumPrivateKey>
{
    public string Name => "Dilithium";

    public (DilithiumPublicKey, DilithiumPrivateKey) GenerateKeys()
    {
        var pk = new byte[DilithiumInterop.CRYPTO_SIGN_PUBLICKEYBYTES];
        var sk = new byte[DilithiumInterop.CRYPTO_SIGN_SECRETKEYBYTES];

        int result = DilithiumInterop.dilithium_keypair(pk, sk);
        if (result != 0)
            throw new Exception("Dilithium keypair generation failed");

        return (new DilithiumPublicKey(pk), new DilithiumPrivateKey(sk));
    }

    public byte[] Sign(byte[] message, DilithiumPrivateKey privateKey)
    {
        var signature = new byte[DilithiumInterop.CRYPTO_SIGN_BYTES];
        ulong sigLen = (ulong)DilithiumInterop.CRYPTO_SIGN_BYTES;

        int result = DilithiumInterop.dilithium_sign(signature, ref sigLen, message, (ulong)message.Length, privateKey.Export());
        if (result != 0)
            throw new Exception("Dilithium signing failed.");

        Array.Resize(ref signature, (int)sigLen);
        return signature;
    }

    public bool Verify(byte[] message, byte[] signature, DilithiumPublicKey publicKey)
    {
        return DilithiumInterop.dilithium_verify(signature, (ulong)signature.Length, message, (ulong)message.Length, publicKey.Export()) == 0;
    }
}
