public sealed class RsaPrivateKey : IPrivateKey
{
    private byte[] _key;

    public RsaPrivateKey(byte[] key)
    {
        if (key == null || key.Length == 0)
            throw new ArgumentException("Private key must not be null or empty.");
        _key = key;
    }

    public byte[] Export() => _key.ToArray();

    public string ToBase64() => Convert.ToBase64String(_key);

    public void Zeroize()
    {
        if (_key != null)
        {
            Array.Clear(_key, 0, _key.Length);
            _key = Array.Empty<byte>();
        }
    }

    public void Dispose() => Zeroize();
}
