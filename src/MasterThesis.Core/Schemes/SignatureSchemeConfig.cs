using System;

/// <summary>
/// Delegate for native keypair generation functions.
/// </summary>
/// <param name="publicKey">The output buffer for the generated public key.</param>
/// <param name="privateKey">The output buffer for the generated private key.</param>
/// <returns>0 on success, non-zero on failure.</returns>
public delegate int KeypairDelegate(byte[] publicKey, byte[] privateKey);

/// <summary>
/// Delegate for native signing functions.
/// </summary>
/// <param name="signature">The output buffer for the generated signature.</param>
/// <param name="signatureLength">The length of the generated signature.</param>
/// <param name="message">The message to sign.</param>
/// <param name="messageLength">The length of the message in bytes.</param>
/// <param name="privateKey">The private key used for signing.</param>
/// <returns>0 on success, non-zero on failure.</returns>
public delegate int SignDelegate(byte[] signature, ref ulong signatureLength, byte[] message, ulong messageLength, byte[] privateKey);

/// <summary>
/// Delegate for native signature verification functions.
/// </summary>
/// <param name="signature">The signature to verify.</param>
/// <param name="signatureLength">The length of the signature in bytes.</param>
/// <param name="message">The original message.</param>
/// <param name="messageLength">The length of the message in bytes.</param>
/// <param name="publicKey">The public key used for verification.</param>
/// <returns>0 if the signature is valid, non-zero otherwise.</returns>
public delegate int VerifyDelegate(byte[] signature, ulong signatureLength, byte[] message, ulong messageLength, byte[] publicKey);

/// <summary>
/// Represents the configuration and native function bindings for a digital signature scheme.
/// </summary>
/// <param name="Name">The name of the signature scheme.</param>
/// <param name="PublicKeySize">The size of the public key in bytes.</param>
/// <param name="PrivateKeySize">The size of the private key in bytes.</param>
/// <param name="SignatureSize">The size of the signature in bytes.</param>
/// <param name="KeypairFunc">The keypair generation function.</param>
/// <param name="SignFunc">The signing function.</param>
/// <param name="VerifyFunc">The signature verification function.</param>
public record SignatureSchemeConfig(
    string Name,
    int PublicKeySize,
    int PrivateKeySize,
    int SignatureSize,
    KeypairDelegate KeypairFunc,
    SignDelegate SignFunc,
    VerifyDelegate VerifyFunc
);
