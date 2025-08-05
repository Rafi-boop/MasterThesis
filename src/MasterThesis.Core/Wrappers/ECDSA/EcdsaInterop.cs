using System;
using System.Security.Cryptography;

public static class EcdsaInterop
{
    public const int CRYPTO_SIGN_PUBLICKEYBYTES = 256; // large enough buffer
    public const int CRYPTO_SIGN_SECRETKEYBYTES = 512; // large enough buffer
    public const int CRYPTO_SIGN_BYTES = 72;           // typical DER signature size

    public static int ecdsa_keypair(byte[] publicKey, byte[] privateKey)
    {
        using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        var pub = ecdsa.ExportSubjectPublicKeyInfo();
        var priv = ecdsa.ExportPkcs8PrivateKey();

        if (pub.Length > publicKey.Length || priv.Length > privateKey.Length)
            throw new ArgumentException("Provided buffers are too small for ECDSA key export.");

        Array.Copy(pub, publicKey, pub.Length);
        Array.Copy(priv, privateKey, priv.Length);

        return 0;
    }

    public static int ecdsa_sign(byte[] signature, ref ulong sigLen, byte[] message, ulong messageLength, byte[] privateKey)
    {
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportPkcs8PrivateKey(privateKey, out _);
        var sig = ecdsa.SignData(message, HashAlgorithmName.SHA256);

        if (sig.Length > signature.Length)
            throw new ArgumentException("Signature buffer too small.");

        Array.Copy(sig, signature, sig.Length);
        sigLen = (ulong)sig.Length;

        return 0;
    }

    public static int ecdsa_verify(byte[] signature, ulong sigLen, byte[] message, ulong messageLength, byte[] publicKey)
    {
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportSubjectPublicKeyInfo(publicKey, out _);
        return ecdsa.VerifyData(message, signature, HashAlgorithmName.SHA256) ? 0 : -1;
    }
}
