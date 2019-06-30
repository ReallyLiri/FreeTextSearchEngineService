using System.Collections.Generic;
using FreeTextSearchEngineService.Interfaces;

namespace FreeTextSearchEngineService.Implementations.Tokenization
{
    public class TwoWordTuplesTokenizationController : ITokenizationController
    {
        private const char Separator = ' ';

        public ITokenizedDocument Tokenize(string document)
        {
            var words = document.Split(Separator);
            var twoWordsTuples = new List<string>();
            for (var i = 0; i < words.Length - 1; i++)
            {
                twoWordsTuples.Add(words[i] + Separator + words[i + 1]);
            }
            return new TokenizedDocument(document, twoWordsTuples);
        }
    }
}
