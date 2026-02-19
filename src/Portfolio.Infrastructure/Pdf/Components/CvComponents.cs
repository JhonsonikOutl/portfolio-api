using Portfolio.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Portfolio.Infrastructure.Pdf.Components
{
    public static class CvComponents
    {
        public static void IconText(this IContainer container, string icon, string text, string color)
        {
            container.Row(row =>
            {
                row.ConstantItem(15).Text(icon).FontSize(10).FontColor(color);
                row.RelativeItem().Text(text).FontSize(8).FontColor(color);
            });
        }

        public static void SkillBar(this IContainer container, string name, int level)
        {
            container.PaddingBottom(8).Column(col =>
            {
                col.Item().Row(row =>
                {
                    row.RelativeItem().Text(name).FontSize(8).FontColor(Colors.White);
                    row.ConstantItem(30).AlignRight().Text($"{level}%").FontSize(7).FontColor(Colors.White).Light();
                });
                col.Item().PaddingTop(2).Height(3).Background("#334155").Row(row =>
                {
                    row.RelativeItem(level).Height(3).Background(CvStyles.PrimaryColor);
                    row.RelativeItem(100 - level).Height(3).Background(Colors.Transparent);
                });
            });
        }

        public static void TitleSection(this IContainer container, string title, bool isDark = false)
        {
            var color = isDark ? Colors.White : CvStyles.PrimaryColor;
            container.PaddingBottom(10).Column(col =>
            {
                col.Item().Text(title).FontSize(10).Bold().FontColor(color);
                col.Item().PaddingTop(2).Height(1).Background(color);
            });
        }

        public static void Badge(this IContainer container, string text)
        {
            container.PaddingRight(4).PaddingBottom(4)
                     .Border(1).BorderColor(CvStyles.AccentColor)
                     .PaddingHorizontal(5).PaddingVertical(1)
                     .Text(text).FontSize(7).FontColor(CvStyles.AccentColor);
        }

        public static void Avatar(this IContainer container, byte[]? photoBytes, string fallbackName)
        {
            container.Width(100).Height(100).Column(col =>
            {
                if (photoBytes != null && photoBytes.Length > 0)
                {
                    col.Item().Width(100).Height(100).Image(photoBytes, ImageScaling.FitArea);
                }
                else
                {
                    col.Item().Width(100).Height(100)
                        .Background(CvStyles.PrimaryColor)
                        .AlignCenter().AlignMiddle()
                        .Text(GetInitials(fallbackName))
                        .FontSize(32)
                        .FontColor(Colors.White)
                        .Bold();
                }
            });
        }

        public static void ExperienceCard(this IContainer container, Experience exp)
        {
            container.PaddingBottom(15).Column(col =>
            {
                // Header
                col.Item().Row(row =>
                {
                    row.RelativeItem().Column(info =>
                    {
                        info.Item().Text(exp.Position).Bold().FontSize(11).FontColor(CvStyles.DarkColor);
                        info.Item().Text(exp.Company).FontColor(CvStyles.PrimaryColor).Medium().FontSize(9);
                    });

                    row.ConstantItem(100).AlignRight().Column(date =>
                    {
                        var badge = exp.IsCurrentJob ? "#10B981" : CvStyles.MediumGray.ToString();
                        date.Item().Background(badge).Padding(5)
                            .AlignCenter().Text($"{exp.StartDate:MMM yyyy} - {(exp.IsCurrentJob ? "Presente" : exp.EndDate?.ToString("MMM yyyy"))}")
                            .FontSize(7).FontColor(Colors.White).Bold();
                    });
                });

                // Descripción
                col.Item().PaddingTop(6).Text(exp.Description)
                    .FontSize(9)
                    .FontColor(CvStyles.MediumGray)
                    .LineHeight(1.3f);

                // Logros
                if (exp.Achievements?.Any() == true)
                {
                    col.Item().PaddingTop(8).Column(achievements =>
                    {
                        foreach (var achievement in exp.Achievements.Take(3))
                        {
                            achievements.Item().PaddingBottom(3).Row(item =>
                            {
                                item.ConstantItem(12).Text("✓").FontSize(8).FontColor("#10B981").Bold();
                                item.RelativeItem().Text(achievement).FontSize(8).FontColor(CvStyles.DarkColor).LineHeight(1.2f);
                            });
                        }
                    });
                }

                // Tecnologías
                if (exp.Technologies?.Any() == true)
                {
                    col.Item().PaddingTop(6).Text(text =>
                    {
                        text.Span("Tech: ").FontSize(7).Bold().FontColor(CvStyles.MediumGray);
                        text.Span(string.Join(" • ", exp.Technologies.Take(8))).FontSize(7).FontColor(CvStyles.MediumGray);
                    });
                }
            });
        }

        public static void ProjectCard(this IContainer container, Project proj)
        {
            container.Background(CvStyles.VeryLightGray).Padding(10).Column(c =>
            {
                c.Item().Text(proj.Title).Bold().FontSize(9).FontColor(CvStyles.PrimaryColor);

                var shortDesc = proj.Description.Length > 120
                    ? proj.Description.Substring(0, 120) + "..."
                    : proj.Description;

                c.Item().PaddingTop(4).Text(shortDesc)
                    .FontSize(8)
                    .FontColor(CvStyles.MediumGray)
                    .LineHeight(1.2f);

                if (proj.Technologies?.Any() == true)
                {
                    c.Item().PaddingTop(6).Text(text =>
                    {
                        text.Span(string.Join(" • ", proj.Technologies.Take(5)))
                            .FontSize(6)
                            .FontColor(CvStyles.AccentColor);
                    });
                }
            });
        }

        public static void SkillsGrouped(this IContainer container, IEnumerable<Skill> skills)
        {
            var grouped = skills.GroupBy(s => s.Category).OrderBy(g => g.Key);

            container.Column(col =>
            {
                foreach (var group in grouped.Take(4)) // Solo 4 categorías
                {
                    col.Item().PaddingBottom(12).Column(catCol =>
                    {
                        catCol.Item().Text(group.Key)
                            .FontSize(8)
                            .Bold()
                            .FontColor(Colors.Grey.Lighten2);

                        catCol.Item().PaddingTop(6).Column(skillsList =>
                        {
                            foreach (var skill in group.OrderByDescending(s => s.Level).Take(5))
                            {
                                skillsList.Item().SkillBar(skill.Name, skill.Level);
                            }
                        });
                    });
                }
            });
        }

        public static void EducationCard(this IContainer container, Education edu)
        {
            container.PaddingBottom(12).Column(col =>
            {
                // Header
                col.Item().Row(row =>
                {
                    row.RelativeItem().Column(info =>
                    {
                        info.Item().Text(edu.Degree).Bold().FontSize(10).FontColor(CvStyles.DarkColor);
                        info.Item().Text(edu.FieldOfStudy).FontSize(9).FontColor(CvStyles.AccentColor);
                        info.Item().Text(edu.Institution).FontSize(8).FontColor(CvStyles.MediumGray).Italic();
                    });

                    row.ConstantItem(80).AlignRight().Text($"{edu.StartDate:yyyy} - {(edu.IsCurrentlyStudying ? "Presente" : edu.EndDate?.Year.ToString() ?? "")}")
                        .FontSize(8).FontColor(CvStyles.MediumGray);
                });

                // Descripción (si existe)
                if (!string.IsNullOrEmpty(edu.Description))
                {
                    col.Item().PaddingTop(4).Text(edu.Description)
                        .FontSize(8)
                        .FontColor(CvStyles.MediumGray)
                        .LineHeight(1.2f);
                }
            });
        }

        // Helper para obtener iniciales
        private static string GetInitials(string fullName)
        {
            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
                return $"{parts[0][0]}{parts[^1][0]}".ToUpper();
            if (parts.Length == 1 && parts[0].Length >= 2)
                return parts[0].Substring(0, 2).ToUpper();
            return "CV";
        }
    }
}
