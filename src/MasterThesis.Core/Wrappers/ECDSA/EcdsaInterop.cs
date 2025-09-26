using System;
using System.Security.Cryptography;

/// <summary>
/// Managed implementation of ECDSA operations using <see cref="System.Security.Cryptography"/>.
/// </summary>
/// <remarks>
/// This interop uses the built-in .NET cryptographic APIs for ECDSA with the NIST P-521 curve
/// and SHA-512 hashing (≈256-bit classical security). Buffers must be large enough to hold
/// the exported key material. Returns <c>0</c> on success, non-zero on failure.
/// </remarks>
public static class EcdsaInterop
{
    /// <summary>
    /// A safe upper bound for the exported public key (SubjectPublicKeyInfo, DER).
    /// </summary>
    public const int CRYPTO_SIGN_PUBLICKEYBYTES = 256;

    /// <summary>
    /// A safe upper bound for the exported private key (PKCS#8, DER).
    /// </summary>
    public const int CRYPTO_SIGN_SECRETKEYBYTES = 512;

    /// <summary>
    /// A safe upper bound for a DER-encoded ECDSA P-521 signature (r,s).
    /// </summary>
    public const int CRYPTO_SIGN_BYTES = 144;

    /// <summary>
    /// Generates a new ECDSA keypair (NIST P-521).
    /// </summary>
    public static int ecdsa_keypair(byte[] publicKey, byte[] privateKey)
    {
        using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP521);
        var pub = ecdsa.ExportSubjectPublicKeyInfo();
        var priv = ecdsa.ExportPkcs8PrivateKey();

        if (pub.Length > publicKey.Length || priv.Length > privateKey.Length)
            throw new ArgumentException("Provided buffers are too small for ECDSA key export.");

        Array.Copy(pub, publicKey, pub.Length);
        Array.Copy(priv, privateKey, priv.Length);
        return 0;
    }

    /// <summary>
    /// Signs a message using an ECDSA P-521 private key with SHA-512.
    /// </summary>
    public static int ecdsa_sign(byte[] signature, ref ulong sigLen, byte[] message, ulong messageLength, byte[] privateKey)
    {
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportPkcs8PrivateKey(privateKey, out _);
        var sig = ecdsa.SignData(message, HashAlgorithmName.SHA512);

        if (sig.Length > signature.Length)
            throw new ArgumentException("Signature buffer too small.");

        Array.Copy(sig, signature, sig.Length);
        sigLen = (ulong)sig.Length;
        return 0;
    }

    /// <summary>
    /// Verifies an ECDSA P-521 signature (SHA-512).
    /// </summary>
    public static int ecdsa_verify(byte[] signature, ulong sigLen, byte[] message, ulong messageLength, byte[] publicKey)
    {
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportSubjectPublicKeyInfo(publicKey, out _);
        return ecdsa.VerifyData(message, signature, HashAlgorithmName.SHA512) ? 0 : -1;
    }
}
