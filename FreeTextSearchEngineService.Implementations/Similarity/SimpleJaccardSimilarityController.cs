using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeTextSearchEngineService.Interfaces;

namespace FreeTextSearchEngineService.Implementations.Similarity
{
    public class SimpleJaccardSimilarityController : ISimilarityController
    {
        private readonly ConcurrentDictionary<string, HashSet<string>> _documentsToTokensSet = new ConcurrentDictionary<string, HashSet<string>>();

        public void PushTokenizedDocument(ITokenizedDocument tokenized)
        {
            _documentsToTokensSet[tokenized.Document] = new HashSet<string>(tokenized.Tokens);
        }

        public List<DocumentSimilarityPair> SimilarityRanks(ITokenizedDocument queried)
        {
            var queriedSet = new HashSet<string>(queried.Tokens);
            var ranks = new Dictionary<string, uint>();
            foreach (var pair in _documentsToTokensSet)
            {
                var currentSet = pair.Value;
                var intersectionSet = new HashSet<string>(currentSet);
                intersectionSet.IntersectWith(queriedSet);
                var unionSet = new HashSet<string>(currentSet);
                unionSet.UnionWith(queriedSet);
                var rank = (uint)(100.0 * intersectionSet.Count / unionSet.Count); // not perfect percision but good enough for now
                if (rank > 0)
                {
                    ranks[pair.Key] = rank;
                }
            }
            var result =  ranks
                .Select(pair => new DocumentSimilarityPair(pair.Key, pair.Value))
                .OrderByDescending(pair => pair.Similarity).ToList();
            return result;
        }
    }
}
