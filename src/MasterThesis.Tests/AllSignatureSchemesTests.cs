using Xunit;

public class AllSignatureSchemesTests
{
    [Fact]
    public void TestFalconScheme() => SignatureSchemeTests.RunTests("falcon");

    [Fact]
    public void TestEdDsaScheme() => SignatureSchemeTests.RunTests("eddsa");

    [Fact]
    public void TestRsaScheme() => SignatureSchemeTests.RunTests("rsa");

    [Fact]
    public void TestEcdsaScheme() => SignatureSchemeTests.RunTests("ecdsa");

    [Fact]
    public void TestDilithiumScheme() => SignatureSchemeTests.RunTests("dilithium");

    [Fact]
    public void TestSphincsScheme() => SignatureSchemeTests.RunTests("sphincs");
}
