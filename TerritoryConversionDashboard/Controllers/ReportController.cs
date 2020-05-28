using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TerritoryConversionDashboard.Controllers
{
    [ApiController]
    [Route("/v1/report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult<ReportModel>> GetReportAsync()
        {
            var report = await _reportService.GetAsync();

            return report;
        }
    }
}
