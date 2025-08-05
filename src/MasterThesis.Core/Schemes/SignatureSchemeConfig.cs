using System;

public delegate int KeypairDelegate(byte[] publicKey, byte[] privateKey);
public delegate int SignDelegate(byte[] signature, ref ulong signatureLength, byte[] message, ulong messageLength, byte[] privateKey);
public delegate int VerifyDelegate(byte[] signature, ulong signatureLength, byte[] message, ulong messageLength, byte[] publicKey);

public record SignatureSchemeConfig(
    string Name,
    int PublicKeySize,
    int PrivateKeySize,
    int SignatureSize,
    KeypairDelegate KeypairFunc,
    SignDelegate SignFunc,
    VerifyDelegate VerifyFunc
);
