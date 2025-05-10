namespace MasterThesis.DTOs;

public class VerifyRequest
{
    public string Message { get; set; } = string.Empty;       // Base64 encoded message
    public string Signature { get; set; } = string.Empty;     // Base64 encoded signature
    public string PublicKey { get; set; } = string.Empty;     // Base64 encoded public key
}
