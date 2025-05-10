## `TODO.md` — MasterThesis Crypto API Framework

### ✅ Core Implementation (Must Have)

* [ ] **Implement ECDSA**

  * [x] `EcdsaSignatureScheme.cs`
  * [x] `EcdsaPrivateKey.cs`
  * [x] `EcdsaPublicKey.cs`
  * [ ] Add to `SignatureSchemeSelector.cs`
  * [ ] Test via controller (`/api/signature/ecdsa/...`)

* [ ] **Implement EdDSA (Ed25519)**

  * [ ] Add `NSec.Cryptography` NuGet package
  * [ ] `Ed25519SignatureScheme.cs`
  * [ ] `Ed25519PrivateKey.cs`, `Ed25519PublicKey.cs`
  * [ ] Register in `SignatureSchemeSelector.cs`

* [ ] **Implement Dilithium (Post-Quantum)**

  * [ ] Integrate C reference via `DllImport`
  * [ ] Wrap as `DilithiumSignatureScheme`, `DilithiumPublicKey`, `DilithiumPrivateKey`
  * [ ] Add to selector

* [ ] **Implement SPHINCS+ (Post-Quantum)**

  * [ ] Compile C reference into DLL
  * [ ] Add signature scheme + key wrappers
  * [ ] Add to selector

* [ ] **Implement Falcon (Post-Quantum)**

  * [ ] Compile and wrap native code (watch for float rounding issues)
  * [ ] Add Falcon scheme + key types
  * [ ] Register

---

### 🧠 API + Controller Enhancements

* [ ] Add `/api/signature/{scheme}/generate` endpoint (✅ ECDSA, RSA – replicate for others)
* [ ] Add structured error responses (e.g., invalid base64, unsupported algorithm)
* [ ] Add `ListAvailableSchemes()` endpoint for introspection
* [ ] Add Swagger UI for REST API docs
* [ ] Secure private key exposure in logs/responses (maybe hide or mask?)

---

### 🔐 Security Hardening

* [ ] Auto-zero private key buffers after signing
* [ ] Add input validation for key lengths
* [ ] Consider timing-attack-resistant compare for signature verification
* [ ] Optional: integrate `SecureString` or native memory protection for private key storage

---

### 📈 Evaluation Prep (for Thesis Write-up)

* [ ] Add benchmark runner: time key generation, signing, and verification for each scheme
* [ ] Capture average time over N iterations and log
* [ ] Output benchmark as table or JSON (for thesis appendix)

---

### 🧪 Tests

* [ ] Add xUnit test project: `CryptoApiFramework.Tests`
* [ ] Write shared tests for `ISignatureScheme<TPub, TPriv>`:

  * Generate → Sign → Verify → Check validity
* [ ] Add failure tests (invalid signature, wrong key)

---

### 📚 Developer UX / Docs

* [ ] Improve `README.md` with curl/Postman examples for all schemes
* [ ] Add CLI interface for signing/verifying files (optional)
* [ ] Document how to add a new scheme (developer guide)
* [ ] Add `.editorconfig` and consistent code style

---

### 🔮 Stretch / Future Ideas

* [ ] Add async support to API methods
* [ ] Add file upload support for signing/verifying binary files via API
* [ ] Implement HSM-backed or TPM-backed key storage (Windows-specific)
* [ ] Add JWT signing/verification using these schemes
* [ ] Add WebAssembly target for client-side signature verification

---
