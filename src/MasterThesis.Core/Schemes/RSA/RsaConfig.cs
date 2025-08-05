public static class RsaConfig
{
    public static readonly SignatureSchemeConfig Config = new(
        "RSA",
        RsaInterop.CRYPTO_SIGN_PUBLICKEYBYTES,
        RsaInterop.CRYPTO_SIGN_SECRETKEYBYTES,
        RsaInterop.CRYPTO_SIGN_BYTES,
        RsaInterop.rsa_keypair,
        RsaInterop.rsa_sign,
        RsaInterop.rsa_verify
    );
}
