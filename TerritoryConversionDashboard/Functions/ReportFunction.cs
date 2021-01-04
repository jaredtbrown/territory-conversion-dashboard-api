using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Google.Cloud.Functions.Framework;
using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
