using System.Text;

class Program
{
    static void Main()
    {
        var scheme = SignatureSchemeFactory.Create("rsa");
        Console.WriteLine($"Using scheme: {scheme.Name}");

        var (pub, priv) = scheme.GenerateKeys();
        var message = Encoding.UTF8.GetBytes("Factory pattern is cool!");

        var signature = scheme.Sign(message, priv);
        bool isValid = scheme.Verify(message, signature, pub);

        Console.WriteLine($"Signature valid: {isValid}");
    }
}
