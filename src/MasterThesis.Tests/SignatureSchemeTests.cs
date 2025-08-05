using System;
using System.Text;
using Xunit;

/// <summary>
/// Provides reusable test methods to validate signature scheme implementations.
/// </summary>
public static class SignatureSchemeTests
{
    /// <summary>Runs the complete set of tests for the specified scheme.</summary>
    public static void RunTests(string schemeName)
    {
        GenerateKeys_ShouldProduceValidKeys(schemeName);
        SignAndVerify_ShouldSucceed_OnValidInput(schemeName);
        Verify_ShouldFail_OnModifiedMessage(schemeName);
        Verify_ShouldFail_WithWrongPublicKey(schemeName);
        Verify_ShouldFail_WithCorruptedSignature(schemeName);
    }

    /// <summary>Ensures that key generation produces valid keys.</summary>
    public static void GenerateKeys_ShouldProduceValidKeys(string schemeName)
    {
        var scheme = GetScheme(schemeName);
        var keyPair = scheme.GenerateKeysDynamic();

        Assert.NotNull(keyPair.PublicKey);
        Assert.NotNull(keyPair.PrivateKey);
    }

    /// <summary>Ensures signing and verification succeed on valid input.</summary>
    public static void SignAndVerify_ShouldSucceed_OnValidInput(string schemeName)
    {
        var scheme = GetScheme(schemeName);
        var keyPair = scheme.GenerateKeysDynamic();
        var message = Encoding.UTF8.GetBytes("secure message");

        var signature = scheme.SignDynamic(message, keyPair.PrivateKey);
        Assert.NotEmpty(signature);

        var valid = scheme.VerifyDynamic(message, signature, keyPair.PublicKey);
        Assert.True(valid);
    }

    /// <summary>Ensures verification fails when the message is modified.</summary>
    public static void Verify_ShouldFail_OnModifiedMessage(string schemeName)
    {
        var scheme = GetScheme(schemeName);
        var keyPair = scheme.GenerateKeysDynamic();
        var original = Encoding.UTF8.GetBytes("original");
        var tampered = Encoding.UTF8.GetBytes("tampered");

        var signature = scheme.SignDynamic(original, keyPair.PrivateKey);
        var valid = scheme.VerifyDynamic(tampered, signature, keyPair.PublicKey);

        Assert.False(valid);
    }

    /// <summary>Ensures verification fails when using the wrong public key.</summary>
    public static void Verify_ShouldFail_WithWrongPublicKey(string schemeName)
    {
        var scheme = GetScheme(schemeName);
        var keyPair1 = scheme.GenerateKeysDynamic();
        var keyPair2 = scheme.GenerateKeysDynamic();

        var message = Encoding.UTF8.GetBytes("data");
        var signature = scheme.SignDynamic(message, keyPair1.PrivateKey);

        var valid = scheme.VerifyDynamic(message, signature, keyPair2.PublicKey);
        Assert.False(valid);
    }

    /// <summary>Ensures verification fails with a corrupted signature.</summary>
    public static void Verify_ShouldFail_WithCorruptedSignature(string schemeName)
    {
        var scheme = GetScheme(schemeName);
        var keyPair = scheme.GenerateKeysDynamic();
        var message = Encoding.UTF8.GetBytes("authentic");

        var signature = scheme.SignDynamic(message, keyPair.PrivateKey);
        signature[0] ^= 0xFF; // corrupt one byte

        var valid = scheme.VerifyDynamic(message, signature, keyPair.PublicKey);
        Assert.False(valid);
    }

    /// <summary>Gets a dynamically selected scheme by name.</summary>
    private static ISignatureSchemeDynamic GetScheme(string schemeName)
    {
        var selector = new SignatureSchemeSelector();
        return (ISignatureSchemeDynamic)selector.GetRawScheme(schemeName);
    }
}
