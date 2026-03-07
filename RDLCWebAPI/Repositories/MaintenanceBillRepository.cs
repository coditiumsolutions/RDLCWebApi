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
            string? project, string? phaseNumber, string? billingMonth, string? billingYear)
        {
            var bills = new List<MaintenanceBillData>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetMaintenanceBillsWithDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Updated parameter names
                    command.Parameters.AddWithValue("@Project",
                        string.IsNullOrEmpty(project) ? DBNull.Value : (object)project);
                    command.Parameters.AddWithValue("@PhaseNumber",
                        string.IsNullOrEmpty(phaseNumber) ? DBNull.Value : (object)phaseNumber);
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
                                // CustomersMaintenance fields (updated)
                                CM_uid = GetInt32(reader, "CM_uid"),
                                CM_KuickPayNo = GetString(reader, "CM_KuickPayNo"),
                                CM_CustomerName = GetString(reader, "CM_CustomerName"),
                                CM_BTNo = GetString(reader, "CM_BTNo"),
                                CM_History = GetString(reader, "CM_History"),

                                // Non-duplicate CM fields (removed deleted fields)
                                GeneratedMonthYear = GetString(reader, "GeneratedMonthYear"),
                                LocationSeqNo = GetString(reader, "LocationSeqNo"),
                                CNICNo = GetString(reader, "CNICNo"),
                                FatherName = GetString(reader, "FatherName"),
                                MobileNo = GetString(reader, "MobileNo"),
                                City = GetString(reader, "City"),
                                Project = GetString(reader, "Project"),
                                PhaseNumber = GetString(reader, "PhaseNumber"),     // Renamed
                                Category = GetString(reader, "Category"),
                                Size = GetString(reader, "Size"),
                                Sector = GetString(reader, "Sector"),
                                PloNo = GetString(reader, "PloNo"),
                                BillGenerationStatus = GetString(reader, "BillGenerationStatus"),
                                ConnectionStatus = GetString(reader, "ConnectionStatus"),
                                CM_PlotStatus = GetString(reader, "CM_PlotStatus"), // Renamed
                                StreetNumber = GetString(reader, "StreetNumber"),
                                UnitType = GetString(reader, "UnitType"),

                                // MaintenanceBills fields (updated)
                                MB_uid = GetInt32(reader, "MB_uid"),
                                MB_KuickPayNo = GetString(reader, "MB_KuickPayNo"),
                                MB_CustomerName = GetString(reader, "MB_CustomerName"),
                                MB_BTNo = GetString(reader, "MB_BTNo"),
                                MB_History = GetString(reader, "MB_History"),

                                // Non-duplicate MB fields
                                Plot_Number = GetString(reader, "Plot_Number"),
                                Street_Number = GetString(reader, "Street_Number"),
                                Phase_Number = GetString(reader, "Phase_Number"),
                                Plot_Category = GetString(reader, "Plot_Category"),
                                PROJECTNAME = GetString(reader, "PROJECTNAME"),
                                MB_PlotStatus = GetString(reader, "MB_PlotStatus"), // Renamed
                                BillingMonth = GetString(reader, "BillingMonth"),
                                BillingYear = GetString(reader, "BillingYear"),
                                DueDate = GetNullableDateTime(reader, "DueDate"),
                                IssueDate = GetNullableDateTime(reader, "IssueDate"),
                                PaymentStatus = GetString(reader, "PaymentStatus"),
                                PaymentDate = GetNullableDateTime(reader, "PaymentDate"),
                                PaymentMethod = GetString(reader, "PaymentMethod"),
                                BankDetail = GetString(reader, "BankDetail"),
                                PAIDBYOPERATOR = GetString(reader, "PAIDBYOPERATOR"),
                                AMOUNTPAID = GetNullableInt32(reader, "AMOUNTPAID"),
                                MaintCharges = GetNullableInt32(reader, "MaintCharges"),
                                WaterCharges = GetNullableInt32(reader, "WaterCharges"),
                                OtherCharges = GetNullableInt32(reader, "OtherCharges"),
                                MiscCharges = GetNullableInt32(reader, "MiscCharges"),
                                installamount = GetNullableInt32(reader, "installamount"),
                                current_gst = GetNullableInt32(reader, "current_gst"),
                                Arrears = GetNullableInt32(reader, "Arrears"),
                                PreviousArrears = GetNullableInt32(reader, "PreviousArrears"),
                                advance_payment = GetNullableInt32(reader, "advance_payment"),
                                AdvanceAmount = GetNullableInt32(reader, "AdvanceAmount"),
                                BillAmountInDueDate = GetNullableInt32(reader, "BillAmountInDueDate"),
                                BillSurcharge = GetNullableInt32(reader, "BillSurcharge"),
                                BillAmountAfterDueDate = GetNullableInt32(reader, "BillAmountAfterDueDate"),
                                GTotal = GetNullableInt32(reader, "GTotal"),
                                compute = GetString(reader, "compute"),
                                conndate = GetNullableDateTime(reader, "conndate"),
                                UpdateBy = GetString(reader, "UpdateBy"),
                                UpdateOn = GetNullableDateTime(reader, "UpdateOn"),
                                PushedBy = GetString(reader, "PushedBy"),
                                PushedOn = GetNullableDateTime(reader, "PushedOn"),

                                // NEW FIELDS ADDED HERE
                                RentAmount = GetNullableInt32(reader, "RentAmount"),
                                FoodSafety = GetNullableInt32(reader, "FoodSafety"),
                                TrollyTrip = GetNullableInt32(reader, "TrollyTrip"),
                                ExtraWork = GetNullableInt32(reader, "ExtraWork"),
                                DieselCost = GetNullableInt32(reader, "DieselCost")
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