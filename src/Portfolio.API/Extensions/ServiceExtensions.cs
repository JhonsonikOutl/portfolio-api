using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Application.Interfaces.Services;
using Portfolio.Application.Services;
using Portfolio.Infrastructure.Data;
using Portfolio.Infrastructure.Repositories;
using Portfolio.Infrastructure.Services;
using QuestPDF.Infrastructure;

namespace Portfolio.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

            services.AddSingleton<MongoDbContext>();

            services.AddHttpClient("ProfileImages")
            .ConfigureHttpClient(client =>
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");
            });

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IExperienceRepository, ExperienceRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IEducationRepository, EducationRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IExperienceService, ExperienceService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ICvGeneratorService, CvGeneratorService>();

            QuestPDF.Settings.License = LicenseType.Community;

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = configuration["JwtSettings__Secret"]
                ?? configuration["JwtSettings:Secret"]
                ?? jwtSettings["SecretKey"]
                ?? jwtSettings["Secret"]
                ?? throw new InvalidOperationException("JWT Secret no configurada");

            if (secretKey.Length < 32)
            {
                throw new InvalidOperationException(
                    $"JWT Secret debe tener al menos 32 caracteres para ser segura. Actual: {secretKey.Length}");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings__Issuer"]
                        ?? jwtSettings["Issuer"],
                    ValidAudience = configuration["JwtSettings__Audience"]
                        ?? jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey))
                };
            });

            return services;
        }

        public static IServiceCollection AddCorsPolicy(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var allowedOrigins = configuration
                .GetSection("Cors:AllowedOrigins")
                .Get<string[]>() ?? new[] { "http://localhost:4200" };

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", builder =>
                {
                    builder.WithOrigins(allowedOrigins)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });

            return services;
        }

        public static IServiceCollection AddSwaggerDocumentation(
            this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Portfolio API",
                    Version = "v1",
                    Description = "API REST para Portfolio Personal - Jonathan Aldana"
                });

                // Configuración de seguridad JWT
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header usando Bearer scheme. Ejemplo: Bearer {token}"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
            });

            return services;
        }
    }
}