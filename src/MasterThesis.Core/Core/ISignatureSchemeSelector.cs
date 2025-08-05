/// <summary>
/// Provides runtime selection and retrieval of registered digital signature schemes.
/// </summary>
public interface ISignatureSchemeSelector
{
    /// <summary>
    /// Retrieves the raw scheme instance for a given algorithm name.
    /// </summary>
    /// <param name="name">The name of the signature scheme (e.g., "rsa", "ecdsa", "dilithium").</param>
    /// <returns>The scheme instance as an object.</returns>
    /// <exception cref="ArgumentException">Thrown if the scheme name is not registered.</exception>
    object GetRawScheme(string name);

    /// <summary>
    /// Lists the names of all available signature schemes.
    /// </summary>
    /// <returns>A collection of scheme names.</returns>
    IEnumerable<string> ListAvailableSchemes();
}
