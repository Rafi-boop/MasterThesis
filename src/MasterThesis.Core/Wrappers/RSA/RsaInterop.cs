using System;
using System.Security.Cryptography;

/// <summary>
/// Managed implementation of RSA operations using <see cref="System.Security.Cryptography"/>.
/// </summary>
/// <remarks>
/// This interop uses .NET's built-in RSA cryptographic APIs with a 15360-bit key (≈256-bit classical security)
/// and SHA-512 hashing. Buffers must be large enough to hold exported key material. Returns <c>0</c> on success,
/// non-zero on failure.
/// </remarks>
public static class RsaInterop
{
    /// <summary>
    /// Upper bound for an exported RSA public key (RSAPublicKey, DER). Generous for 15360-bit.
    /// </summary>
    public const int CRYPTO_SIGN_PUBLICKEYBYTES = 4096;

    /// <summary>
    /// Upper bound for an exported RSA private key (RSAPrivateKey, DER). Generous for 15360-bit.
    /// </summary>
    public const int CRYPTO_SIGN_SECRETKEYBYTES = 16384;

    /// <summary>
    /// Exact length of an RSA signature equals the modulus size in bytes (15360/8 = 1920).
    /// </summary>
    public const int CRYPTO_SIGN_BYTES = 1920;

    /// <summary>
    /// Generates a new RSA keypair (15360-bit).
    /// </summary>
    public static int rsa_keypair(byte[] publicKey, byte[] privateKey)
    {
        using var rsa = RSA.Create(15360);
        var pub = rsa.ExportRSAPublicKey();
        var priv = rsa.ExportRSAPrivateKey();

        if (pub.Length > publicKey.Length || priv.Length > privateKey.Length)
            throw new ArgumentException("Provided buffers are too small for RSA key export.");

        Array.Copy(pub, publicKey, pub.Length);
        Array.Copy(priv, privateKey, priv.Length);
        return 0;
    }

    /// <summary>
    /// Signs a message using an RSA private key with SHA-512 and PKCS#1 v1.5 padding.
    /// </summary>
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

    /// <summary>
    /// Verifies an RSA signature using SHA-512 and PKCS#1 v1.5 padding.
    /// </summary>
    public static int rsa_verify(byte[] signature, ulong sigLen, byte[] message, ulong messageLength, byte[] publicKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(publicKey, out _);
        return rsa.VerifyData(message, signature, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1) ? 0 : -1;
    }
}
