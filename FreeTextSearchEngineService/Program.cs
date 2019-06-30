using System;
using FreeTextSearchEngineService.HttpServer.Properties;
using Nancy.Hosting.Self;
using NLog;

namespace FreeTextSearchEngineService.HttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                Console.Title = "Free Text Search Engine Service";

                var baseUri = new Uri($"http://localhost:{Settings.Default.Port}");
                var hostConfiguration= new HostConfiguration {RewriteLocalhost = true};
                logger.Info($"Starting service at '{baseUri}'");
                var nancyHost = new NancyHost(baseUri, new Bootstrapper(logger), hostConfiguration);
                nancyHost.Start();

                Console.WriteLine("Web server running... press any key to abort");
                Console.ReadKey();

                nancyHost.Stop();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Service unhandled exception");
                throw;
            }
        }
    }
}
