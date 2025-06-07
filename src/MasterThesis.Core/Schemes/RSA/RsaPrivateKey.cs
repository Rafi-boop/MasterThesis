public sealed class RsaPrivateKey : IPrivateKey
{
    private byte[] _key;

    public RsaPrivateKey(byte[] key)
    {
        _key = key ?? throw new ArgumentException("Private key must not be null.");
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
