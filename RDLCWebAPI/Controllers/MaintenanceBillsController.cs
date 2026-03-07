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

        // Existing endpoint - Safari-1
        [HttpGet("Safari-1")]
        public async Task<IActionResult> GenerateSafari1Report(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseNumber = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("Safari-1", project, phaseNumber, billingMonth, billingYear);
        }

        // Safari-2 endpoint
        [HttpGet("Safari-2")]
        public async Task<IActionResult> GenerateSafari2Report(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseNumber = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("Safari-2", project, phaseNumber, billingMonth, billingYear);
        }

        // Safari-3 endpoint
        [HttpGet("Safari-3")]
        public async Task<IActionResult> GenerateSafari3Report(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseNumber = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("Safari-3", project, phaseNumber, billingMonth, billingYear);
        }

        // SafariHeights endpoint
        [HttpGet("SafariHeights")]
        public async Task<IActionResult> GenerateSafariHeightsReport(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseNumber = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("SafariHeights", project, phaseNumber, billingMonth, billingYear);
        }

        // Private helper method
        private async Task<IActionResult> GenerateReportInternal(
            string reportType,
            string? project,
            string? phaseNumber,
            string? billingMonth,
            string? billingYear)
        {
            try
            {
                Console.WriteLine($"========== {reportType} REPORT REQUEST ==========");
                Console.WriteLine($"API Call received with:");
                Console.WriteLine($"  Project: {(string.IsNullOrEmpty(project) ? "NULL" : project)}");
                Console.WriteLine($"  PhaseNumber: {(string.IsNullOrEmpty(phaseNumber) ? "NULL" : phaseNumber)}");
                Console.WriteLine($"  Month: {(string.IsNullOrEmpty(billingMonth) ? "NULL" : billingMonth)}");
                Console.WriteLine($"  Year: {(string.IsNullOrEmpty(billingYear) ? "NULL" : billingYear)}");

                var reportBytes = await _reportService.GenerateReportAsync(
                    reportType, project, phaseNumber, billingMonth, billingYear);

                string fileName = $"{reportType}_Bill_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                return File(reportBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating {reportType} report: {ex.Message}");
            }
        }
    }
}