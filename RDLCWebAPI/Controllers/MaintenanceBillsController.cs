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

        // Safari-1 endpoint
        [HttpGet("Safari-1")]
        public async Task<IActionResult> GenerateSafari1Report(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseName = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("Safari-1", project, phaseName, billingMonth, billingYear);
        }

        // Safari-2 endpoint
        [HttpGet("Safari-2")]
        public async Task<IActionResult> GenerateSafari2Report(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseName = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("Safari-2", project, phaseName, billingMonth, billingYear);
        }

        // Safari-3 endpoint
        [HttpGet("Safari-3")]
        public async Task<IActionResult> GenerateSafari3Report(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseName = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("Safari-3", project, phaseName, billingMonth, billingYear);
        }

        // BahriaSpring endpoint
        [HttpGet("BahriaSpring")]
        public async Task<IActionResult> GenerateBahriaSpringReport(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseName = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("BahriaSpring", project, phaseName, billingMonth, billingYear);
        }

        // SafariHeights endpoint
        [HttpGet("SafariHeights")]
        public async Task<IActionResult> GenerateSafariHeightsReport(
            [FromQuery] string? project = null,
            [FromQuery] string? phaseName = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            return await GenerateReportInternal("SafariHeights", project, phaseName, billingMonth, billingYear);
        }

        // Single bill endpoint (only 4 parameters)
        [HttpGet("SingleBill")]
        public async Task<IActionResult> GenerateSingleBillReport(
            [FromQuery] string? project = null,
            [FromQuery] string? btNo = null,
            [FromQuery] string? billingMonth = null,
            [FromQuery] string? billingYear = null)
        {
            try
            {
                Console.WriteLine($"========== SINGLE BILL REPORT REQUEST ==========");
                Console.WriteLine($"Project: {project}, BTNo: {btNo}, Month: {billingMonth}, Year: {billingYear}");

                //if (string.IsNullOrEmpty(btNo))
                //{
                //    return BadRequest("BTNo is required");
                //}

                string reportType = GetReportTypeByProject(project);
                Console.WriteLine($"Report Type: {reportType}");

                var bills = await _reportService.GetBillsByParametersAsync(
                    reportType, project, btNo, billingMonth, billingYear);

                if (bills == null || bills.Count == 0)
                {
                    return NotFound($"No bill found for BTNo: {btNo}");
                }

                var reportBytes = await _reportService.GenerateReportFromDataAsync(
                    reportType, bills, project ?? "", billingMonth ?? "", billingYear ?? "");

                string fileName = $"Bill_{btNo}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                return File(reportBytes, "application/pdf", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating bill: {ex.Message}");
            }
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

        private string GetReportTypeByProject(string? project)
        {
            if (string.IsNullOrEmpty(project))
                return "Safari-1";

            // Convert to lowercase for case-insensitive comparison
            var projectLower = project.ToLower().Trim();

            Console.WriteLine($"Mapping project: '{project}' to report type...");

            // ✅ Bahria Heights -> Safari Heights report
            if (projectLower == "bahria heights" ||
                projectLower.Contains("bahria heights") ||
                projectLower == "bahriaheights")
            {
                Console.WriteLine($"Matched: '{project}' -> SafariHeights (Report)");
                return "SafariHeights";
            }

            // Safari-1 project
            if (projectLower.Contains("safari-1") || projectLower == "safari1")
            {
                Console.WriteLine($"Matched: '{project}' -> Safari-1");
                return "Safari-1";
            }

            // Safari-2 project
            if (projectLower.Contains("safari-2") || projectLower == "safari2")
            {
                Console.WriteLine($"Matched: '{project}' -> Safari-2");
                return "Safari-2";
            }

            // Safari-3 project
            if (projectLower.Contains("safari-3") || projectLower == "safari3")
            {
                Console.WriteLine($"Matched: '{project}' -> Safari-3");
                return "Safari-3";
            }

            // Bahria Spring project
            if (projectLower.Contains("bahria spring") || projectLower == "bahriaspring")
            {
                Console.WriteLine($"Matched: '{project}' -> BahriaSpring");
                return "Safari-3";
            }

            // Default
            Console.WriteLine($"No match found for '{project}', using default: Safari-1");
            return "Safari-1";
        }
    }
}