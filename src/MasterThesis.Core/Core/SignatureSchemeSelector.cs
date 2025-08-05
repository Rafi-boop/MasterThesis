using System;
using System.Collections.Generic;

/// <summary>
/// Provides a registry and runtime selection mechanism for available signature schemes.
/// </summary>
public class SignatureSchemeSelector : ISignatureSchemeSelector
{
    private readonly Dictionary<string, object> _schemes = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Initializes the selector and registers all supported signature schemes.
    /// </summary>
    public SignatureSchemeSelector()
    {
        RegisterScheme("rsa", RsaConfig.Config);
        RegisterScheme("ecdsa", EcdsaConfig.Config);
        RegisterScheme("eddsa", EdDsaConfig.Config);
        RegisterScheme("dilithium", DilithiumConfig.Config);
        RegisterScheme("sphincs", SphincsConfig.Config);
        RegisterScheme("falcon", FalconConfig.Config);
    }

    /// <summary>
    /// Registers a signature scheme in the internal dictionary.
    /// </summary>
    /// <param name="name">The name of the scheme (e.g., "rsa").</param>
    /// <param name="config">The scheme configuration.</param>
    private void RegisterScheme(string name, SignatureSchemeConfig config)
    {
        _schemes[name] = new SignatureSchemeAdapter<GenericPublicKey, GenericPrivateKey>(
            new GenericSignatureScheme(config),
            config.PublicKeySize,
            config.PrivateKeySize
        );
    }

    /// <inheritdoc/>
    public object GetRawScheme(string name)
    {
        if (_schemes.TryGetValue(name, out var scheme))
            return scheme;

        throw new ArgumentException($"Scheme '{name}' is not registered.");
    }

    /// <inheritdoc/>
    public IEnumerable<string> ListAvailableSchemes() => _schemes.Keys;
}
