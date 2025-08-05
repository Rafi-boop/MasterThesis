/// <summary>
/// Provides the configuration for the ECDSA signature scheme.
/// </summary>
public static class EcdsaConfig
{
    /// <summary>
    /// The predefined ECDSA configuration.
    /// </summary>
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
