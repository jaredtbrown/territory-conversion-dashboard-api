using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TerritoryConversionDashboard.Factories;
using System.Net.Http;

namespace TerritoryConversionDashboard
{
    public class NewStartup : FunctionsStartup
    {
        public override void ConfigureServices(WebHostBuilderContext context, IServiceCollection services) {
            services.AddHttpClient();

            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ITerritoryReportModelFactory, TerritoryReportModelFactory>();

            IConfigurationSection trelloApiSection = context.Configuration.GetSection("TrelloApi");
            services.Configure<TrelloApiConfig>(trelloApiSection);
        }
    }
}
