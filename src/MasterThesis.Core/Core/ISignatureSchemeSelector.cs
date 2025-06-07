// Interface for runtime scheme selection
public interface ISignatureSchemeSelector
{
    object GetRawScheme(string name); // for runtime lookup
    IEnumerable<string> ListAvailableSchemes();

}
