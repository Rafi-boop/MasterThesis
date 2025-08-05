using System;
using System.Security.Cryptography;

/// <summary>
/// Managed implementation of ECDSA operations using <see cref="System.Security.Cryptography"/>.
/// </summary>
/// <remarks>
/// This interop uses the built-in .NET cryptographic APIs for ECDSA with the NIST P-256 curve
/// and SHA-256 hashing. Buffers must be large enough to hold the exported key material.
/// Returns <c>0</c> on success, non-zero on failure.
/// </remarks>
public static class EcdsaInterop
{
    /// <summary>
    /// The maximum size of the exported public key in bytes.
    /// </summary>
    public const int CRYPTO_SIGN_PUBLICKEYBYTES = 256;

    /// <summary>
    /// The maximum size of the exported private key in bytes.
    /// </summary>
    public const int CRYPTO_SIGN_SECRETKEYBYTES = 512;

    /// <summary>
    /// The typical size of an ECDSA signature in bytes (DER encoded).
    /// </summary>
    public const int CRYPTO_SIGN_BYTES = 72;

    /// <summary>
    /// Generates a new ECDSA keypair.
    /// </summary>
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

    /// <summary>
    /// Signs a message using an ECDSA private key.
    /// </summary>
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

    /// <summary>
    /// Verifies an ECDSA signature.
    /// </summary>
    public static int ecdsa_verify(byte[] signature, ulong sigLen, byte[] message, ulong messageLength, byte[] publicKey)
    {
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportSubjectPublicKeyInfo(publicKey, out _);
        return ecdsa.VerifyData(message, signature, HashAlgorithmName.SHA256) ? 0 : -1;
    }
}
