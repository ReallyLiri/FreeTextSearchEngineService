using System.Collections.Generic;
using FreeTextSearchEngineService.Interfaces;

namespace FreeTextSearchEngineService.Implementations.Tokenization
{
    public class PrefixSplittingTokenizationController : ITokenizationController
    {
        public ITokenizedDocument Tokenize(string document)
        {
            var prefixes = new List<string> {document};
            var currentPrefix = document;
            while (true)
            {
                var lastSpace = currentPrefix.LastIndexOf(' ');
                if (lastSpace < 0)
                {
                    break;
                }
                currentPrefix = currentPrefix.Substring(0, lastSpace);
                prefixes.Add(currentPrefix);
            }
            return new TokenizedDocument(document, prefixes);
        }
    }
}
