# MasterThesis - Cryptographic API Framework

A modular and extensible API framework for integrating **pre- and post-quantum digital signature schemes** with a focus on usability, type safety, and cryptographic hygiene.

## Project Structure

```

MasterThesis/
├── Core/                    # Generic interfaces and abstractions
│   ├── ISignatureScheme.cs
│   ├── IPublicKey.cs
│   ├── IPrivateKey.cs
│   ├── ISignatureSchemeSelector.cs
│   └── SignatureSchemeSelector.cs
├── Schemes/
│   └── RSA/                # RSA implementation
│       ├── RsaSignatureScheme.cs
│       ├── RsaPublicKey.cs
│       └── RsaPrivateKey.cs
├── Controllers/            # Web API controller (optional module)
│   └── SignatureController.cs
├── Program.cs              # App entry point
├── MasterThesis.csproj
├── MasterThesis.sln
├── README.md
└── .gitignore

````

## Features

- Generic cryptographic scheme abstraction
- Strongly typed key wrappers (`RsaPublicKey`, `RsaPrivateKey`)
- Support for DI and runtime scheme selection
- REST API controller for signing/verification
- Easily extensible with new schemes (e.g., Dilithium, SPHINCS+)