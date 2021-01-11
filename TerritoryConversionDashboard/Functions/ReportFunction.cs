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

        public ReportFunction(IReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task HandleAsync(HttpContext context)
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
        }
    }
}
