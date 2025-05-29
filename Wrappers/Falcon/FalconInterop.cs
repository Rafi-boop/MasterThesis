using System;
using System.Runtime.InteropServices;

internal static class FalconInterop
{
    internal const int CRYPTO_SIGN_BYTES = 1462;
    internal const int CRYPTO_SIGN_PUBLICKEYBYTES = 1793;
    internal const int CRYPTO_SIGN_SECRETKEYBYTES = 2305;

    [DllImport("falcon.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int falcon_keypair(byte[] pk, byte[] sk);

    [DllImport("falcon.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int falcon_sign(
        byte[] sig, ref ulong siglen,
        byte[] msg, ulong msglen,
        byte[] sk);

    [DllImport("falcon.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int falcon_verify(
        byte[] sig, ulong siglen,
        byte[] msg, ulong msglen,
        byte[] pk);
}
