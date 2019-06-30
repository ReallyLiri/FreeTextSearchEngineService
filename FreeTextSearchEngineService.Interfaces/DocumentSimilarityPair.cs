namespace FreeTextSearchEngineService.Interfaces
{
    public class DocumentSimilarityPair
    {
        public DocumentSimilarityPair(string document, uint similarity)
        {
            Document = document;
            Similarity = similarity;
        }

        public string Document { get; }
        public uint Similarity { get; }
    }
}
