using Portfolio.Domain.Entities;
using Portfolio.Application.DTOs.Seed;
using MongoDB.Driver;

namespace Portfolio.Infrastructure.Data
{
    /// <summary>
    /// Clase para poblar la base de datos con datos iniciales.
    /// Basado en el CV de Jonathan Aldana.
    /// </summary>
    public class DataSeeder
    {
        private readonly MongoDbContext _context;

        public DataSeeder(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<SeedResultDto> SeedAllAsync()
        {
            var profileCount = await SeedProfileAsync();
            var skillsCount = await SeedSkillsAsync();
            var experiencesCount = await SeedExperiencesAsync();
            var projectsCount = await SeedProjectsAsync();

            Console.WriteLine("✅ Base de datos poblada correctamente con datos de Jonathan Aldana");

            return new SeedResultDto
            {
                ProfileCreated = profileCount,
                SkillsCreated = skillsCount,
                ExperiencesCreated = experiencesCount,
                ProjectsCreated = projectsCount
            };
        }

        private async Task<int> SeedSkillsAsync()
        {
            var existingSkills = await _context.Skills.CountDocumentsAsync(_ => true);
            if (existingSkills > 0)
            {
                Console.WriteLine("⏭️  Skills ya existen, omitiendo seed");
                return 0;
            }

            var skills = new List<Skill>
        {
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = ".NET Core",
                Category = "Backend",
                Level = 95,
                Icon = "devicon-dotnetcore-plain",
                DisplayOrder = 1
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "C#",
                Category = "Backend",
                Level = 95,
                Icon = "devicon-csharp-plain",
                DisplayOrder = 2
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "ASP.NET Core",
                Category = "Backend",
                Level = 90,
                Icon = "devicon-dotnetcore-plain",
                DisplayOrder = 3
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "Clean Architecture",
                Category = "Backend",
                Level = 90,
                Icon = null,
                DisplayOrder = 4
            },
            
            // Frontend
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "Angular",
                Category = "Frontend",
                Level = 90,
                Icon = "devicon-angular-plain",
                DisplayOrder = 5
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "TypeScript",
                Category = "Frontend",
                Level = 85,
                Icon = "devicon-typescript-plain",
                DisplayOrder = 6
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "JavaScript",
                Category = "Frontend",
                Level = 85,
                Icon = "devicon-javascript-plain",
                DisplayOrder = 7
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "HTML5",
                Category = "Frontend",
                Level = 90,
                Icon = "devicon-html5-plain",
                DisplayOrder = 8
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "CSS3",
                Category = "Frontend",
                Level = 85,
                Icon = "devicon-css3-plain",
                DisplayOrder = 9
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "RxJS",
                Category = "Frontend",
                Level = 80,
                Icon = null,
                DisplayOrder = 10
            },
            
            // Bases de Datos
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "SQL Server",
                Category = "Database",
                Level = 85,
                Icon = "devicon-microsoftsqlserver-plain",
                DisplayOrder = 11
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "MongoDB",
                Category = "Database",
                Level = 80,
                Icon = "devicon-mongodb-plain",
                DisplayOrder = 12
            },
            
            // Herramientas
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "Git",
                Category = "Tools",
                Level = 85,
                Icon = "devicon-git-plain",
                DisplayOrder = 13
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "GitHub",
                Category = "Tools",
                Level = 85,
                Icon = "devicon-github-original",
                DisplayOrder = 14
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "Azure DevOps",
                Category = "Tools",
                Level = 80,
                Icon = "devicon-azure-plain",
                DisplayOrder = 15
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "Visual Studio",
                Category = "Tools",
                Level = 90,
                Icon = "devicon-visualstudio-plain",
                DisplayOrder = 16
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "Postman",
                Category = "Tools",
                Level = 85,
                Icon = null,
                DisplayOrder = 17
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "Swagger",
                Category = "Tools",
                Level = 80,
                Icon = null,
                DisplayOrder = 18
            },
            
            // Metodologías
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "Scrum",
                Category = "Methodology",
                Level = 85,
                Icon = null,
                DisplayOrder = 19
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "Ágil",
                Category = "Methodology",
                Level = 85,
                Icon = null,
                DisplayOrder = 20
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "Clean Code",
                Category = "Methodology",
                Level = 90,
                Icon = null,
                DisplayOrder = 21
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "SOLID",
                Category = "Methodology",
                Level = 85,
                Icon = null,
                DisplayOrder = 22
            },
            new Skill
            {
                Id = Guid.NewGuid(),
                Name = "Domain-Driven Design",
                Category = "Methodology",
                Level = 80,
                Icon = null,
                DisplayOrder = 23
            }
        };

            await _context.Skills.InsertManyAsync(skills);
            Console.WriteLine($"✅ {skills.Count} skills creadas");

            return skills.Count;
        }

        private async Task<int> SeedExperiencesAsync()
        {
            var existingExperiences = await _context.Experiences.CountDocumentsAsync(_ => true);
            if (existingExperiences > 0)
            {
                Console.WriteLine("⏭️  Experiencias ya existen, omitiendo seed");
                return 0;
            }

            var experiences = new List<Experience>
        {
            new Experience
            {
                Id = Guid.NewGuid(),
                Company = "IQ Outsourcing SAS",
                Position = "Analista de Desarrollo Senior",
                Description = "Liderazgo técnico en el diseño y desarrollo de soluciones empresariales complejas utilizando .NET Core y C#, definiendo arquitecturas escalables basadas en Clean Architecture y patrones de diseño avanzados.",
                Achievements = new List<string>
                {
                    "Desarrollo y optimización de APIs RESTful de alto rendimiento con estrategias de caché, paginación, versionado y seguridad mediante JWT y OAuth",
                    "Diseño de modelos de datos complejos en SQL Server, incluyendo optimización avanzada de consultas, índices y tuning de rendimiento",
                    "Implementación de soluciones híbridas con MongoDB para escenarios de alta disponibilidad, replicación y sharding",
                    "Desarrollo de aplicaciones frontend robustas con Angular, implementando arquitectura modular, lazy loading y gestión de estado con RxJS",
                    "Liderazgo de code reviews y mentoría de desarrolladores junior e intermedios",
                    "Participación activa en ceremonias ágiles, aportando perspectiva técnica en la estimación y planificación de sprints"
                },
                Technologies = new List<string>
                {
                    ".NET Core", "C#", "Angular", "TypeScript", "SQL Server", "MongoDB",
                    "Clean Architecture", "SOLID", "DDD", "Azure DevOps", "Git"
                },
                StartDate = new DateTime(2022, 11, 1),
                EndDate = new DateTime(2026, 1, 31),
                IsCurrentJob = false,
                DisplayOrder = 1
            },
            new Experience
            {
                Id = Guid.NewGuid(),
                Company = "IQ Outsourcing SAS",
                Position = "Analista de Desarrollo Intermedio",
                Description = "Desarrollo y mantenimiento de aplicaciones web utilizando .NET Framework y .NET Core, garantizando calidad mediante clean code y aplicación de patrones de diseño como Repository, Dependency Injection y Unit of Work.",
                Achievements = new List<string>
                {
                    "Diseño e implementación de APIs RESTful escalables y seguras siguiendo estándares de la industria",
                    "Modelado y administración de bases de datos relacionales en SQL Server y soluciones NoSQL con MongoDB",
                    "Desarrollo de interfaces de usuario interactivas y responsivas con Angular, TypeScript, HTML5 y CSS3",
                    "Colaboración con equipos multidisciplinarios en metodologías ágiles para entrega oportuna de proyectos",
                    "Promoción a Analista Senior en 1 año 3 meses por desempeño excepcional"
                },
                Technologies = new List<string>
                {
                    ".NET Framework", ".NET Core", "C#", "Angular", "SQL Server", "MongoDB",
                    "Repository Pattern", "Scrum"
                },
                StartDate = new DateTime(2021, 6, 1),
                EndDate = new DateTime(2022, 10, 31),
                IsCurrentJob = false,
                DisplayOrder = 2
            },
            new Experience
            {
                Id = Guid.NewGuid(),
                Company = "LabCoreSoft",
                Position = "Implementador de Aplicaciones",
                Description = "Creación y ejecución de planes de pruebas, instalación de aplicativos en ambientes de producción y elaboración de documentación técnica.",
                Achievements = new List<string>
                {
                    "Elaboración de manuales de usuario, instalación y procedimientos técnicos",
                    "Participación en análisis de procesos y transferencia de conocimiento a mesa de soporte",
                    "Instalación y configuración de aplicaciones corporativas en ambientes productivos"
                },
                Technologies = new List<string>
                {
                    ".NET", "SQL Server", "Windows Server"
                },
                StartDate = new DateTime(2021, 1, 1),
                EndDate = new DateTime(2021, 5, 31),
                IsCurrentJob = false,
                DisplayOrder = 3
            },
            new Experience
            {
                Id = Guid.NewGuid(),
                Company = "LabCoreSoft",
                Position = "Analista de Soporte de Aplicaciones",
                Description = "Instalación, configuración y mantenimiento de aplicaciones corporativas. Detección y resolución de averías en sistemas, diagnóstico de malfuncionamiento de hardware y software.",
                Achievements = new List<string>
                {
                    "Detección y resolución de averías en sistemas empresariales",
                    "Diagnóstico de malfuncionamiento de hardware y software",
                    "Apoyo en pruebas de calidad de productos de software y revisiones de seguridad"
                },
                Technologies = new List<string>
                {
                    ".NET", "SQL Server", "Windows Server", "Active Directory"
                },
                StartDate = new DateTime(2018, 10, 1),
                EndDate = new DateTime(2020, 12, 31),
                IsCurrentJob = false,
                DisplayOrder = 4
            }
        };

            await _context.Experiences.InsertManyAsync(experiences);
            Console.WriteLine($"✅ {experiences.Count} experiencias creadas");

            return experiences.Count;
        }

        private async Task<int> SeedProjectsAsync()
        {
            var existingProjects = await _context.Projects.CountDocumentsAsync(_ => true);
            if (existingProjects > 0)
            {
                Console.WriteLine("⏭️  Proyectos ya existen, omitiendo seed");
                return 0;
            }

            var projects = new List<Project>
        {
            new Project
            {
                Id = Guid.NewGuid(),
                Title = "Portfolio API - Clean Architecture",
                Description = "API RESTful desarrollada con .NET Core 8 y MongoDB Atlas, implementando Clean Architecture, SOLID principles, y JWT authentication. Incluye gestión completa de proyectos, habilidades, experiencia laboral y mensajes de contacto.",
                Technologies = new List<string>
                {
                    ".NET Core 8", "C#", "MongoDB", "Clean Architecture",
                    "JWT", "Swagger", "SOLID", "Repository Pattern"
                },
                ImageUrl = null,
                GithubUrl = "https://github.com/jonathanaldana",
                LiveUrl = null,
                StartDate = new DateTime(2026, 2, 16),
                EndDate = null,
                IsFeatured = true,
                DisplayOrder = 1
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Title = "Sistema ERP Empresarial",
                Description = "Sistema integral de planificación de recursos empresariales desarrollado con .NET Core, Angular y SQL Server. Implementa módulos de inventario, facturación, compras, ventas y reportería avanzada.",
                Technologies = new List<string>
                {
                    ".NET Core", "C#", "Angular", "TypeScript", "SQL Server",
                    "Entity Framework Core", "Clean Architecture"
                },
                ImageUrl = null,
                GithubUrl = null,
                LiveUrl = null,
                StartDate = new DateTime(2023, 1, 1),
                EndDate = new DateTime(2023, 12, 31),
                IsFeatured = true,
                DisplayOrder = 2
            },
            new Project
            {
                Id = Guid.NewGuid(),
                Title = "API de Gestión de Usuarios",
                Description = "API REST escalable para gestión de usuarios, roles y permisos con autenticación JWT, refresh tokens, y políticas de seguridad avanzadas. Implementa rate limiting, audit logging y encriptación de datos sensibles.",
                Technologies = new List<string>
                {
                    ".NET Core", "C#", "MongoDB", "JWT", "BCrypt",
                    "Rate Limiting", "CORS", "Swagger"
                },
                ImageUrl = null,
                GithubUrl = null,
                LiveUrl = null,
                StartDate = new DateTime(2024, 3, 1),
                EndDate = new DateTime(2024, 8, 31),
                IsFeatured = true,
                DisplayOrder = 3
            }
        };

            await _context.Projects.InsertManyAsync(projects);
            Console.WriteLine($"✅ {projects.Count} proyectos creados");

            return projects.Count;
        }

        private async Task<int> SeedProfileAsync()
        {
            var existingProfile = await _context.Profiles.CountDocumentsAsync(_ => true);
            if (existingProfile > 0)
            {
                Console.WriteLine("⏭️  Perfil ya existe, omitiendo seed");
                return 0;
            }

            var profile = new Profile
            {
                Id = Guid.NewGuid(),
                FullName = "Jonathan Fabián Aldana Torres",
                Title = "Desarrollador Senior Full Stack | .NET & Angular",
                Email = "jonathanaldana9@hotmail.com",
                Phone = "+573227139473",
                Location = "Bogotá, D.C., Colombia",
                PhotoUrl = "https://i.imgur.com/YZGCYQA.jpeg",
                Bio = "Desarrollador Senior Full Stack con más de 5 años de experiencia en el ecosistema .NET y tecnologías modernas de desarrollo web. Especializado en diseño, desarrollo e implementación de soluciones empresariales escalables aplicando Clean Architecture y principios de código limpio.",
                AvailableToJob = true,
                SocialLinks = new ProfileSocialLinks
                {
                    LinkedIn = "https://linkedin.com/in/jonathan-aldana",
                    GitHub = "https://github.com/jonathanaldana",
                    Twitter = null,
                    Website = null,
                    Email = "_blank",
                    Whatsapp = "https://api.whatsapp.com/send/?phone=573227139473&text=https%3A%2F%2FJonathan-Aldana%2F%0A%0ABuen+d%C3%ADa+estoy+interesado+en+contactar+para...+&type=phone_number&app_absent=0"
                }
            };

            await _context.Profiles.InsertOneAsync(profile);
            Console.WriteLine("✅ 1 perfil creado");

            return 1;
        }
    }
}