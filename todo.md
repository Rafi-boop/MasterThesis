## `TODO.md` — MasterThesis Crypto API Framework

### Core Implementation (Must Have)

* [x] **Implement ECDSA**
  * [x] `EcdsaSignatureScheme.cs`
  * [x] `EcdsaPrivateKey.cs`
  * [x] `EcdsaPublicKey.cs`
  * [x] Add to `SignatureSchemeSelector.cs`
  * [x] Test via controller (`/api/signature/ecdsa/...`)

* [x] **Implement EdDSA (Ed25519)**
  * [x] Use native `libsodium` via `DllImport` (instead of NSec)
  * [x] `EdDsaSignatureScheme.cs`
  * [x] `EdDsaPrivateKey.cs`, `EdDsaPublicKey.cs`
  * [x] Register in `SignatureSchemeSelector.cs`
  * [x] Expose via controller `/api/signature/eddsa/...`
  * [x] Add secure memory zeroing, input length checks, and pinning
  * [x] Add dynamic adapter for controller support

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

### API + Controller Enhancements

* [x] Add `/api/signature/{scheme}/generate` endpoint ([x]ECDSA, [x]RSA, [x]EdDSA)
* [ ] Add structured error responses (e.g., invalid base64, unsupported algorithm)
* [x] Add `ListAvailableSchemes()` endpoint for introspection
* [ ] Add Swagger UI for REST API docs
* [ ] Secure private key exposure in logs/responses (maybe hide or mask?)

---

### Security Hardening

* [x] Auto-zero private key buffers after signing (EdDSA implemented)
* [x] Add input validation for key lengths (EdDSA implemented)
* [x] Consider timing-attack-resistant compare for signature verification (libsodium does this internally)
* [ ] Optional: integrate `SecureString` or native memory protection for private key storage

---

### Evaluation Prep (for Thesis Write-up)

* [ ] Add benchmark runner: time key generation, signing, and verification for each scheme
* [ ] Capture average time over N iterations and log
* [ ] Output benchmark as table or JSON (for thesis appendix)

---

### Tests

* [x] Add xUnit test project: `CryptoApiFramework.Tests`
* [x] Write shared tests for `ISignatureScheme<TPub, TPriv>`:
  * [x] Generate → Sign → Verify → Check validity
  * [x] EdDSA: verify correct/incorrect messages, corrupted sig, wrong keys
* [ ] Add failure tests for other schemes (ECDSA, RSA)
* [ ] Add fuzzing / edge-case tests

---

### Developer UX / Docs

* [ ] Improve `README.md` with curl/Postman/C# examples for all schemes
* [ ] Add CLI interface for signing/verifying files (optional)
* [ ] Document how to add a new scheme (developer guide)
* [ ] Add `.editorconfig` and consistent code style

---

### Stretch / Future Ideas

* [ ] Add async support to API methods
* [ ] Add file upload support for signing/verifying binary files via API
* [ ] Implement HSM-backed or TPM-backed key storage (Windows-specific)
* [ ] Add JWT signing/verification using these schemes
* [ ] Add WebAssembly target for client-side signature verification

---
