public sealed class RsaPublicKey : IPublicKey
{
    private readonly byte[] _key;

    public RsaPublicKey(byte[] key)
    {
        if (key == null || key.Length == 0)
            throw new ArgumentException("Public key must not be null or empty.");
        _key = key;
    }

    public byte[] Export() => _key.ToArray();

    public string ToBase64() => Convert.ToBase64String(_key);
}
