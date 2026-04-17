using RDLCWebAPI.Models;

namespace RDLCWebAPI.Repositories
{
    public interface IMaintenanceBillRepository
    {
        Task<List<MaintenanceBillData>> GetMaintenanceBillsAsync(
            string? project,
            string? phaseName,
            string? btNo,
            string? billingMonth,
            string? billingYear);
    }
}