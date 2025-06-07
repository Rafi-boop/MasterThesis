public sealed class DilithiumPublicKey : IPublicKey
{
    private readonly byte[] _key;

    public DilithiumPublicKey(byte[] key)
    {
        if (key == null || key.Length != DilithiumInterop.CRYPTO_SIGN_PUBLICKEYBYTES)
            throw new ArgumentException($"Invalid Dilithium public key length. Expected {DilithiumInterop.CRYPTO_SIGN_PUBLICKEYBYTES} bytes.");

        _key = key.ToArray();
    }

    public byte[] Export() => _key.ToArray();

    public string ToBase64() => Convert.ToBase64String(_key);
}
