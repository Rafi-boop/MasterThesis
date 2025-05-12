using MasterThesis.DTOs;

public interface ISignatureSchemeDynamic : ISignatureSchemeBase
{
    KeyPairResponse GenerateKeysDynamic();
    byte[] SignDynamic(byte[] message, string privateKeyBase64);
    bool VerifyDynamic(byte[] message, byte[] signature, string publicKeyBase64);
}
