public static class DilithiumConfig
{
    public static readonly SignatureSchemeConfig Config = new(
        "Dilithium",
        DilithiumInterop.CRYPTO_SIGN_PUBLICKEYBYTES,
        DilithiumInterop.CRYPTO_SIGN_SECRETKEYBYTES,
        DilithiumInterop.CRYPTO_SIGN_BYTES,
        DilithiumInterop.dilithium_keypair,
        DilithiumInterop.dilithium_sign,
        DilithiumInterop.dilithium_verify
    );
}
