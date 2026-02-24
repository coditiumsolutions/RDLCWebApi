using RDLCWebAPI.Repositories;
using RDLCWebAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddScoped<IMaintenanceBillRepository, MaintenanceBillRepository>();

// Register services
builder.Services.AddScoped<IMaintenanceReportService, MaintenanceReportService>();

// Register encoding
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Ensure Reports folder exists
var reportsPath = Path.Combine(app.Environment.ContentRootPath, "Reports");
if (!Directory.Exists(reportsPath))
{
    Directory.CreateDirectory(reportsPath);
}

app.Run();