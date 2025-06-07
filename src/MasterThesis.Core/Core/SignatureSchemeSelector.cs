using System;
using System.Collections.Generic;

// implementation of ISignatureSchemeSelector, registers supported signature schemes and resolves them dynamically at runtime
public class SignatureSchemeSelector : ISignatureSchemeSelector
{
    private readonly Dictionary<string, object> _schemes = new(StringComparer.OrdinalIgnoreCase);

    public SignatureSchemeSelector()
    {
        // Register supported schemes
        _schemes["rsa"] = new SignatureSchemeAdapter<RsaPublicKey, RsaPrivateKey>(new RsaSignatureScheme());
        _schemes["ecdsa"] = new SignatureSchemeAdapter<EdDsaPublicKey, EdDsaPrivateKey>(new EdDsaSignatureScheme());
        _schemes["eddsa"] = new SignatureSchemeAdapter<EdDsaPublicKey, EdDsaPrivateKey>(new EdDsaSignatureScheme());
        _schemes["dilithium"] = new SignatureSchemeAdapter<RsaPublicKey, RsaPrivateKey>(new RsaSignatureScheme());
        _schemes["sphincs"] = new SignatureSchemeAdapter<EdDsaPublicKey, EdDsaPrivateKey>(new EdDsaSignatureScheme());
        _schemes["falcon"] = new SignatureSchemeAdapter<EdDsaPublicKey, EdDsaPrivateKey>(new EdDsaSignatureScheme());
    }

    public object GetRawScheme(string name)
    {
        if (_schemes.TryGetValue(name, out var scheme))
            return scheme;

        throw new ArgumentException($"Scheme '{name}' is not registered.");
    }

    public IEnumerable<string> ListAvailableSchemes() => _schemes.Keys;
}
