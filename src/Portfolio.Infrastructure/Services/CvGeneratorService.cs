using Portfolio.Application.Interfaces.Services;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Pdf;
using Portfolio.Infrastructure.Pdf.Components;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Portfolio.Infrastructure.Services
{
    public class CvGeneratorService : ICvGeneratorService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CvGeneratorService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<byte[]> GenerateCv(
            Profile profile,
            IEnumerable<Experience> experiences,
            IEnumerable<Skill> skills,
            IEnumerable<Project> projects,
            IEnumerable<Education> educations)
        {
            // 1. OBTENCIÓN DE FOTO ASÍNCRONA (Drive, LinkedIn o Directa)
            byte[]? photoBytes = await DownloadProfileImageAsync(profile.PhotoUrl);

            // 2. GENERACIÓN DEL DOCUMENTO QUESTPDF
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(0);
                    page.DefaultTextStyle(x => x.FontFamily(Fonts.Verdana).FontSize(10));

                    page.Content().Row(row =>
                    {
                        // --- COLUMNA IZQUIERDA (SIDEBAR) ---
                        row.ConstantItem(200).Background(CvStyles.DarkColor).Padding(20).Column(sidebar =>
                        {
                            // Avatar circular con lógica de fallback integrada
                            sidebar.Item().PaddingBottom(15).AlignCenter().Avatar(photoBytes, profile.FullName);

                            // Identidad en Sidebar
                            sidebar.Item().AlignCenter().Text(profile.FullName.ToUpper())
                                .FontSize(14).Bold().FontColor(Colors.White);

                            sidebar.Item().PaddingTop(3).AlignCenter().Text(profile.Title)
                                .FontSize(8).FontColor(Colors.Grey.Lighten2).LineHeight(1.2f).AlignCenter();

                            // Sección de Contacto
                            sidebar.Item().PaddingTop(15).TitleSection("CONTACTO", isDark: true);
                            sidebar.Item().IconText("✉", profile.Email, Colors.White);
                            sidebar.Item().PaddingTop(4).IconText("📞", profile.Phone, Colors.White);
                            sidebar.Item().PaddingTop(4).IconText("📍", profile.Location, Colors.White);

                            if (!string.IsNullOrEmpty(profile.SocialLinks?.LinkedIn))
                                sidebar.Item().PaddingTop(4).IconText("🔗", "LinkedIn", Colors.White);

                            if (!string.IsNullOrEmpty(profile.SocialLinks?.GitHub))
                                sidebar.Item().PaddingTop(4).IconText("💻", "GitHub", Colors.White);

                            // Habilidades con lógica de agrupación (Backend, Frontend, etc.)
                            sidebar.Item().PaddingTop(20).TitleSection("HABILIDADES", isDark: true);
                            sidebar.Item().SkillsGrouped(skills);
                        });

                        // --- COLUMNA DERECHA (CONTENIDO PRINCIPAL) ---
                        row.RelativeItem().Padding(30).Column(main =>
                        {
                            // Perfil Profesional / Bio
                            main.Item().TitleSection("PERFIL PROFESIONAL");
                            main.Item().PaddingBottom(15).Text(profile.Bio)
                                .FontSize(9)
                                .LineHeight(1.5f)
                                .Justify()
                                .FontColor(CvStyles.DarkColor);

                            // Experiencia Laboral (Ordenada por DisplayOrder o Fecha)
                            main.Item().PaddingTop(10).TitleSection("EXPERIENCIA PROFESIONAL");
                            foreach (var exp in experiences.OrderBy(e => e.DisplayOrder))
                            {
                                main.Item().ExperienceCard(exp);
                            }

                            // Educación
                            if (educations != null && educations.Any())
                            {
                                main.Item().PaddingTop(20).TitleSection("EDUCACIÓN");
                                foreach (var edu in educations.OrderBy(e => e.DisplayOrder))
                                {
                                    main.Item().EducationCard(edu);
                                }
                            }

                            // Proyectos Destacados en Grid de 2 columnas
                            main.Item().PaddingTop(20).TitleSection("PROYECTOS DESTACADOS");
                            main.Item().Grid(grid =>
                            {
                                grid.Columns(2);
                                grid.Spacing(10);

                                foreach (var proj in projects.Where(p => p.IsFeatured).OrderBy(p => p.DisplayOrder))
                                {
                                    grid.Item().ProjectCard(proj);
                                }
                            });
                        });
                    });

                    // Footer Profesional
                    page.Footer().Height(25).Background(CvStyles.VeryLightGray)
                        .AlignCenter().AlignMiddle()
                        .Text($"CV generado con Portfolio Professional • {DateTime.Now:dd/MM/yyyy}")
                        .FontSize(7).FontColor(CvStyles.MediumGray);
                });
            }).GeneratePdf();
        }

        private async Task<byte[]?> DownloadProfileImageAsync(string? url)
        {
            if (string.IsNullOrEmpty(url)) return null;

            try
            {
                var client = _httpClientFactory.CreateClient("ProfileImages");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36");

                // Timeout de 10 segundos para evitar bloqueos
                client.Timeout = TimeSpan.FromSeconds(10);

                string finalUrl = NormalizeGoogleDriveUrl(url);

                var response = await client.GetAsync(finalUrl);

                if (response.IsSuccessStatusCode)
                {
                    var contentType = response.Content.Headers.ContentType?.MediaType;

                    // Verificar que sea realmente una imagen
                    if (contentType?.StartsWith("image/") == true)
                    {
                        return await response.Content.ReadAsByteArrayAsync();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                // Log opcional (puedes agregar ILogger si lo deseas)
                Console.WriteLine($"Error descargando imagen: {ex.Message}");
                return null;
            }
        }

        private string NormalizeGoogleDriveUrl(string url)
        {
            // Si ya es una URL de descarga directa, usarla tal cual
            if (url.Contains("drive.usercontent.google.com/uc?id=") ||
                url.Contains("drive.google.com/uc?") ||
                url.Contains("export=download"))
            {
                return url;
            }

            // Si es formato de vista (/file/d/{id}/view)
            if (url.Contains("drive.google.com/file/d/"))
            {
                var fileId = url.Split("/file/d/")[1].Split("/")[0];
                return $"https://drive.usercontent.google.com/uc?id={fileId}&export=download";
            }

            // Si es formato abierto (/open?id={id})
            if (url.Contains("drive.google.com/open?id="))
            {
                var fileId = url.Split("id=")[1].Split("&")[0];
                return $"https://drive.usercontent.google.com/uc?id={fileId}&export=download";
            }

            // Cualquier otra URL, retornar tal cual
            return url;
        }
    }
}