namespace FreeTextSearchEngineService.Interfaces
{
    public interface ITokenizationController
    {
        ITokenizedDocument Tokenize(string document);
    }
}
