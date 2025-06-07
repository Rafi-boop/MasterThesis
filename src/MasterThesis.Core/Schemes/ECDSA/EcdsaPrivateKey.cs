public sealed class EcdsaPrivateKey : IPrivateKey
{
    private byte[] _key;

    public EcdsaPrivateKey(byte[] key)
    {
        _key = key ?? throw new ArgumentNullException(nameof(key));
    }

    public byte[] Export() => _key.ToArray();
    public string ToBase64() => Convert.ToBase64String(_key);
    public void Zeroize() => Array.Clear(_key, 0, _key.Length);
    public void Dispose() => Zeroize();
}
