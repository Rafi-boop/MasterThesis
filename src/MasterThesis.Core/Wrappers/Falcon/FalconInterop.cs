using System;
using System.Runtime.InteropServices;

/// <summary>
/// Low-level P/Invoke bindings for the Falcon post-quantum signature scheme.
/// </summary>
/// <remarks>
/// This class provides direct access to <c>falcon.dll</c>.
/// Buffers must match the expected lengths. Returns <c>0</c> on success, non-zero on failure.
/// </remarks>
internal static class FalconInterop
{
    internal const int CRYPTO_SIGN_BYTES = 1462;
    internal const int CRYPTO_SIGN_PUBLICKEYBYTES = 1793;
    internal const int CRYPTO_SIGN_SECRETKEYBYTES = 2305;

    /// <summary>
    /// Generates a new Falcon keypair.
    /// </summary>
    [DllImport("falcon.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int falcon_keypair(byte[] pk, byte[] sk);

    /// <summary>
    /// Signs a message using a Falcon private key.
    /// </summary>
    [DllImport("falcon.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int falcon_sign(
        byte[] sig, ref ulong siglen,
        byte[] msg, ulong msglen,
        byte[] sk);

    /// <summary>
    /// Verifies a Falcon signature.
    /// </summary>
    [DllImport("falcon.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int falcon_verify(
        byte[] sig, ulong siglen,
        byte[] msg, ulong msglen,
        byte[] pk);
}
