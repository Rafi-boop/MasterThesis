using System;
using System.Collections.Generic;

public class SignatureSchemeSelector : ISignatureSchemeSelector
{
    private readonly Dictionary<string, object> _schemes = new(StringComparer.OrdinalIgnoreCase);

    public SignatureSchemeSelector()
    {
        // Register supported schemes
        _schemes["rsa"] = new RsaSignatureScheme();
        _schemes["ecdsa"] = new EcdsaSignatureScheme();
        // _schemes["dilithium"] = new DilithiumSignatureScheme();
        // _schemes["sphincs"] = new SphincsSignatureScheme();
    }

    public object GetRawScheme(string name)
    {
        if (_schemes.TryGetValue(name, out var scheme))
            return scheme;

        throw new ArgumentException($"Scheme '{name}' is not registered.");
    }

    public IEnumerable<string> ListAvailableSchemes() => _schemes.Keys;
}
