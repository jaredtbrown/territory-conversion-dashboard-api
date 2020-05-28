using TerritoryConversionDashboard.Models;

namespace TerritoryConversionDashboard
{
    public class ReportModel
    {
        public TerritoryReportModel AllTerritories { get; set; }
        public TerritoryReportModel HomeTerritories { get; set; }
        public TerritoryReportModel RuralTerritories { get; set; }
    }
}
