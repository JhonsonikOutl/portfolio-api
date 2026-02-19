Portfolio API - Backend
API RESTful para portfolio profesional desarrollada con .NET Core 8, MongoDB Atlas y Clean Architecture.
Características

Clean Architecture + SOLID Principles
MongoDB Atlas (cloud database)
JWT Authentication con BCrypt
Generación dinámica de CV en PDF (QuestPDF)
Swagger/OpenAPI Documentation
CORS configurado
Data seeding automático

Stack Tecnológico

.NET Core 8
MongoDB Atlas
QuestPDF
JWT Bearer Authentication
Swagger/OpenAPI
BCrypt

Estructura del Proyecto
Portfolio.API/
├── src/
│   ├── API/                    # Controllers, Middleware, Startup
│   ├── Application/            # DTOs, Interfaces, Services
│   ├── Domain/                 # Entities, Value Objects
│   └── Infrastructure/         # Repositories, Data Access, External Services
Instalación
bash# Clonar repositorio
git clone https://github.com/JonathanAldairAv/portfolio-api.git

# Navegar al proyecto
cd Portfolio.API

# Restaurar paquetes NuGet
dotnet restore

# Ejecutar la aplicación
dotnet run --project src/API
Configuración
Crea o actualiza appsettings.json:
json{
  "MongoDbSettings": {
    "ConnectionString": "mongodb+srv://usuario:password@cluster.mongodb.net/",
    "DatabaseName": "PortfolioDB"
  },
  "JwtSettings": {
    "Secret": "tu-clave-secreta-super-segura-de-al-menos-32-caracteres",
    "Issuer": "PortfolioAPI",
    "Audience": "PortfolioClient",
    "ExpirationInMinutes": 60
  },
  "AllowedOrigins": [
    "http://localhost:4200",
    "https://tu-dominio.com"
  ]
}
Endpoints Principales
Públicos (sin autenticación)

GET /api/profile - Obtener perfil profesional
GET /api/profile/cv - Descargar CV en PDF
GET /api/projects - Listar proyectos
GET /api/skills - Listar habilidades técnicas
GET /api/experience - Listar experiencia laboral
POST /api/contact - Enviar mensaje de contacto
POST /api/auth/login - Iniciar sesión

Protegidos (requieren JWT)

PUT /api/profile - Actualizar perfil
POST /api/projects - Crear proyecto
PUT /api/projects/{id} - Actualizar proyecto
DELETE /api/projects/{id} - Eliminar proyecto
CRUD completo para Skills, Experience, Education

Seed de Datos
Ejecuta el endpoint de seed para poblar la base de datos con datos iniciales:
bashPOST https://localhost:5001/api/seed
Documentación API
Swagger UI disponible en: https://localhost:5001/swagger
Generación de CV
El sistema genera dinámicamente un CV en PDF profesional usando QuestPDF. El CV incluye:

Información personal y de contacto
Perfil profesional
Experiencia laboral con logros
Educación
Habilidades técnicas agrupadas por categoría
Proyectos destacados

Variables de Entorno
Para producción, configura estas variables de entorno:

MONGODB_CONNECTION_STRING
JWT_SECRET
ASPNETCORE_ENVIRONMENT

Autor
Jonathan Fabián Aldana Torres
Desarrollador Senior Full Stack | .NET & Angular
Contacto: jonathanaldana9@hotmail.com