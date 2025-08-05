namespace MasterThesis.WebAPI.DTOs;

/// <summary>
/// Request object for the signing endpoint.
/// </summary>
public class SignRequest
{
    /// <summary>Gets or sets the Base64-encoded message to sign.</summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>Gets or sets the Base64-encoded private key to use for signing.</summary>
    public string PrivateKey { get; set; } = string.Empty;
}
