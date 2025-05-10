public interface ISignatureSchemeSelector
{
    object GetRawScheme(string name); // for runtime lookup
}
