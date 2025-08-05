public sealed class GenericPublicKey : IPublicKey
{
    private readonly byte[] _key;

    public GenericPublicKey(byte[] key, int expectedLength)
    {
        if (key == null || key.Length != expectedLength)
            throw new ArgumentException($"Invalid public key length. Expected {expectedLength} bytes.");
        _key = key.ToArray();
    }

    public byte[] Export() => _key.ToArray();

    public string ToBase64() => Convert.ToBase64String(_key);
}
