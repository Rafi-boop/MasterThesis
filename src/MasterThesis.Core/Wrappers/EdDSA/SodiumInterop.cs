using System;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class Libsodium
    {
        internal const int CRYPTO_SIGN_BYTES = 64;
        internal const int CRYPTO_SIGN_PUBLICKEYBYTES = 32;
        internal const int CRYPTO_SIGN_SECRETKEYBYTES = 64;

        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int sodium_init();

        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr sodium_version_string();

        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_keypair(
            byte[] pk,
            byte[] sk);

        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_detached(
            byte[] sig,
            ref ulong siglen,
            byte[] msg,
            ulong msglen,
            byte[] sk);

        [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_verify_detached(
            byte[] sig,
            byte[] msg,
            ulong msglen,
            byte[] pk);
    }
}
