public sealed class EcdsaPublicKey : IPublicKey
{
    private readonly byte[] _key;

    public EcdsaPublicKey(byte[] key)
    {
        _key = key ?? throw new ArgumentNullException(nameof(key));
    }

    public byte[] Export() => _key.ToArray();
    public string ToBase64() => Convert.ToBase64String(_key);
}
