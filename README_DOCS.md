# API Documentation Generation

This repository is configured to generate **full API documentation** (HTML + PDF) from XML comments in:
- Core library
- Web API project
- Unit tests

## 1. Prerequisites
Install **DocFX** globally:
```bash
dotnet tool install -g docfx
````

Check version:

```bash
docfx --version
```

## 2. Generate Metadata

From the root of the repository:

```bash
docfx metadata
```

## 3. Build HTML Docs

```bash
docfx build
```

* Output will be in the `docs/` folder.
* Open `docs/index.html` in a browser.

## 4. Generate PDF Docs

```bash
docfx pdf
```
