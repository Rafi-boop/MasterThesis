/// <summary>
/// Provides the configuration for the EdDSA (Ed25519) signature scheme using libsodium.
/// </summary>
public static class EdDsaConfig
{
    /// <summary>
    /// The predefined EdDSA configuration.
    /// </summary>
    public static readonly SignatureSchemeConfig Config = new(
        "EdDSA",
        EdDsaInterop.CRYPTO_SIGN_PUBLICKEYBYTES,
        EdDsaInterop.CRYPTO_SIGN_SECRETKEYBYTES,
        EdDsaInterop.CRYPTO_SIGN_BYTES,
        EdDsaInterop.crypto_sign_ed25519_keypair,
        EdDsaInterop.crypto_sign_ed25519_detached,
        (byte[] sig, ulong sigLen, byte[] msg, ulong msgLen, byte[] pk) =>
            EdDsaInterop.crypto_sign_ed25519_verify_detached(sig, msg, msgLen, pk)
    );
}
