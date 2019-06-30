using FreeTextSearchEngineService.Interfaces;

namespace FreeTextSearchEngineService.Implementations
{
    public class BuiltInDocumentHasher : IDocumentHasher
    {
        public int GetHash(string document)
        {
            return document.GetHashCode();
        }
    }
}
