using RDLCWebAPI.Models;

namespace RDLCWebAPI.Repositories
{
    public interface IMaintenanceBillRepository
    {
        Task<List<MaintenanceBillData>> GetMaintenanceBillsAsync(
            string? project, string? subProject, string? billingMonth, string? billingYear);
    }
}