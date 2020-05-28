using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using TerritoryConversionDashboard.Models;

namespace TerritoryConversionDashboard.Factories
{
    public interface ITerritoryReportModelFactory
    {
        TerritoryReportModel Create(IEnumerable<TrelloCard> trelloCards, string labelId = null);
    }

    public class TerritoryReportModelFactory : ITerritoryReportModelFactory
    {
        private readonly TrelloApiConfig _trelloApiConfig;

        public TerritoryReportModelFactory(IOptions<TrelloApiConfig> trelloApiConfigAccessor)
        {
            _trelloApiConfig = trelloApiConfigAccessor.Value;
        }

        public TerritoryReportModel Create(IEnumerable<TrelloCard> trelloCards, string labelId = null)
        {
            if (labelId != null)
                trelloCards = trelloCards.Where(x => x.Labels.Any(x => x.Id == labelId));

            return new TerritoryReportModel
            {
                Total = trelloCards.Count(),
                TotalCompleted = trelloCards.Count(x => x.IdList == _trelloApiConfig.CompletedListId),
                TotalInProgress = trelloCards.Count(x => x.IdList == _trelloApiConfig.InProgressListId),
                TotalRemaining = trelloCards.Count(x => x.IdList == _trelloApiConfig.ToDoListId)
            };
        }
    }
}
