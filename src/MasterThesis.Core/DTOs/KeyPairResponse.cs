namespace MasterThesis.Core.DTOs
{
    /// <summary>
    /// Represents a response containing a generated key pair.
    /// </summary>
    public class KeyPairResponse
    {
        /// <summary>
        /// Gets or sets the public key in Base64 format.
        /// </summary>
        public string PublicKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the private key in Base64 format.
        /// </summary>
        public string PrivateKey { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the signature scheme used to generate the keys.
        /// </summary>
        public string Algorithm { get; set; } = string.Empty;
    }
}
