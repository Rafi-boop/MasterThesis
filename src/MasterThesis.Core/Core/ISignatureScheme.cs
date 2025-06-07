// Generic interface for any signature scheme
public interface ISignatureScheme<TPublicKey, TPrivateKey> : ISignatureSchemeBase
    where TPublicKey : IPublicKey
    where TPrivateKey : IPrivateKey
{
    (TPublicKey publicKey, TPrivateKey privateKey) GenerateKeys();

    byte[] Sign(byte[] message, TPrivateKey privateKey);

    bool Verify(byte[] message, byte[] signature, TPublicKey publicKey);
}
