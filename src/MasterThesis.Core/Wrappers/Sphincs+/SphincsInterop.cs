using System;
using System.Runtime.InteropServices;

internal static class SphincsInterop
{
    internal const int CRYPTO_SIGN_BYTES = 49856;
    internal const int CRYPTO_SIGN_PUBLICKEYBYTES = 64;
    internal const int CRYPTO_SIGN_SECRETKEYBYTES = 128;

    [DllImport("sphincs.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int sphincs_keypair(byte[] pk, byte[] sk);

    [DllImport("sphincs.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int sphincs_sign(
        byte[] sig, ref ulong siglen,
        byte[] msg, ulong msglen,
        byte[] sk);

    [DllImport("sphincs.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int sphincs_verify(
        byte[] sig, ulong siglen,
        byte[] msg, ulong msglen,
        byte[] pk);
}
