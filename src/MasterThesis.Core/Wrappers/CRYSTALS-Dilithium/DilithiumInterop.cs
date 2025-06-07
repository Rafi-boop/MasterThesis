using System;
using System.Runtime.InteropServices;

internal static class DilithiumInterop
{
    internal const int CRYPTO_SIGN_BYTES = 4627;
    internal const int CRYPTO_SIGN_PUBLICKEYBYTES = 2592;
    internal const int CRYPTO_SIGN_SECRETKEYBYTES = 4896;

    [DllImport("dilithium.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int dilithium_keypair(byte[] pk, byte[] sk);

    [DllImport("dilithium.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int dilithium_sign(
        byte[] sig, ref ulong siglen,
        byte[] msg, ulong msglen,
        byte[] sk);

    [DllImport("dilithium.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int dilithium_verify(
        byte[] sig, ulong siglen,
        byte[] msg, ulong msglen,
        byte[] pk);
}
