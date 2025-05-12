using System.Runtime.InteropServices;

internal static class SodiumInterop
{
    public const int PublicKeyBytes = 32;
    public const int SecretKeyBytes = 64;
    public const int SignatureBytes = 64;

    [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
    public static extern int sodium_init();

    [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr sodium_version_string();

    [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
    public static extern void crypto_sign_keypair(byte[] pk, byte[] sk);

    [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
    public static extern int crypto_sign_detached(
        byte[] sig, out long siglen,
        byte[] m, long mlen,
        byte[] sk);

    [DllImport("libsodium", CallingConvention = CallingConvention.Cdecl)]
    public static extern int crypto_sign_verify_detached(
        byte[] sig, byte[] m, long mlen, byte[] pk);
}

