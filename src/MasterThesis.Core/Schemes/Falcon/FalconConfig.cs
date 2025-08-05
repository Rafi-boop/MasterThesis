public static class FalconConfig
{
    public static readonly SignatureSchemeConfig Config = new(
        "Falcon",
        FalconInterop.CRYPTO_SIGN_PUBLICKEYBYTES,
        FalconInterop.CRYPTO_SIGN_SECRETKEYBYTES,
        FalconInterop.CRYPTO_SIGN_BYTES,
        FalconInterop.falcon_keypair,
        FalconInterop.falcon_sign,
        FalconInterop.falcon_verify
    );
}
