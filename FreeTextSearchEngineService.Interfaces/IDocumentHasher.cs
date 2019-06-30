namespace FreeTextSearchEngineService.Interfaces
{
    public interface IDocumentHasher
    {
        int GetHash(string document);
    }
}
