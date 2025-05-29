using System;

public class FalconSignatureScheme : ISignatureScheme<FalconPublicKey, FalconPrivateKey>
{
    public string Name => "falcon";

    public (FalconPublicKey, FalconPrivateKey) GenerateKeys()
    {
        var pk = new byte[FalconInterop.CRYPTO_SIGN_PUBLICKEYBYTES];
        var sk = new byte[FalconInterop.CRYPTO_SIGN_SECRETKEYBYTES];

        int result = FalconInterop.falcon_keypair(pk, sk);
        if (result != 0)
            throw new Exception("Falcon keypair generation failed");

        return (new FalconPublicKey(pk), new FalconPrivateKey(sk));
    }

    public byte[] Sign(byte[] message, FalconPrivateKey privateKey)
    {
        var signature = new byte[FalconInterop.CRYPTO_SIGN_BYTES];
        ulong sigLen = (ulong)FalconInterop.CRYPTO_SIGN_BYTES;

        int result = FalconInterop.falcon_sign(signature, ref sigLen, message, (ulong)message.Length, privateKey.Export());
        if (result != 0)
            throw new Exception("Falcon signing failed.");

        Array.Resize(ref signature, (int)sigLen);
        return signature;
    }

    public bool Verify(byte[] message, byte[] signature, FalconPublicKey publicKey)
    {
        return FalconInterop.falcon_verify(signature, (ulong)signature.Length, message, (ulong)message.Length, publicKey.Export()) == 0;
    }
}
