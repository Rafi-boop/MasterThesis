using System;
using System.Runtime.InteropServices;

/// <summary>
/// Low-level P/Invoke bindings for the EdDSA (Ed25519) signature scheme using libsodium.
/// </summary>
/// <remarks>
/// Provides direct access to native functions in <c>libsodium</c>.
/// Buffers must be the correct size. Returns <c>0</c> on success, non-zero on failure.
/// </remarks>
internal static class EdDsaInterop
{
    internal const int CRYPTO_SIGN_BYTES = 64;
    internal const int CRYPTO_SIGN_PUBLICKEYBYTES = 32;
    internal const int CRYPTO_SIGN_SECRETKEYBYTES = 64;

    /// <summary>
    /// Initializes the libsodium library.
    /// </summary>
    [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int sodium_init();

    /// <summary>
    /// Gets the version string of libsodium.
    /// </summary>
    [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr sodium_version_string();

    /// <summary>
    /// Generates an Ed25519 keypair.
    /// </summary>
    [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int crypto_sign_ed25519_keypair(byte[] pk, byte[] sk);

    /// <summary>
    /// Signs a message using an Ed25519 private key.
    /// </summary>
    [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int crypto_sign_ed25519_detached(
        byte[] sig,
        ref ulong siglen,
        byte[] msg,
        ulong msglen,
        byte[] sk);

    /// <summary>
    /// Verifies an Ed25519 signature.
    /// </summary>
    [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int crypto_sign_ed25519_verify_detached(
        byte[] sig,
        byte[] msg,
        ulong msglen,
        byte[] pk);
}
