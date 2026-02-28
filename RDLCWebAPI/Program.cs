using RDLCWebAPI.Repositories;
using RDLCWebAPI.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Enable CORS (Allow All)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Register repositories
builder.Services.AddScoped<IMaintenanceBillRepository, MaintenanceBillRepository>();

// Register services
builder.Services.AddScoped<IMaintenanceReportService, MaintenanceReportService>();

// Register encoding for RDLC
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var app = builder.Build();

// ✅ Enable Swagger in ALL environments (Development + Production)
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "RDLC Web API V1");
    options.RoutePrefix = "swagger";
    // swagger/index.html
});

// Optional: redirect root to swagger
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();

// ✅ Enable CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Ensure Reports folder exists
var reportsPath = Path.Combine(app.Environment.ContentRootPath, "Reports");

if (!Directory.Exists(reportsPath))
{
    Directory.CreateDirectory(reportsPath);
}

app.Run();