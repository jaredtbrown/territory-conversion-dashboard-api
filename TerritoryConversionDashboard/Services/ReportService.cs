using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TerritoryConversionDashboard.Factories;

namespace TerritoryConversionDashboard
{
    public interface IReportService
    {
        Task<ReportModel> GetAsync();
    }

    public class ReportService : IReportService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TrelloApiConfig _trelloApiConfig;
        private readonly ITerritoryReportModelFactory _territoryReportModelFactory;

        public ReportService(IHttpClientFactory httpClientFactory, IOptions<TrelloApiConfig> trelloApiConfigAccessor, ITerritoryReportModelFactory territoryReportModelFactory)
        {
            _httpClientFactory = httpClientFactory;
            _trelloApiConfig = trelloApiConfigAccessor.Value;
            _territoryReportModelFactory = territoryReportModelFactory;
        }

        public async Task<ReportModel> GetAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{_trelloApiConfig.BaseUrl}/1/boards/{_trelloApiConfig.BoardId}/cards?key={_trelloApiConfig.Key}&token={_trelloApiConfig.Token}");

            var client = _httpClientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var stringContent = await response.Content.ReadAsStringAsync();
            var trelloCards = JsonConvert.DeserializeObject<IEnumerable<TrelloCard>>(stringContent);

            var numberOfTerritories = trelloCards.Count();
            var numberOfTerritoriesCompleted = trelloCards.Count(x => x.IdList == _trelloApiConfig.CompletedListId);

            return new ReportModel
            {
                AllTerritories = _territoryReportModelFactory.Create(trelloCards),
                HomeTerritories = _territoryReportModelFactory.Create(trelloCards, _trelloApiConfig.HomeLabelId),
                RuralTerritories = _territoryReportModelFactory.Create(trelloCards, _trelloApiConfig.RuralLabelId)
            };
        }
    }
}
