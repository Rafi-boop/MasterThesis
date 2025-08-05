public static class EcdsaConfig
{
    public static readonly SignatureSchemeConfig Config = new(
        "ECDSA",
        EcdsaInterop.CRYPTO_SIGN_PUBLICKEYBYTES,
        EcdsaInterop.CRYPTO_SIGN_SECRETKEYBYTES,
        EcdsaInterop.CRYPTO_SIGN_BYTES,
        EcdsaInterop.ecdsa_keypair,
        EcdsaInterop.ecdsa_sign,
        EcdsaInterop.ecdsa_verify
    );
}
