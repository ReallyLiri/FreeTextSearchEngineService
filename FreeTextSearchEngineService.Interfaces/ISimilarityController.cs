using System;
using System.Collections.Generic;

namespace FreeTextSearchEngineService.Interfaces
{
    public interface ISimilarityController
    {
        void PushTokenizedDocument(ITokenizedDocument tokenized);
        List<DocumentSimilarityPair> SimilarityRanks(ITokenizedDocument queried);
    }
}
