public static class EdDsaConfig
{
    public static readonly SignatureSchemeConfig Config = new(
        "EdDSA",
        Interop.Libsodium.CRYPTO_SIGN_PUBLICKEYBYTES,
        Interop.Libsodium.CRYPTO_SIGN_SECRETKEYBYTES,
        Interop.Libsodium.CRYPTO_SIGN_BYTES,
        Interop.Libsodium.crypto_sign_ed25519_keypair,
        Interop.Libsodium.crypto_sign_ed25519_detached,
        (byte[] sig, ulong sigLen, byte[] msg, ulong msgLen, byte[] pk) =>
            Interop.Libsodium.crypto_sign_ed25519_verify_detached(sig, msg, msgLen, pk)
    );
}
