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

        [HttpGet("Safari-1")]
        public async Task<IActionResult> GenerateSafari1Report(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseName = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("Safari-1", project, phaseName, billingMonth, billingYear);
        }

        [HttpGet("Safari-2")]
        public async Task<IActionResult> GenerateSafari2Report(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseName = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("Safari-2", project, phaseName, billingMonth, billingYear);
        }

        [HttpGet("Safari-3")]
        public async Task<IActionResult> GenerateSafari3Report(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseName = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("Safari-3", project, phaseName, billingMonth, billingYear);
        }

        [HttpGet("BahriaSpring")]
        public async Task<IActionResult> GenerateBahriaSpringReport(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseName = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("BahriaSpring", project, phaseName, billingMonth, billingYear);
        }

        [HttpGet("SafariHeights")]
        public async Task<IActionResult> GenerateSafariHeightsReport(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseName = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("SafariHeights", project, phaseName, billingMonth, billingYear);
        }

        private async Task<IActionResult> GenerateReportInternal(
            string reportType,
            string? project,
            string? phaseName,
            string? billingMonth,
            string? billingYear)
        {
            try
            {
                Console.WriteLine($"========== {reportType} REPORT REQUEST ==========");

                var reportBytes = await _reportService.GenerateReportAsync(
                    reportType, project ?? "", phaseName ?? "", billingMonth ?? "", billingYear ?? "");

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