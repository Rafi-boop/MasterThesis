using System;
using System.Runtime.InteropServices;

/// <summary>
/// Low-level P/Invoke bindings for the CRYSTALS-Dilithium post-quantum signature scheme.
/// </summary>
/// <remarks>
/// This class provides direct access to native functions in <c>dilithium.dll</c>.
/// These bindings are not misuse-resistant and require correct buffer sizes.
/// All functions return <c>0</c> on success and a non-zero value on failure.
/// </remarks>
internal static class DilithiumInterop
{
    /// <summary>
    /// The length of a Dilithium signature in bytes.
    /// </summary>
    internal const int CRYPTO_SIGN_BYTES = 4627;

    /// <summary>
    /// The length of a Dilithium public key in bytes.
    /// </summary>
    internal const int CRYPTO_SIGN_PUBLICKEYBYTES = 2592;

    /// <summary>
    /// The length of a Dilithium private key in bytes.
    /// </summary>
    internal const int CRYPTO_SIGN_SECRETKEYBYTES = 4896;

    /// <summary>
    /// Generates a new Dilithium keypair.
    /// </summary>
    /// <param name="pk">The output buffer for the public key (must be <see cref="CRYPTO_SIGN_PUBLICKEYBYTES"/> bytes).</param>
    /// <param name="sk">The output buffer for the private key (must be <see cref="CRYPTO_SIGN_SECRETKEYBYTES"/> bytes).</param>
    /// <returns><c>0</c> on success, non-zero on failure.</returns>
    [DllImport("dilithium.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int dilithium_keypair(byte[] pk, byte[] sk);

    /// <summary>
    /// Signs a message using a Dilithium private key.
    /// </summary>
    /// <param name="sig">The output buffer for the generated signature.</param>
    /// <param name="siglen">The actual length of the generated signature.</param>
    /// <param name="msg">The message to sign.</param>
    /// <param name="msglen">The length of the message in bytes.</param>
    /// <param name="sk">The private key.</param>
    /// <returns><c>0</c> on success, non-zero on failure.</returns>
    [DllImport("dilithium.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int dilithium_sign(
        byte[] sig, ref ulong siglen,
        byte[] msg, ulong msglen,
        byte[] sk);

    /// <summary>
    /// Verifies a Dilithium signature.
    /// </summary>
    /// <param name="sig">The signature to verify.</param>
    /// <param name="siglen">The length of the signature in bytes.</param>
    /// <param name="msg">The original message.</param>
    /// <param name="msglen">The length of the message in bytes.</param>
    /// <param name="pk">The public key.</param>
    /// <returns><c>0</c> if the signature is valid, non-zero otherwise.</returns>
    [DllImport("dilithium.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int dilithium_verify(
        byte[] sig, ulong siglen,
        byte[] msg, ulong msglen,
        byte[] pk);
}
