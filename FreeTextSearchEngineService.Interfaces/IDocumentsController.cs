using System;
using System.Collections.Generic;

namespace FreeTextSearchEngineService.Interfaces
{
    public interface IDocumentsController
    {
        void Index(string document);
        List<DocumentSimilarityPair> Query(string document);
    }
}
