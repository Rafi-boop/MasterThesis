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
        var algo = _selector.GetRawScheme(scheme);

        if (algo is ISignatureScheme<RsaPublicKey, RsaPrivateKey> rsa)
        {
            var (pub, priv) = rsa.GenerateKeys();

            return Ok(new KeyPairResponse
            {
                Algorithm = rsa.Name,
                PublicKey = pub.ToBase64(),
                PrivateKey = priv.ToBase64()
            });
        }

        return BadRequest("Unsupported scheme.");
    }

    [HttpPost("{scheme}/sign")]
    public ActionResult<string> Sign(string scheme, [FromBody] SignRequest request)
    {
        var algo = _selector.GetRawScheme(scheme);

        if (algo is ISignatureScheme<RsaPublicKey, RsaPrivateKey> rsa)
        {
            var privateKey = new RsaPrivateKey(Convert.FromBase64String(request.PrivateKey));
            var message = Convert.FromBase64String(request.Message);

            var signature = rsa.Sign(message, privateKey);
            return Ok(Convert.ToBase64String(signature));
        }

        return BadRequest("Unsupported scheme.");
    }

    [HttpPost("{scheme}/verify")]
    public ActionResult<object> Verify(string scheme, [FromBody] VerifyRequest request)
    {
        var algo = _selector.GetRawScheme(scheme);

        if (algo is ISignatureScheme<RsaPublicKey, RsaPrivateKey> rsa)
        {
            var publicKey = new RsaPublicKey(Convert.FromBase64String(request.PublicKey));
            var message = Convert.FromBase64String(request.Message);
            var signature = Convert.FromBase64String(request.Signature);

            var isValid = rsa.Verify(message, signature, publicKey);
            return Ok(new { valid = isValid });
        }

        return BadRequest("Unsupported scheme.");
    }
}
