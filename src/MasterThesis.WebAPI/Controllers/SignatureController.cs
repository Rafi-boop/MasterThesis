using Microsoft.AspNetCore.Mvc;
using MasterThesis.WebAPI.DTOs;
using MasterThesis.Core.DTOs;

namespace MasterThesis.Controllers;

/// <summary>
/// Provides HTTP endpoints for generating keys, signing messages, and verifying signatures.
/// </summary>
[ApiController]
[Route("api/signature")]
public class SignatureController : ControllerBase
{
    private readonly ISignatureSchemeSelector _selector;

    /// <summary>
    /// Initializes a new instance of the <see cref="SignatureController"/> class.
    /// </summary>
    public SignatureController(ISignatureSchemeSelector selector)
    {
        _selector = selector;
    }

    /// <summary>Generates a key pair for the given scheme.</summary>
    [HttpGet("{scheme}/generate")]
    public ActionResult<KeyPairResponse> GenerateKeyPair(string scheme)
    {
        if (_selector.GetRawScheme(scheme) is ISignatureSchemeDynamic schemeDyn)
            return Ok(schemeDyn.GenerateKeysDynamic());

        return BadRequest("Unsupported scheme.");
    }

    /// <summary>Signs a Base64-encoded message using the given scheme and private key.</summary>
    [HttpPost("{scheme}/sign")]
    public ActionResult<string> Sign(string scheme, [FromBody] SignRequest request)
    {
        if (_selector.GetRawScheme(scheme) is ISignatureSchemeDynamic schemeDyn)
        {
            var message = Convert.FromBase64String(request.Message);
            var signature = schemeDyn.SignDynamic(message, request.PrivateKey);
            return Ok(Convert.ToBase64String(signature));
        }

        return BadRequest("Unsupported scheme.");
    }

    /// <summary>Verifies a Base64-encoded signature for a given message and public key.</summary>
    [HttpPost("{scheme}/verify")]
    public ActionResult<object> Verify(string scheme, [FromBody] VerifyRequest request)
    {
        if (_selector.GetRawScheme(scheme) is ISignatureSchemeDynamic schemeDyn)
        {
            var message = Convert.FromBase64String(request.Message);
            var signature = Convert.FromBase64String(request.Signature);
            var valid = schemeDyn.VerifyDynamic(message, signature, request.PublicKey);
            return Ok(new { valid });
        }

        return BadRequest("Unsupported scheme.");
    }

    /// <summary>Lists all available signature schemes.</summary>
    [HttpGet("schemes")]
    public ActionResult<IEnumerable<string>> ListSchemes()
    {
        var schemes = _selector.ListAvailableSchemes();
        return Ok(schemes);
    }
}
