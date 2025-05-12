using Microsoft.AspNetCore.Mvc;
using System.Text;
using MasterThesis.DTOs;

namespace MasterThesis.Controllers;

[ApiController]
[Route("api/signature")]
public class SignatureController : ControllerBase
{
    private readonly ISignatureSchemeSelector _selector;

    public SignatureController(ISignatureSchemeSelector selector)
    {
        _selector = selector;
    }

    [HttpGet("{scheme}/generate")]
    public ActionResult<KeyPairResponse> GenerateKeyPair(string scheme)
    {
        if (_selector.GetRawScheme(scheme) is ISignatureSchemeDynamic schemeDyn)
            return Ok(schemeDyn.GenerateKeysDynamic());

        return BadRequest("Unsupported scheme.");
    }

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
}
