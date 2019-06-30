using System;
using System.Collections.Generic;
using FreeTextSearchEngineService.Interfaces;
using NLog;

namespace FreeTextSearchEngineService.Implementations
{
    public class DocumentsController : IDocumentsController
    {
        private readonly ILogger _logger;
        private readonly ISimilarityController _similarityController;
        private readonly ITokenizationController _tokenizationController;

        public DocumentsController(ILogger logger, ISimilarityController similarityController, ITokenizationController tokenizationController)
        {
            _logger = logger;
            _similarityController = similarityController;
            _tokenizationController = tokenizationController;
        }

        public void Index(string document)
        {
            _logger.Debug($"Indexing document: '{document}'");
            var tokenizedDocument = _tokenizationController.Tokenize(document);
            _similarityController.PushTokenizedDocument(tokenizedDocument);
            _logger.Debug($"Indexed document using {tokenizedDocument.Tokens.Count} tokens: '{document}'");
        }

        public List<DocumentSimilarityPair> Query(string document)
        {
            _logger.Debug($"Querying document: '{document}'");
            var tokenizedDocument = _tokenizationController.Tokenize(document);
            var similarityRanks = _similarityController.SimilarityRanks(tokenizedDocument);
            _logger.Debug($"Queried document: '{document}', found {similarityRanks.Count} ranks");
            return similarityRanks;
        }
    }
}
