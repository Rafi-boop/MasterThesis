# TODO

## 1. Code Structure and Refactoring
- [ ] Improve code structure and refactor existing classes, interfaces, and controllers.
- [ ] Enhance modularity, clarity, and maintainability of the framework.
- [ ] Remove duplicate logic in runtime scheme selection and adapters.

## 2. Security Review and Hardening
- [ ] Perform a thorough security review of the entire codebase.
- [ ] Check for potential security vulnerabilities in scheme implementations (e.g., proper zeroization of private keys, buffer size checks, constant-time operations).
- [ ] Ensure secure handling of sensitive data (e.g., private keys in memory and logs).
- [ ] Mitigate risk of exposing private keys at runtime (e.g., avoid logging them, ensure secure export/serialization).

## 3. Testing Improvements
- [ ] Improve and extend integration tests for all signature schemes.
- [ ] Include edge cases, invalid inputs, and failure modes.
- [ ] Automate testing

## 4. Example Implementations and Documentation
- [ ] Provide clear and comprehensive example implementations:
  - [ ] Example of library import and usage for integrating the framework.
  - [ ] Example API usage for external consumers.
- [ ] Include proper documentation about:
  - [ ] Framework
  - [ ] How to use

## 5. API Packaging and Documentation
- [ ] Package the API for easy deployment (e.g. NuGet).
- [ ] Publish Swagger UI documentation for all API endpoints.

## 6. External Validation and Feedback
- [ ] Engage a third-party for independent testing of the API and cryptographic primitives.
- [ ] Validate API usability, correctness, and security with external testers.

## 7. Swagger UI Integration
- [ ] ? Integrate Swagger UI for all REST API endpoints for clear and interactive documentation.
- [ ] ? Include usage examples and expected payloads for each endpoint.

## 8. Secure Private Key Handling
- [ ] Review runtime handling of private keys to ensure no accidental exposure in logs or memory.
- [ ] Validate private key lifecycle management (e.g., zeroization, memory protection).
