using System.Text;

namespace MasterThesis
{
    class Program
    {
        static void Main()
        {
            try
            {
                var selector = new SignatureSchemeSelector();

                var rawScheme = selector.GetRawScheme("rsa");

                if (rawScheme is ISignatureScheme<RsaPublicKey, RsaPrivateKey> scheme)
                {
                    Console.WriteLine($"Using scheme: {scheme.Name}");

                    var (pub, priv) = scheme.GenerateKeys();
                    var message = Encoding.UTF8.GetBytes("Factory pattern is cool!");

                    var signature = scheme.Sign(message, priv);
                    bool isValid = scheme.Verify(message, signature, pub);

                    Console.WriteLine($"Signature valid: {isValid}");
                }
                else
                {
                    Console.WriteLine("Selected scheme is not compatible with expected types.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
