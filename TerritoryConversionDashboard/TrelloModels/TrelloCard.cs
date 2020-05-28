using System.Collections.Generic;
using TerritoryConversionDashboard.TrelloModels;

namespace TerritoryConversionDashboard
{
    public class TrelloCard
    {
        public string Id { get; set; }
        public string IdList { get; set; }
        public IEnumerable<TrelloLabelModel> Labels { get; set; }
    }
}
