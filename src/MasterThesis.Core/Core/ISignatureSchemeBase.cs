/// <summary>
/// Represents the base contract for all signature scheme implementations.
/// </summary>
public interface ISignatureSchemeBase
{
    /// <summary>
    /// Gets the name of the signature scheme.
    /// </summary>
    string Name { get; }
}
