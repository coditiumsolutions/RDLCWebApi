using Microsoft.Reporting.NETCore;
using RDLCWebAPI.Models;
using RDLCWebAPI.Repositories;
using System.Data;
using System.Reflection;
using System.Text;

namespace RDLCWebAPI.Services
{
    public class MaintenanceReportService : IMaintenanceReportService
    {
        private readonly IMaintenanceBillRepository _billRepository;
        private readonly IWebHostEnvironment _environment;

        private readonly string _reportFileName = "Safari - 1.rdlc";

        public MaintenanceReportService(
            IMaintenanceBillRepository billRepository,
            IWebHostEnvironment environment)
        {
            _billRepository = billRepository;
            _environment = environment;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public async Task<byte[]> GenerateReportAsync(
            string project, string phaseNumber, string billingMonth, string billingYear)
        {
            try
            {
                Console.WriteLine("========== MAINTENANCE REPORT DEBUG ==========");
                Console.WriteLine($"Parameters: Project={project}, PhaseNumber={phaseNumber}, Month={billingMonth}, Year={billingYear}");
                Console.WriteLine($"Using Report File: {_reportFileName}");

                // 1. Get data
                var bills = await _billRepository.GetMaintenanceBillsAsync(
                    string.IsNullOrEmpty(project) ? null : project,
                    string.IsNullOrEmpty(phaseNumber) ? null : phaseNumber,
                    string.IsNullOrEmpty(billingMonth) ? null : billingMonth,
                    string.IsNullOrEmpty(billingYear) ? null : billingYear);

                Console.WriteLine($"Records retrieved: {bills?.Count ?? 0}");

                if (bills == null || bills.Count == 0)
                {
                    throw new Exception($"No data found for the specified parameters");
                }

                // 2. Convert to DataTable
                DataTable dt = CreateDataTable(bills);
                Console.WriteLine($"DataTable created: {dt.Columns.Count} columns, {dt.Rows.Count} rows");

                // 3. Find report file
                string reportPath = FindReportFile();
                Console.WriteLine($"Report path: {reportPath}");

                // 4. Load report
                using var fs = new FileStream(reportPath, FileMode.Open, FileAccess.Read);
                using var report = new LocalReport();
                report.LoadReportDefinition(fs);
                Console.WriteLine("Report loaded successfully");

                // 5. Add data source
                report.DataSources.Add(new ReportDataSource("DataSet1", dt));

                // 6. Set parameters (updated parameter name)
                var parameters = new[]
                {
                    new ReportParameter("Project", project ?? ""),
                    new ReportParameter("PhaseNumber", phaseNumber ?? ""),    // Renamed
                    new ReportParameter("BillingMonth", billingMonth ?? ""),
                    new ReportParameter("BillingYear", billingYear ?? "")
                };

                report.SetParameters(parameters);
                Console.WriteLine("Parameters set successfully");

                // 7. Render PDF
                var renderedBytes = report.Render("PDF");
                Console.WriteLine($"✅ Report generated: {renderedBytes.Length} bytes");
                return renderedBytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"INNER: {ex.InnerException.Message}");
                }
                throw new Exception($"Report generation failed: {ex.Message}", ex);
            }
        }

        private string FindReportFile()
        {
            string[] paths = new[]
            {
                Path.Combine(_environment.ContentRootPath, "Reports", _reportFileName),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", _reportFileName),
                Path.Combine(Directory.GetCurrentDirectory(), "Reports", _reportFileName),
            };

            foreach (var path in paths)
            {
                if (File.Exists(path))
                {
                    Console.WriteLine($"✅ Found report at: {path}");
                    return path;
                }
            }

            throw new FileNotFoundException($"Report file '{_reportFileName}' not found in Reports folder.");
        }

        private DataTable CreateDataTable(List<MaintenanceBillData> bills)
        {
            DataTable dt = new DataTable("MaintenanceData");

            var properties = typeof(MaintenanceBillData).GetProperties();

            foreach (var prop in properties)
            {
                Type propType = prop.PropertyType;
                if (propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propType = Nullable.GetUnderlyingType(propType);
                }
                dt.Columns.Add(prop.Name, propType ?? typeof(string));
            }

            foreach (var bill in bills)
            {
                var row = dt.NewRow();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(bill);

                    if (value == null)
                    {
                        if (prop.PropertyType == typeof(string))
                            row[prop.Name] = "";
                        else if (prop.PropertyType == typeof(int?) || prop.PropertyType == typeof(decimal?))
                            row[prop.Name] = 0;
                        else if (prop.PropertyType == typeof(DateTime?))
                            row[prop.Name] = DBNull.Value;
                        else
                            row[prop.Name] = DBNull.Value;
                    }
                    else
                    {
                        row[prop.Name] = value;
                    }
                }
                dt.Rows.Add(row);
            }

            return dt;
        }
    }
}