using MasterThesis.Core.DTOs;
using System;

/// <summary>
/// Adapts a strongly-typed signature scheme to a dynamic, Base64-friendly interface.
/// </summary>
/// <typeparam name="TPub">The public key type.</typeparam>
/// <typeparam name="TPriv">The private key type.</typeparam>
public class SignatureSchemeAdapter<TPub, TPriv> : ISignatureSchemeDynamic
    where TPub : IPublicKey
    where TPriv : IPrivateKey
{
    private readonly ISignatureScheme<TPub, TPriv> _impl;
    private readonly int _publicKeyLength;
    private readonly int _privateKeyLength;

    /// <summary>
    /// Initializes a new adapter for the specified signature scheme.
    /// </summary>
    /// <param name="impl">The underlying strongly-typed scheme implementation.</param>
    /// <param name="publicKeyLength">The expected public key length in bytes.</param>
    /// <param name="privateKeyLength">The expected private key length in bytes.</param>
    public SignatureSchemeAdapter(ISignatureScheme<TPub, TPriv> impl, int publicKeyLength, int privateKeyLength)
    {
        _impl = impl;
        _publicKeyLength = publicKeyLength;
        _privateKeyLength = privateKeyLength;
    }

    /// <inheritdoc/>
    public string Name => _impl.Name;

    /// <inheritdoc/>
    public KeyPairResponse GenerateKeysDynamic()
    {
        var (pub, priv) = _impl.GenerateKeys();
        return new KeyPairResponse
        {
            Algorithm = _impl.Name,
            PublicKey = pub.ToBase64(),
            PrivateKey = priv.ToBase64()
        };
    }

    /// <inheritdoc/>
    public byte[] SignDynamic(byte[] message, string privateKeyBase64)
    {
        var privBytes = Convert.FromBase64String(privateKeyBase64);
        using var priv = (TPriv)Activator.CreateInstance(typeof(TPriv), privBytes, _privateKeyLength)!;
        return _impl.Sign(message, priv);
    }

    /// <inheritdoc/>
    public bool VerifyDynamic(byte[] message, byte[] signature, string publicKeyBase64)
    {
        var pubBytes = Convert.FromBase64String(publicKeyBase64);
        var pub = (TPub)Activator.CreateInstance(typeof(TPub), pubBytes, _publicKeyLength)!;
        return _impl.Verify(message, signature, pub);
    }
}
