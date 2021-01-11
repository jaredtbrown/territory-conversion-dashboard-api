using System.Net;
using System.Threading.Tasks;
using Google.Cloud.Functions.Framework;
using Google.Cloud.Functions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace TerritoryConversionDashboard.Functions
{
    [FunctionsStartup(typeof(Startup))]
    public class ReportFunction : IHttpFunction
    {
        private readonly IReportService _reportService;
        private readonly ILogger _logger;

        public ReportFunction(IReportService reportService, ILogger logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        public async Task HandleAsync(HttpContext context)
        {
            try
            {
                var response = context.Response;
                response.Headers.Append("Access-Control-Allow-Origin", "*");
                switch (context.Request.Method)
                {
                    case "GET":
                        var report = await _reportService.GetAsync();
                        response.StatusCode = (int)HttpStatusCode.OK;
                        response.ContentType = "application/json";
                        await response.WriteAsync(JsonConvert.SerializeObject(report));
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                        break;
                }

            } catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
