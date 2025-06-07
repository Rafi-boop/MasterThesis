namespace MasterThesis.WebAPI.DTOs;

// Request object for the signing endpoint
public class SignRequest
{
    public string Message { get; set; } = string.Empty;       // Base64 encoded message
    public string PrivateKey { get; set; } = string.Empty;    // Base64 encoded private key
}
