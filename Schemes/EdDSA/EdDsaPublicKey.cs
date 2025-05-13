public sealed class EdDsaPublicKey : IPublicKey
{
    private readonly byte[] _key;

    public EdDsaPublicKey(byte[] key)
    {
        if (key == null || key.Length != Interop.Libsodium.CRYPTO_SIGN_PUBLICKEYBYTES)
            throw new ArgumentException($"Public key must be {Interop.Libsodium.CRYPTO_SIGN_PUBLICKEYBYTES} bytes.", nameof(key));

        _key = key.ToArray();
    }

    public byte[] Export() => _key.ToArray();

    public string ToBase64() => Convert.ToBase64String(_key);
}
