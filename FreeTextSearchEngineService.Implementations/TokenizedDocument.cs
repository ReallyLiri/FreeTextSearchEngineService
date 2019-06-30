using System.Collections.Generic;
using FreeTextSearchEngineService.Interfaces;

namespace FreeTextSearchEngineService.Implementations
{
    public class TokenizedDocument : ITokenizedDocument
    {
        public TokenizedDocument(string document, ICollection<string> tokens)
        {
            Document = document;
            Tokens = tokens;
        }

        public string Document { get; }
        public ICollection<string> Tokens { get; }
    }
}
