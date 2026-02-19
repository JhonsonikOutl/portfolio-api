using QuestPDF.Infrastructure;

namespace Portfolio.Infrastructure.Pdf
{
    internal static class CvStyles
    {
        public static string PrimaryColorHex = "#6366F1";
        public static Color PrimaryColor = Color.FromHex(PrimaryColorHex);
        public static Color DarkColor = Color.FromHex("#1E293B");
        public static Color AccentColor = Color.FromHex("#3B82F6");
        public static Color MediumGray = Color.FromHex("#64748B");
        public static Color LightGray = Color.FromHex("#E2E8F0");
        public static Color VeryLightGray = Color.FromHex("#F8FAFC");
    }
}