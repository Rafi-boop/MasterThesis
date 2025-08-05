using System;

/// <summary>
/// Represents a private key in a digital signature scheme.
/// </summary>
public interface IPrivateKey : IDisposable
{
    /// <summary>
    /// Exports the private key as a byte array.
    /// </summary>
    /// <returns>The private key bytes.</returns>
    byte[] Export();

    /// <summary>
    /// Encodes the private key as a Base64 string.
    /// </summary>
    /// <returns>The private key in Base64 format.</returns>
    string ToBase64();

    /// <summary>
    /// Securely clears the private key from memory.
    /// </summary>
    void Zeroize();
}
