using System;
using System.Collections.Generic;

public class SignatureSchemeSelector : ISignatureSchemeSelector
{
    private readonly Dictionary<string, object> _schemes = new(StringComparer.OrdinalIgnoreCase);

    public SignatureSchemeSelector()
    {
        // Register all supported schemes using GenericSignatureScheme + configs
        RegisterScheme("rsa", RsaConfig.Config);
        RegisterScheme("ecdsa", EcdsaConfig.Config);
        RegisterScheme("eddsa", EdDsaConfig.Config);
        RegisterScheme("dilithium", DilithiumConfig.Config);
        RegisterScheme("sphincs", SphincsConfig.Config);
        RegisterScheme("falcon", FalconConfig.Config);
    }

    private void RegisterScheme(string name, SignatureSchemeConfig config)
    {
        _schemes[name] = new SignatureSchemeAdapter<GenericPublicKey, GenericPrivateKey>(
            new GenericSignatureScheme(config),
            config.PublicKeySize,
            config.PrivateKeySize
        );
    }

    public object GetRawScheme(string name)
    {
        if (_schemes.TryGetValue(name, out var scheme))
            return scheme;

        throw new ArgumentException($"Scheme '{name}' is not registered.");
    }

    public IEnumerable<string> ListAvailableSchemes() => _schemes.Keys;
}
