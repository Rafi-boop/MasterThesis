using Xunit;

/// <summary>
/// Unit tests to run the standard signature scheme tests against all registered algorithms.
/// </summary>
public class AllSignatureSchemesTests
{
    /// <summary>Tests the Falcon signature scheme.</summary>
    [Fact]
    public void TestFalconScheme() => SignatureSchemeTests.RunTests("falcon");

    /// <summary>Tests the EdDSA (Ed25519) signature scheme.</summary>
    [Fact]
    public void TestEdDsaScheme() => SignatureSchemeTests.RunTests("eddsa");

    /// <summary>Tests the RSA signature scheme.</summary>
    [Fact]
    public void TestRsaScheme() => SignatureSchemeTests.RunTests("rsa");

    /// <summary>Tests the ECDSA signature scheme.</summary>
    [Fact]
    public void TestEcdsaScheme() => SignatureSchemeTests.RunTests("ecdsa");

    /// <summary>Tests the CRYSTALS-Dilithium signature scheme.</summary>
    [Fact]
    public void TestDilithiumScheme() => SignatureSchemeTests.RunTests("dilithium");

    /// <summary>Tests the SPHINCS+ signature scheme.</summary>
    [Fact]
    public void TestSphincsScheme() => SignatureSchemeTests.RunTests("sphincs");
}
