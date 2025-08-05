using System;
using System.Security.Cryptography;

public static class RsaInterop
{
    // Sizes here are only defaults for preallocation in configs — actual lengths may vary
    public const int CRYPTO_SIGN_PUBLICKEYBYTES = 2048; 
    public const int CRYPTO_SIGN_SECRETKEYBYTES = 4096; 
    public const int CRYPTO_SIGN_BYTES = 512;           

    public static int rsa_keypair(byte[] publicKey, byte[] privateKey)
    {
        using var rsa = RSA.Create(4096);
        var pub = rsa.ExportRSAPublicKey();
        var priv = rsa.ExportRSAPrivateKey();

        if (pub.Length > publicKey.Length || priv.Length > privateKey.Length)
            throw new ArgumentException("Provided buffers are too small for RSA key export.");

        Array.Copy(pub, publicKey, pub.Length);
        Array.Copy(priv, privateKey, priv.Length);

        return 0;
    }

    public static int rsa_sign(byte[] signature, ref ulong sigLen, byte[] message, ulong messageLength, byte[] privateKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(privateKey, out _);
        var sig = rsa.SignData(message, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);

        if (sig.Length > signature.Length)
            throw new ArgumentException("Signature buffer too small.");

        Array.Copy(sig, signature, sig.Length);
        sigLen = (ulong)sig.Length;

        return 0;
    }

    public static int rsa_verify(byte[] signature, ulong sigLen, byte[] message, ulong messageLength, byte[] publicKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(publicKey, out _);
        return rsa.VerifyData(message, signature, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1) ? 0 : -1;
    }
}
