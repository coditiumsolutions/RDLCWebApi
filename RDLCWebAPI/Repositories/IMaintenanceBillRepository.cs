using RDLCWebAPI.Models;

namespace RDLCWebAPI.Repositories
{
    public interface IMaintenanceBillRepository
    {
        Task<List<MaintenanceBillData>> GetMaintenanceBillsAsync(
            string? project, string? phaseNumber, string? billingMonth, string? billingYear);
    }
}