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
            string? project,
            string? phase,
            string? btNo,
            string? billingMonth,
            string? billingYear)
        {
            var bills = new List<MaintenanceBillData>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetMaintenanceBillsWithDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Project",
                        string.IsNullOrEmpty(project) ? DBNull.Value : (object)project);
                    command.Parameters.AddWithValue("@PhaseName",
                        string.IsNullOrEmpty(phase) ? DBNull.Value : (object)phase);
                    command.Parameters.AddWithValue("@BTNo",
                        string.IsNullOrEmpty(btNo) ? DBNull.Value : (object)btNo);
                    command.Parameters.AddWithValue("@BillingMonth",
                        string.IsNullOrEmpty(billingMonth) ? DBNull.Value : (object)billingMonth);
                    command.Parameters.AddWithValue("@BillingYear",
                        string.IsNullOrEmpty(billingYear) ? DBNull.Value : (object)billingYear);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var bill = new MaintenanceBillData
                            {
                                // ========== CustomersMaintenance Fields (column names as per table) ==========
                                CM_uid = GetInt32(reader, "CM_uid"),
                                CM_KuickPayNo = GetString(reader, "CM_KuickPayNo"),
                                CM_CustomerName = GetString(reader, "CM_CustomerName"),
                                CM_BTNo = GetString(reader, "CM_BTNo"),
                                CM_History = GetString(reader, "CM_History"),
                                GeneratedMonthYear = GetString(reader, "GeneratedMonthYear"),
                                LocationSeqNo = GetString(reader, "LocationSeqNo"),
                                CNICNo = GetString(reader, "CNICNo"),
                                FatherName = GetString(reader, "FatherName"),
                                MobileNo = GetString(reader, "MobileNo"),
                                City = GetString(reader, "City"),
                                Project = GetString(reader, "Project"),
                                PhaseName = GetString(reader, "PhaseName"),                    // ← PhaseName se Phase
                                Category = GetString(reader, "Category"),
                                Size = GetString(reader, "Size"),
                                Sector = GetString(reader, "Sector"),
                                PlotNo = GetString(reader, "PlotNo"),                  // ← PloNo se PlotNo
                                BillGenerationStatus = GetString(reader, "BillGenerationStatus"),
                                ConnectionStatus = GetString(reader, "ConnectionStatus"),
                                CM_PlotStatus = GetString(reader, "CM_PlotStatus"),
                                StreetNo = GetString(reader, "StreetNo"),              // ← StreetNumber se StreetNo
                                UnitType = GetString(reader, "UnitType"),

                                // Extra charges
                                Maint = GetNullableDouble(reader, "Maint"),
                                Misc = GetNullableDouble(reader, "Misc"),
                                Water = GetNullableDouble(reader, "Water"),
                                Rent = GetNullableDouble(reader, "Rent"),
                                Generator = GetNullableDouble(reader, "Generator"),
                                Other = GetNullableDouble(reader, "Other"),
                                foodsafety = GetNullableDouble(reader, "foodsafety"),
                                trollytrip = GetNullableDouble(reader, "trollytrip"),
                                extrawork = GetNullableDouble(reader, "extrawork"),

                                // ========== MaintenanceBills Fields ==========
                                MB_uid = GetInt32(reader, "MB_uid"),
                                MB_KuickPayNo = GetString(reader, "MB_KuickPayNo"),
                                MB_CustomerName = GetString(reader, "MB_CustomerName"),
                                MB_BTNo = GetString(reader, "MB_BTNo"),
                                MB_History = GetString(reader, "MB_History"),
                                Plot_Number = GetString(reader, "Plot_Number"),
                                Street_Number = GetString(reader, "Street_Number"),
                                MB_PhaseName = GetString(reader, "MB_PhaseName"),              // ← MB_PhaseName se MB_Phase
                                MB_Category = GetString(reader, "MB_Category"),
                                MB_Project = GetString(reader, "MB_Project"),
                                MB_PlotStatus = GetString(reader, "MB_PlotStatus"),
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
                                PushedOn = GetNullableDateTime(reader, "PushedOn")
                            };

                            bills.Add(bill);
                        }
                    }
                }
            }

            return bills;
        }

        // Helper methods (same as before)
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

        private double? GetNullableDouble(SqlDataReader reader, string columnName)
        {
            return reader[columnName] != DBNull.Value ? Convert.ToDouble(reader[columnName]) : (double?)null;
        }

        private DateTime? GetNullableDateTime(SqlDataReader reader, string columnName)
        {
            return reader[columnName] != DBNull.Value ? Convert.ToDateTime(reader[columnName]) : (DateTime?)null;
        }
    }
}