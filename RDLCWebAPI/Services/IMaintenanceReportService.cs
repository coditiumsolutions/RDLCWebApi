namespace RDLCWebAPI.Services
{
    public interface IMaintenanceReportService
    {
        Task<byte[]> GenerateReportAsync(
            string project, string phaseNumber, string billingMonth, string billingYear);
    }
}