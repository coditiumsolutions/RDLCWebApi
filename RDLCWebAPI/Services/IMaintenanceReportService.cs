namespace RDLCWebAPI.Services
{
    public interface IMaintenanceReportService
    {
        Task<byte[]> GenerateReportAsync(
            string reportType,  // Added report type parameter
            string project,
            string phaseNumber,
            string billingMonth,
            string billingYear);
    }
}