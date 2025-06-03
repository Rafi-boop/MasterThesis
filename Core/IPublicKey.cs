// interface for public key objects
public interface IPublicKey
{
    byte[] Export();
    string ToBase64();
}
