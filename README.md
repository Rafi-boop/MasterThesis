# Master Thesis: API Framework for Pre- and Post-Quantum Digital Signature Schemes

This repository contains the source code for the Master's thesis project **"Designing an API Framework for Simplified Integration of Pre- and Post-Quantum Digital Signature Schemes for Software Developers"**.

The solution consists of three main projects:

## Projects

### 1. MasterThesis.Core
- Implements a unified, strongly-typed interface for multiple signature schemes.
- Supports **classical** schemes:
  - RSA (SHA-512, PKCS#1 padding)
  - ECDSA (P-256, SHA-256)
  - EdDSA (Ed25519, libsodium)
- Supports **post-quantum** schemes (via native C interop):
  - CRYSTALS-Dilithium
  - Falcon
  - SPHINCS+
- Features:
  - Strongly-typed keys to prevent misuse.
  - Secure key memory handling.
  - Dynamic scheme selection via `ISignatureSchemeSelector`.

### 2. MasterThesis.WebAPI
- ASP.NET Core Web API exposing the cryptographic framework over HTTP.
- Endpoints:
  - `GET /api/signature/{scheme}/generate` — generate a keypair.
  - `POST /api/signature/{scheme}/sign` — sign a message.
  - `POST /api/signature/{scheme}/verify` — verify a signature.
  - `GET /api/signature/schemes` — list supported schemes.
- Accepts/returns Base64-encoded data for safe transport.

### 3. MasterThesis.Tests
- xUnit test suite verifying correctness of all signature schemes.
- Covers:
  - Key generation.
  - Valid signing and verification.
  - Verification failure cases (modified messages, wrong keys, corrupted signatures).
- Shared test logic via `SignatureSchemeTests` for consistency.

---

## 🛠 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Native libraries for post-quantum schemes in the `runtimes/` folder:
  - `dilithium.dll`
  - `falcon.dll`
  - `sphincs.dll`
  - `libsodium.dll`

---

## Building the Solution

From the repository root:
```bash
dotnet build
````

---

## Running the Web API

```bash
cd MasterThesis.WebAPI
dotnet run
```

Swagger UI will be available (in development mode) at:

```
http://localhost:5000/swagger
```

---

## Running Tests

From the repository root:

```bash
dotnet test
```

---

## Generating API Documentation

Full HTML + PDF API documentation can be generated using **DocFX**.

See [README\_DOCS.md](README_DOCS.md) for detailed instructions.

---

## Repository Structure

```
/MasterThesis.Core        # Core cryptographic framework
/MasterThesis.WebAPI      # ASP.NET Core Web API
/MasterThesis.Tests       # xUnit test project
/docfx.json               # DocFX configuration
/README_DOCS.md           # API documentation generation guide
```
