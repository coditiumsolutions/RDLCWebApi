using RDLCWebAPI.Models;

namespace RDLCWebAPI.Services
{
    public interface IMaintenanceReportService
    {
        // For bulk reports (with phaseName)
        Task<byte[]> GenerateReportAsync(
            string reportType,
            string project,
            string phaseName,
            string billingMonth,
            string billingYear);

        // For single bill (only 4 parameters)
        Task<List<MaintenanceBillData>> GetBillsByParametersAsync(
            string reportType,
            string? project,
            string? btNo,
            string? billingMonth,
            string? billingYear);

        // Generate PDF from existing data
        Task<byte[]> GenerateReportFromDataAsync(
            string reportType,
            List<MaintenanceBillData> bills,
            string project,
            string billingMonth,
            string billingYear);
    }
}