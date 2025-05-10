public interface IPublicKey
{
    byte[] Export();
    string ToBase64();
}
