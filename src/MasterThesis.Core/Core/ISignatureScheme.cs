/// <summary>
/// Defines the generic contract for any digital signature scheme.
/// </summary>
/// <typeparam name="TPublicKey">The public key type.</typeparam>
/// <typeparam name="TPrivateKey">The private key type.</typeparam>
public interface ISignatureScheme<TPublicKey, TPrivateKey> : ISignatureSchemeBase
    where TPublicKey : IPublicKey
    where TPrivateKey : IPrivateKey
{
    /// <summary>
    /// Generates a new key pair.
    /// </summary>
    /// <returns>A tuple containing the public and private keys.</returns>
    (TPublicKey publicKey, TPrivateKey privateKey) GenerateKeys();

    /// <summary>
    /// Signs a message using the provided private key.
    /// </summary>
    /// <param name="message">The message to sign.</param>
    /// <param name="privateKey">The private key.</param>
    /// <returns>The digital signature as a byte array.</returns>
    byte[] Sign(byte[] message, TPrivateKey privateKey);

    /// <summary>
    /// Verifies a signature using the provided public key.
    /// </summary>
    /// <param name="message">The original message.</param>
    /// <param name="signature">The digital signature to verify.</param>
    /// <param name="publicKey">The public key.</param>
    /// <returns><c>true</c> if the signature is valid; otherwise, <c>false</c>.</returns>
    bool Verify(byte[] message, byte[] signature, TPublicKey publicKey);
}
