using System;

/// <summary>
/// Generic implementation of a digital signature scheme using a provided configuration.
/// </summary>
public class GenericSignatureScheme : ISignatureScheme<GenericPublicKey, GenericPrivateKey>
{
    private readonly SignatureSchemeConfig _config;

    /// <summary>
    /// Initializes a new instance of the <see cref="GenericSignatureScheme"/> class.
    /// </summary>
    /// <param name="config">The signature scheme configuration.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="config"/> is null.</exception>
    public GenericSignatureScheme(SignatureSchemeConfig config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    /// <inheritdoc/>
    public string Name => _config.Name;

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public byte[] Sign(byte[] message, GenericPrivateKey privateKey)
    {
        var signature = new byte[_config.SignatureSize];
        ulong sigLen = (ulong)_config.SignatureSize;

        if (_config.SignFunc(signature, ref sigLen, message, (ulong)message.Length, privateKey.Export()) != 0)
            throw new Exception($"{_config.Name} signing failed.");

        Array.Resize(ref signature, (int)sigLen);
        return signature;
    }

    /// <inheritdoc/>
    public bool Verify(byte[] message, byte[] signature, GenericPublicKey publicKey)
    {
        return _config.VerifyFunc(signature, (ulong)signature.Length, message, (ulong)message.Length, publicKey.Export()) == 0;
    }
}
