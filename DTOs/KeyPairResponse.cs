namespace MasterThesis.DTOs;

// Response object for key pair generation requests
public class KeyPairResponse
{
    public string PublicKey { get; set; } = string.Empty;
    public string PrivateKey { get; set; } = string.Empty;
    public string Algorithm { get; set; } = string.Empty;
}
