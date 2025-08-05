namespace MasterThesis.WebAPI.DTOs;

/// <summary>
/// Request object for the signature verification endpoint.
/// </summary>
public class VerifyRequest
{
    /// <summary>Gets or sets the Base64-encoded message to verify.</summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>Gets or sets the Base64-encoded signature to verify.</summary>
    public string Signature { get; set; } = string.Empty;

    /// <summary>Gets or sets the Base64-encoded public key used for verification.</summary>
    public string PublicKey { get; set; } = string.Empty;
}
