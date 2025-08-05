public static class SphincsConfig
{
    public static readonly SignatureSchemeConfig Config = new(
        "Sphincs+",
        SphincsInterop.CRYPTO_SIGN_PUBLICKEYBYTES,
        SphincsInterop.CRYPTO_SIGN_SECRETKEYBYTES,
        SphincsInterop.CRYPTO_SIGN_BYTES,
        SphincsInterop.sphincs_keypair,
        SphincsInterop.sphincs_sign,
        SphincsInterop.sphincs_verify
    );
}
