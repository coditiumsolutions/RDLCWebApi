using Microsoft.Extensions.Configuration;
using RDLCWebAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace RDLCWebAPI.Repositories
{
    public class MaintenanceBillRepository : IMaintenanceBillRepository
    {
        private readonly string _connectionString;

        public MaintenanceBillRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<MaintenanceBillData>> GetMaintenanceBillsAsync(
            string? project, string? subProject, string? billingMonth, string? billingYear)
        {
            var bills = new List<MaintenanceBillData>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetMaintenanceBillsWithDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Send NULL if parameter is empty or null
                    command.Parameters.AddWithValue("@Project",
                        string.IsNullOrEmpty(project) ? DBNull.Value : (object)project);
                    command.Parameters.AddWithValue("@SubProject",
                        string.IsNullOrEmpty(subProject) ? DBNull.Value : (object)subProject);
                    command.Parameters.AddWithValue("@BillingMonth",
                        string.IsNullOrEmpty(billingMonth) ? DBNull.Value : (object)billingMonth);
                    command.Parameters.AddWithValue("@BillingYear",
                        string.IsNullOrEmpty(billingYear) ? DBNull.Value : (object)billingYear);

                    await connection.OpenAsync();
                    Console.WriteLine("Database connected successfully");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var bill = new MaintenanceBillData
                            {
                                // CustomersMaintenance fields
                                CM_uid = GetInt32(reader, "CM_uid"),
                                CM_CustomerNo = GetString(reader, "CM_CustomerNo"),
                                CM_CustomerName = GetString(reader, "CM_CustomerName"),
                                CM_BTNo = GetString(reader, "CM_BTNo"),
                                CM_MeterNo = GetString(reader, "CM_MeterNo"),
                                CM_History = GetString(reader, "CM_History"),

                                // Non-duplicate CM fields
                                GeneratedMonthYear = GetString(reader, "GeneratedMonthYear"),
                                LocationSeqNo = GetString(reader, "LocationSeqNo"),
                                CNICNo = GetString(reader, "CNICNo"),
                                FatherName = GetString(reader, "FatherName"),
                                InstalledOn = GetString(reader, "InstalledOn"),
                                MobileNo = GetString(reader, "MobileNo"),
                                TelephoneNo = GetString(reader, "TelephoneNo"),
                                MeterType = GetString(reader, "MeterType"),
                                NTNNumber = GetString(reader, "NTNNumber"),
                                City = GetString(reader, "City"),
                                Project = GetString(reader, "Project"),
                                SubProject = GetString(reader, "SubProject"),
                                TariffName = GetString(reader, "TariffName"),
                                BankNo = GetString(reader, "BankNo"),
                                BTNoMaintenance = GetString(reader, "BTNoMaintenance"),
                                Category = GetString(reader, "Category"),
                                Block = GetString(reader, "Block"),
                                PlotType = GetString(reader, "PlotType"),
                                Size = GetString(reader, "Size"),
                                Sector = GetString(reader, "Sector"),
                                PloNo = GetString(reader, "PloNo"),
                                BillStatusMaint = GetString(reader, "BillStatusMaint"),
                                BillStatus = GetString(reader, "BillStatus"),
                                BillGenerationStatus = GetString(reader, "BillGenerationStatus"),
                                ConnectionStatus = GetString(reader, "ConnectionStatus"),

                                // MaintenanceBills fields
                                MB_uid = GetInt32(reader, "MB_uid"),
                                MB_CustomerNo = GetString(reader, "MB_CustomerNo"),
                                MB_CustomerName = GetString(reader, "MB_CustomerName"),
                                MB_BTNo = GetString(reader, "MB_BTNo"),
                                MB_MeterNo = GetString(reader, "MB_MeterNo"),
                                MB_History = GetString(reader, "MB_History"),

                                // Non-duplicate MB fields
                                InvoiceNo = GetString(reader, "InvoiceNo"),
                                PlotStatus = GetString(reader, "PlotStatus"),
                                BillingMonth = GetString(reader, "BillingMonth"),
                                BillingYear = GetString(reader, "BillingYear"),
                                BillingDate = GetNullableDateTime(reader, "BillingDate"),
                                DueDate = GetNullableDateTime(reader, "DueDate"),
                                IssueDate = GetNullableDateTime(reader, "IssueDate"),
                                ValidDate = GetNullableDateTime(reader, "ValidDate"),
                                PaymentStatus = GetString(reader, "PaymentStatus"),
                                PaymentDate = GetNullableDateTime(reader, "PaymentDate"),
                                PaymentMethod = GetString(reader, "PaymentMethod"),
                                BankDetail = GetString(reader, "BankDetail"),
                                LastUpdated = GetNullableDateTime(reader, "LastUpdated"),
                                MaintCharges = GetNullableInt32(reader, "MaintCharges"),
                                BillAmountInDueDate = GetNullableInt32(reader, "BillAmountInDueDate"),
                                BillSurcharge = GetNullableInt32(reader, "BillSurcharge"),
                                BillAmountAfterDueDate = GetNullableInt32(reader, "BillAmountAfterDueDate"),
                                Arrears = GetNullableInt32(reader, "Arrears"),
                                TaxAmount = GetNullableInt32(reader, "TaxAmount"),
                                Fine = GetNullableInt32(reader, "Fine"),
                                OtherCharges = GetNullableInt32(reader, "OtherCharges"),
                                WaterCharges = GetNullableInt32(reader, "WaterCharges"),
                                FineDept = GetString(reader, "FineDept"),
                                MiscCharges = GetNullableInt32(reader, "MiscCharges")
                            };

                            bills.Add(bill);
                        }
                    }
                }
            }

            Console.WriteLine($"Repository returning {bills.Count} records");
            return bills;
        }

        // Helper methods
        private string GetString(SqlDataReader reader, string columnName)
        {
            return reader[columnName] != DBNull.Value ? reader[columnName].ToString() : "";
        }

        private int GetInt32(SqlDataReader reader, string columnName)
        {
            return reader[columnName] != DBNull.Value ? Convert.ToInt32(reader[columnName]) : 0;
        }

        private int? GetNullableInt32(SqlDataReader reader, string columnName)
        {
            return reader[columnName] != DBNull.Value ? Convert.ToInt32(reader[columnName]) : (int?)null;
        }

        private DateTime? GetNullableDateTime(SqlDataReader reader, string columnName)
        {
            return reader[columnName] != DBNull.Value ? Convert.ToDateTime(reader[columnName]) : (DateTime?)null;
        }
    }
}