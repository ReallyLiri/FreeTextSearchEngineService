using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FreeTextSearchEngineService.Interfaces;

namespace FreeTextSearchEngineService.Implementations.Similarity
{
    // Implementation notes:
    // * if a token repeats itself in any document, we currently ignore its recurrences
    // * tokens are case sensitive
    public class TokensOccurrencesSimilarityController : ISimilarityController
    {
        private readonly IDocumentHasher _hasher;
        private readonly ConcurrentDictionary<int, string> _documentsByHash = new ConcurrentDictionary<int, string>();
        private readonly ConcurrentDictionary<string, HashSet<int>> _tokensToHashes = new ConcurrentDictionary<string, HashSet<int>>();

        public TokensOccurrencesSimilarityController(IDocumentHasher hasher)
        {
            _hasher = hasher;
        }

        public void PushTokenizedDocument(ITokenizedDocument tokenized)
        {
            var documentHash = _hasher.GetHash(tokenized.Document);
            _documentsByHash[documentHash] = tokenized.Document;

            foreach (var token in tokenized.Tokens.Distinct())
            {
                _tokensToHashes.AddOrUpdate(token, 
                    addValueFactory: t => new HashSet<int> {documentHash},
                    updateValueFactory: (t, existingHashes) =>
                    {
                        existingHashes.Add(documentHash);
                        return existingHashes;
                    });
            }
        }

        public List<DocumentSimilarityPair> SimilarityRanks(ITokenizedDocument queried)
        {
            var documentsAndHits = new ConcurrentDictionary<string, uint>();
            foreach (var token in queried.Tokens.Distinct())
            {
                HashSet<int> matchingHashes;
                if (!_tokensToHashes.TryGetValue(token, out matchingHashes))
                {
                    continue;
                }
                foreach (var matchingHash in matchingHashes)
                {
                    var document = _documentsByHash[matchingHash];
                    documentsAndHits.AddOrUpdate(document, doc => 1, (doc, count) => count + 1);
                }
            }
            var result = documentsAndHits
                .Select(pair => new DocumentSimilarityPair(pair.Key, pair.Value))
                .OrderByDescending(tuple => tuple.Similarity).ToList();
            return result;
        }
    }
}
