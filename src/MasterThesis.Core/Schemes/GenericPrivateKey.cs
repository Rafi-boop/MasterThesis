using System;
using System.Runtime.InteropServices;

/// <summary>
/// Represents a generic private key with secure memory handling.
/// </summary>
public sealed class GenericPrivateKey : IPrivateKey
{
    private byte[] _key;
    private GCHandle _pinned;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericPrivateKey"/> class.
    /// </summary>
    /// <param name="key">The private key bytes.</param>
    /// <param name="expectedLength">The expected length of the key in bytes.</param>
    /// <exception cref="ArgumentException">Thrown if the key length does not match the expected length.</exception>
    public GenericPrivateKey(byte[] key, int expectedLength)
    {
        if (key == null || key.Length != expectedLength)
            throw new ArgumentException($"Invalid private key length. Expected {expectedLength} bytes.");

        _key = key.ToArray();
        _pinned = GCHandle.Alloc(_key, GCHandleType.Pinned);
    }

    /// <inheritdoc/>
    public byte[] Export() => _key.ToArray();

    /// <inheritdoc/>
    public string ToBase64() => Convert.ToBase64String(_key);

    /// <inheritdoc/>
    public void Zeroize()
    {
        if (_key.Length > 0)
        {
            Array.Clear(_key, 0, _key.Length);
            _pinned.Free();
            _key = Array.Empty<byte>();
        }
    }

    /// <inheritdoc/>
    public void Dispose() => Zeroize();
}
