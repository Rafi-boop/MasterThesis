public sealed class FalconPublicKey : IPublicKey
{
    private readonly byte[] _key;

    public FalconPublicKey(byte[] key)
    {
        if (key == null || key.Length != FalconInterop.CRYPTO_SIGN_PUBLICKEYBYTES)
            throw new ArgumentException($"Invalid Falcon public key length. Expected {FalconInterop.CRYPTO_SIGN_PUBLICKEYBYTES} bytes.");

        _key = key.ToArray();
    }

    public byte[] Export() => _key.ToArray();

    public string ToBase64() => Convert.ToBase64String(_key);
}
