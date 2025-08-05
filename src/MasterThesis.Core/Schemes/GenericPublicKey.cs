using System;

/// <summary>
/// Represents a generic public key.
/// </summary>
public sealed class GenericPublicKey : IPublicKey
{
    private readonly byte[] _key;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericPublicKey"/> class.
    /// </summary>
    /// <param name="key">The public key bytes.</param>
    /// <param name="expectedLength">The expected length of the key in bytes.</param>
    /// <exception cref="ArgumentException">Thrown if the key length does not match the expected length.</exception>
    public GenericPublicKey(byte[] key, int expectedLength)
    {
        if (key == null || key.Length != expectedLength)
            throw new ArgumentException($"Invalid public key length. Expected {expectedLength} bytes.");
        _key = key.ToArray();
    }

    /// <inheritdoc/>
    public byte[] Export() => _key.ToArray();

    /// <inheritdoc/>
    public string ToBase64() => Convert.ToBase64String(_key);
}
