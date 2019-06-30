using FreeTextSearchEngineService.Interfaces;

namespace FreeTextSearchEngineService.Implementations.Tokenization
{
    public class SpaceSplittingTokenizationController : ITokenizationController
    {
        public ITokenizedDocument Tokenize(string document)
        {
            var splitted = document.Split(' ');
            return new TokenizedDocument(document, splitted);
        }
    }
}
