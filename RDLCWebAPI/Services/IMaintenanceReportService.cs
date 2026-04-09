namespace RDLCWebAPI.Services
{
    public interface IMaintenanceReportService
    {
        Task<byte[]> GenerateReportAsync(
            string reportType,
            string project,
            string phaseName,
            string billingMonth,
            string billingYear);
    }
}