public sealed class EdDsaPublicKey : IPublicKey
{
    private readonly byte[] _key;

    public EdDsaPublicKey(byte[] key)
    {
        _key = key ?? throw new ArgumentException("Public key must not be null.");
    }

    public byte[] Export() => _key.ToArray();

    public string ToBase64() => Convert.ToBase64String(_key);
}
