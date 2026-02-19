using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Infrastructure.Data;
using MongoDB.Driver;

namespace Portfolio.API.Controllers
{
    /// <summary>
    /// Controller para poblar la base de datos con datos iniciales.
    /// SOLO DESARROLLO
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public SeedController(MongoDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SeedDatabase()
        {
            try
            {
                var seeder = new DataSeeder(_context);
                var result = await seeder.SeedAllAsync();

                return Ok(new
                {
                    message = "✅ Base de datos poblada correctamente",
                    summary = new
                    {
                        totalCreated = result.TotalCreated,
                        message = $"{result.TotalCreated} documentos creados en total"
                    },
                    details = new
                    {
                        profile = $"{result.ProfileCreated} perfil creado",
                        skills = $"{result.SkillsCreated} habilidades creadas",
                        experiences = $"{result.ExperiencesCreated} experiencias laborales creadas",
                        projects = $"{result.ProjectsCreated} proyectos creados"
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "❌ Error al poblar la base de datos",
                    error = ex.Message
                });
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> ClearDatabase()
        {
            try
            {
                await _context.Profiles.DeleteManyAsync(_ => true);
                await _context.Skills.DeleteManyAsync(_ => true);
                await _context.Experiences.DeleteManyAsync(_ => true);
                await _context.Projects.DeleteManyAsync(_ => true);
                await _context.ContactMessages.DeleteManyAsync(_ => true);

                return Ok(new
                {
                    message = "✅ Base de datos limpiada (usuarios mantenidos)"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "❌ Error al limpiar la base de datos",
                    error = ex.Message
                });
            }
        }
    }
}