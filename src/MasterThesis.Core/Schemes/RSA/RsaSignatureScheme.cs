using System.Security.Cryptography;

public class RsaSignatureScheme : ISignatureScheme<RsaPublicKey, RsaPrivateKey>
{
    public string Name => "RSA";

    public (RsaPublicKey, RsaPrivateKey) GenerateKeys()
    {
        using var rsa = RSA.Create(4096);
        return (
            new RsaPublicKey(rsa.ExportRSAPublicKey()),
            new RsaPrivateKey(rsa.ExportRSAPrivateKey())
        );
    }

    public byte[] Sign(byte[] message, RsaPrivateKey privateKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(privateKey.Export(), out _);
        return rsa.SignData(message, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
    }

    public bool Verify(byte[] message, byte[] signature, RsaPublicKey publicKey)
    {
        using var rsa = RSA.Create();
        rsa.ImportRSAPublicKey(publicKey.Export(), out _);
        return rsa.VerifyData(message, signature, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1);
    }
}
