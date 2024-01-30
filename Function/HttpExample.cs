using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionVolumeTest.Function
{
    public class IsAlive
    {
        private readonly ILogger _logger;

        public IsAlive(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<IsAlive>();
        }

        [Function("IsAlive")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            var name = req.Query["name"];
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString($"Welcome to Azure Functions, {name}!");
            return response;
        }
    }
}
