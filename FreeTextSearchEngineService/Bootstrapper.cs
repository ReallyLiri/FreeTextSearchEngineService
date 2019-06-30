using FreeTextSearchEngineService.Implementations;
using FreeTextSearchEngineService.Implementations.Similarity;
using FreeTextSearchEngineService.Implementations.Tokenization;
using FreeTextSearchEngineService.Interfaces;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using NLog;

namespace FreeTextSearchEngineService.HttpServer
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private readonly ILogger _logger;

        public Bootstrapper(ILogger logger)
        {
            _logger = logger;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            container.Register(_logger);
            container.Register(typeof(IDocumentHasher), typeof(BuiltInDocumentHasher)).AsSingleton();
            container.Register(typeof(IDocumentsController), typeof(DocumentsController)).AsSingleton();
            container.Register(typeof(ITokenizedDocument), typeof(TokenizedDocument)).AsSingleton();

            RegisterTokenizationController(container);
            RegisterSimilarityController(container);
        }

        private void RegisterTokenizationController(TinyIoCContainer container)
        {
            //container.Register(typeof(ITokenizationController), typeof(SpaceSplittingTokenizationController));
            //container.Register(typeof(ITokenizationController), typeof(PrefixSplittingTokenizationController));
            container.Register(typeof(ITokenizationController), typeof(TwoWordTuplesTokenizationController));
        }

        private void RegisterSimilarityController(TinyIoCContainer container)
        {
            container.Register(typeof(ISimilarityController), typeof(TokensOccurrencesSimilarityController));
            //container.Register(typeof(ISimilarityController), typeof(SimpleJaccardSimilarityController));
        }
    }
}
