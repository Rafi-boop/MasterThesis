/// <summary>
/// Provides the configuration for the Falcon post-quantum signature scheme.
/// </summary>
public static class FalconConfig
{
    /// <summary>
    /// The predefined Falcon configuration.
    /// </summary>
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
