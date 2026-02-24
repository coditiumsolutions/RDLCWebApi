using Microsoft.AspNetCore.Mvc;
using RDLCWebAPI.Services;

namespace RDLCWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaintenanceBillsController : ControllerBase
    {
        private readonly IMaintenanceReportService _reportService;

        public MaintenanceBillsController(IMaintenanceReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GenerateReport(
            [FromQuery] string? project = null,
            [FromQuery] string? subProject = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            try
            {
                Console.WriteLine($"API Call received with:");
                Console.WriteLine($"  Project: {(string.IsNullOrEmpty(project) ? "NULL" : project)}");
                Console.WriteLine($"  SubProject: {(string.IsNullOrEmpty(subProject) ? "NULL" : subProject)}");
                Console.WriteLine($"  Month: {(string.IsNullOrEmpty(billingMonth) ? "NULL" : billingMonth)}");
                Console.WriteLine($"  Year: {(string.IsNullOrEmpty(billingYear) ? "NULL" : billingYear)}");

                var reportBytes = await _reportService.GenerateReportAsync(
                    project, subProject, billingMonth, billingYear);

                string fileName = $"MaintenanceBill_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                return File(reportBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}