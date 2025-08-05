using System;
using System.Runtime.InteropServices;

/// <summary>
/// Low-level P/Invoke bindings for the SPHINCS+ post-quantum signature scheme.
/// </summary>
/// <remarks>
/// Directly calls into <c>sphincs.dll</c>. Buffers must be correctly sized. 
/// Returns <c>0</c> on success, non-zero on failure.
/// </remarks>
internal static class SphincsInterop
{
    internal const int CRYPTO_SIGN_BYTES = 49856;
    internal const int CRYPTO_SIGN_PUBLICKEYBYTES = 64;
    internal const int CRYPTO_SIGN_SECRETKEYBYTES = 128;

    /// <summary>
    /// Generates a new SPHINCS+ keypair.
    /// </summary>
    [DllImport("sphincs.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int sphincs_keypair(byte[] pk, byte[] sk);

    /// <summary>
    /// Signs a message using a SPHINCS+ private key.
    /// </summary>
    [DllImport("sphincs.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int sphincs_sign(
        byte[] sig, ref ulong siglen,
        byte[] msg, ulong msglen,
        byte[] sk);

    /// <summary>
    /// Verifies a SPHINCS+ signature.
    /// </summary>
    [DllImport("sphincs.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int sphincs_verify(
        byte[] sig, ulong siglen,
        byte[] msg, ulong msglen,
        byte[] pk);
}
