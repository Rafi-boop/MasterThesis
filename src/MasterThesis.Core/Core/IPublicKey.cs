/// <summary>
/// Represents a public key in a digital signature scheme.
/// </summary>
public interface IPublicKey
{
    /// <summary>
    /// Exports the public key as a byte array.
    /// </summary>
    /// <returns>The public key bytes.</returns>
    byte[] Export();

    /// <summary>
    /// Encodes the public key as a Base64 string.
    /// </summary>
    /// <returns>The public key in Base64 format.</returns>
    string ToBase64();
}
