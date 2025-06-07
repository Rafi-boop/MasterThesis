using MasterThesis.Core.DTOs;

// Extension of the base interface to offer a dynamic, runtime-friendly API
public interface ISignatureSchemeDynamic : ISignatureSchemeBase
{
    KeyPairResponse GenerateKeysDynamic();
    byte[] SignDynamic(byte[] message, string privateKeyBase64);
    bool VerifyDynamic(byte[] message, byte[] signature, string publicKeyBase64);
}
