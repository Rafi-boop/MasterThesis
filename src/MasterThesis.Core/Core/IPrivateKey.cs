// Interface for private key objects
public interface IPrivateKey : IDisposable
{
    byte[] Export();
    string ToBase64();
    void Zeroize();
}
