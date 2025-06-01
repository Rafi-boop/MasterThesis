public sealed class SphincsPublicKey : IPublicKey
{
    private readonly byte[] _key;

    public SphincsPublicKey(byte[] key)
    {
        if (key == null || key.Length != SphincsInterop.CRYPTO_SIGN_PUBLICKEYBYTES)
            throw new ArgumentException($"Invalid Sphincs public key length. Expected {SphincsInterop.CRYPTO_SIGN_PUBLICKEYBYTES} bytes.");

        _key = key.ToArray();
    }

    public byte[] Export() => _key.ToArray();

    public string ToBase64() => Convert.ToBase64String(_key);
}
