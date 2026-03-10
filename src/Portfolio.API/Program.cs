using Portfolio.API.Extensions;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// CONFIGURAR PUERTO DINÁMICO PARA RAILWAY
// ============================================

if (!builder.Environment.IsDevelopment())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

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
builder.Services.AddSwaggerDocumentation();

// NUEVO
builder.Services.AddHealthChecks();
builder.Services.AddApiRateLimiter();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// ============================================
// MIDDLEWARE GLOBAL
// ============================================

app.UseDeveloperExceptionPage();

// ============================================
// SWAGGER
// ============================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API v1 - Dev");
    });
}

// ============================================
// PIPELINE HTTP
// ============================================

app.UseCors("AllowAngular");

app.UseRateLimiter();

app.UseAuthentication();

app.UseAuthorization();

// ============================================
// ENDPOINTS
// ============================================

app.MapControllers();

app.MapHealthChecks("/health");

app.MapGet("/", () => Results.Ok(new
{
    status = "Healthy",
    message = "Portfolio API is running",
    timestamp = DateTime.UtcNow
}));

Console.WriteLine("🚀 Portfolio API iniciada correctamente");

app.Run();