using MasterThesis.Core.DTOs;

/// <summary>
/// Extends the base signature scheme interface with runtime-friendly operations
/// using Base64-encoded keys.
/// </summary>
public interface ISignatureSchemeDynamic : ISignatureSchemeBase
{
    /// <summary>
    /// Generates a new key pair and returns it as Base64-encoded strings.
    /// </summary>
    /// <returns>A <see cref="KeyPairResponse"/> containing the public and private keys.</returns>
    KeyPairResponse GenerateKeysDynamic();

    /// <summary>
    /// Signs a message using a Base64-encoded private key.
    /// </summary>
    /// <param name="message">The message to sign.</param>
    /// <param name="privateKeyBase64">The private key in Base64 encoding.</param>
    /// <returns>The digital signature as a byte array.</returns>
    byte[] SignDynamic(byte[] message, string privateKeyBase64);

    /// <summary>
    /// Verifies a signature using a Base64-encoded public key.
    /// </summary>
    /// <param name="message">The original message.</param>
    /// <param name="signature">The digital signature to verify.</param>
    /// <param name="publicKeyBase64">The public key in Base64 encoding.</param>
    /// <returns><c>true</c> if the signature is valid; otherwise, <c>false</c>.</returns>
    bool VerifyDynamic(byte[] message, byte[] signature, string publicKeyBase64);
}
