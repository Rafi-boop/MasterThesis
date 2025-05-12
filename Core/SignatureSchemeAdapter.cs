using MasterThesis.DTOs;

public class SignatureSchemeAdapter<TPub, TPriv> : ISignatureSchemeDynamic
    where TPub : IPublicKey
    where TPriv : IPrivateKey
{
    private readonly ISignatureScheme<TPub, TPriv> _impl;

    public SignatureSchemeAdapter(ISignatureScheme<TPub, TPriv> impl)
    {
        _impl = impl;
    }

    public string Name => _impl.Name;

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

    public byte[] SignDynamic(byte[] message, string privateKeyBase64)
    {
        var privBytes = Convert.FromBase64String(privateKeyBase64);
        using var priv = (TPriv)Activator.CreateInstance(typeof(TPriv), privBytes)!;
        return _impl.Sign(message, priv);
    }

    public bool VerifyDynamic(byte[] message, byte[] signature, string publicKeyBase64)
    {
        var pubBytes = Convert.FromBase64String(publicKeyBase64);
        var pub = (TPub)Activator.CreateInstance(typeof(TPub), pubBytes)!;
        return _impl.Verify(message, signature, pub);
    }
}
