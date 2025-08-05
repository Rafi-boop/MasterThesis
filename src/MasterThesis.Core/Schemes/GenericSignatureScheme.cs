using System;

public class GenericSignatureScheme : ISignatureScheme<GenericPublicKey, GenericPrivateKey>
{
    private readonly SignatureSchemeConfig _config;

    public GenericSignatureScheme(SignatureSchemeConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public string Name => _config.Name;

    public (GenericPublicKey, GenericPrivateKey) GenerateKeys()
    {
        var pk = new byte[_config.PublicKeySize];
        var sk = new byte[_config.PrivateKeySize];

        if (_config.KeypairFunc(pk, sk) != 0)
            throw new Exception($"{_config.Name} keypair generation failed.");

        return (
            new GenericPublicKey(pk, _config.PublicKeySize),
            new GenericPrivateKey(sk, _config.PrivateKeySize)
        );
    }

    public byte[] Sign(byte[] message, GenericPrivateKey privateKey)
    {
        var signature = new byte[_config.SignatureSize];
        ulong sigLen = (ulong)_config.SignatureSize;

        if (_config.SignFunc(signature, ref sigLen, message, (ulong)message.Length, privateKey.Export()) != 0)
            throw new Exception($"{_config.Name} signing failed.");

        Array.Resize(ref signature, (int)sigLen);
        return signature;
    }

    public bool Verify(byte[] message, byte[] signature, GenericPublicKey publicKey)
    {
        return _config.VerifyFunc(signature, (ulong)signature.Length, message, (ulong)message.Length, publicKey.Export()) == 0;
    }
}
