using FreeTextSearchEngineService.Interfaces;
using Nancy;
using Nancy.Extensions;

namespace FreeTextSearchEngineService.HttpServer
{
    public class MainModule : NancyModule
    {
        public MainModule(IDocumentsController documentsController)
        {
            Get["/isAlive"] = _ => true;
            Post["/index"] = _ =>
            {
                documentsController.Index(Request.Body.AsString());
                return "Ok";
            };
            Post["/search"] = _ => documentsController.Query(Request.Body.AsString());
        }
    }
}
