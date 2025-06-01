using System;

public class SphincsSignatureScheme : ISignatureScheme<SphincsPublicKey, SphincsPrivateKey>
{
    public string Name => "Sphincs";

    public (SphincsPublicKey, SphincsPrivateKey) GenerateKeys()
    {
        var pk = new byte[SphincsInterop.CRYPTO_SIGN_PUBLICKEYBYTES];
        var sk = new byte[SphincsInterop.CRYPTO_SIGN_SECRETKEYBYTES];

        int result = SphincsInterop.sphincs_keypair(pk, sk);
        if (result != 0)
            throw new Exception("Sphincs keypair generation failed");

        return (new SphincsPublicKey(pk), new SphincsPrivateKey(sk));
    }

    public byte[] Sign(byte[] message, SphincsPrivateKey privateKey)
    {
        var signature = new byte[SphincsInterop.CRYPTO_SIGN_BYTES];
        ulong sigLen = (ulong)SphincsInterop.CRYPTO_SIGN_BYTES;

        int result = SphincsInterop.sphincs_sign(signature, ref sigLen, message, (ulong)message.Length, privateKey.Export());
        if (result != 0)
            throw new Exception("Sphincs signing failed.");

        Array.Resize(ref signature, (int)sigLen);
        return signature;
    }

    public bool Verify(byte[] message, byte[] signature, SphincsPublicKey publicKey)
    {
        return SphincsInterop.sphincs_verify(signature, (ulong)signature.Length, message, (ulong)message.Length, publicKey.Export()) == 0;
    }
}
