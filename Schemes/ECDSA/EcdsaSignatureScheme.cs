using System.Security.Cryptography;

public class EcdsaSignatureScheme : ISignatureScheme<EcdsaPublicKey, EcdsaPrivateKey>
{
    public string Name => "ECDSA";

    public (EcdsaPublicKey, EcdsaPrivateKey) GenerateKeys()
    {
        using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        return (
            new EcdsaPublicKey(ecdsa.ExportSubjectPublicKeyInfo()),
            new EcdsaPrivateKey(ecdsa.ExportPkcs8PrivateKey())
        );
    }

    public byte[] Sign(byte[] message, EcdsaPrivateKey privateKey)
    {
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportPkcs8PrivateKey(privateKey.Export(), out _);
        return ecdsa.SignData(message, HashAlgorithmName.SHA256);
    }

    public bool Verify(byte[] message, byte[] signature, EcdsaPublicKey publicKey)
    {
        using var ecdsa = ECDsa.Create();
        ecdsa.ImportSubjectPublicKeyInfo(publicKey.Export(), out _);
        return ecdsa.VerifyData(message, signature, HashAlgorithmName.SHA256);
    }
}
