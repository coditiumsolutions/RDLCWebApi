using RDLCWebAPI.Models;

namespace RDLCWebAPI.Repositories
{
    public interface IMaintenanceBillRepository
    {
        Task<List<MaintenanceBillData>> GetMaintenanceBillsAsync(
            string? project, string? phaseName, string? billingMonth, string? billingYear);
    }
}