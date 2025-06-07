using System.Runtime.InteropServices;

public sealed class SphincsPrivateKey : IPrivateKey
{
    private byte[] _key;
    private GCHandle _pinned;

    public SphincsPrivateKey(byte[] key)
    {
        if (key == null || key.Length != SphincsInterop.CRYPTO_SIGN_SECRETKEYBYTES)
            throw new ArgumentException($"Invalid Sphincs private key length. Expected {SphincsInterop.CRYPTO_SIGN_SECRETKEYBYTES} bytes.");

        _key = key.ToArray();
        _pinned = GCHandle.Alloc(_key, GCHandleType.Pinned);
    }

    public byte[] Export() => _key.ToArray();

    public string ToBase64() => Convert.ToBase64String(_key);

    public void Zeroize()
    {
        if (_key != null && _key.Length > 0)
        {
            Array.Clear(_key, 0, _key.Length);
            _pinned.Free();
            _key = Array.Empty<byte>();
        }
    }

    public void Dispose() => Zeroize();
}
