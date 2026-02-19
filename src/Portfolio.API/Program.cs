using Portfolio.API.Extensions;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configurar puerto dinámico para Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// ============================================
// CONFIGURACIÓN DE SERVICIOS
// ============================================

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// ============================================
// CONFIGURACIÓN DEL PIPELINE HTTP
// ============================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine("🚀 Portafolio API iniciada correctamente");
Console.WriteLine("📄 Swagger disponible en: http://localhost:5000/swagger");

app.MapGet("/", () => Results.Ok(new
{
    status = "Healthy",
    message = "Portfolio API is running",
    timestamp = DateTime.UtcNow
}));

app.Run();