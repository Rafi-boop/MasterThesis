using System.Runtime.InteropServices;

public sealed class GenericPrivateKey : IPrivateKey
{
    private byte[] _key;
    private GCHandle _pinned;

    public GenericPrivateKey(byte[] key, int expectedLength)
    {
        if (key == null || key.Length != expectedLength)
            throw new ArgumentException($"Invalid private key length. Expected {expectedLength} bytes.");

        _key = key.ToArray();
        _pinned = GCHandle.Alloc(_key, GCHandleType.Pinned);
    }

    public byte[] Export() => _key.ToArray();

    public string ToBase64() => Convert.ToBase64String(_key);

    public void Zeroize()
    {
        if (_key.Length > 0)
        {
            Array.Clear(_key, 0, _key.Length);
            _pinned.Free();
            _key = Array.Empty<byte>();
        }
    }

    public void Dispose() => Zeroize();
}
