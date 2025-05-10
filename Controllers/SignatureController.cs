using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/signature")]
public class SignatureController : ControllerBase
{
    private readonly ISignatureSchemeSelector _selector;

    public SignatureController(ISignatureSchemeSelector selector)
    {
        _selector = selector;
    }

    [HttpPost("{scheme}/sign")]
    public IActionResult Sign(string scheme, [FromBody] SignRequest request)
    {
        var algo = _selector.GetScheme(scheme);
        var signature = algo.Sign(Convert.FromBase64String(request.Message), Convert.FromBase64String(request.PrivateKey));
        return Ok(Convert.ToBase64String(signature));
    }

    [HttpPost("{scheme}/verify")]
    public IActionResult Verify(string scheme, [FromBody] VerifyRequest request)
    {
        var algo = _selector.GetScheme(scheme);
        bool valid = algo.Verify(
            Convert.FromBase64String(request.Message),
            Convert.FromBase64String(request.Signature),
            Convert.FromBase64String(request.PublicKey)
        );
        return Ok(new { valid });
    }
}

public class SignRequest
{
    public string Message { get; set; } = string.Empty;
    public string PrivateKey { get; set; } = string.Empty;
}

public class VerifyRequest
{
    public string Message { get; set; } = string.Empty;
    public string Signature { get; set; } = string.Empty;
    public string PublicKey { get; set; } = string.Empty;
}
