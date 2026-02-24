namespace RDLCWebAPI.Services
{
    public interface IMaintenanceReportService
    {
        Task<byte[]> GenerateReportAsync(
            string project, string subProject, string billingMonth, string billingYear);
    }
}