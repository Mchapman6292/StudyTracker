

namespace CodingTracker.View.FormService.ColourServices
{
    public static class ColourService
    {
        // Base colors
        public static Color Teal => Color.FromArgb(0, 128, 128);
        public static Color LightTeal => Color.FromArgb(0, 180, 180);
        public static Color White => Color.White;

        // Dark theme colors
        public static Color DarkGrey => Color.FromArgb(64, 63, 79);
        public static Color MediumGrey => Color.FromArgb(128, 127, 145);
        public static Color LightGrey => Color.FromArgb(200, 200, 220);

        // Selection and highlight colors
        public static Color DarkerGrey => Color.FromArgb(35, 34, 50); // Primary background/display colour.
        public static Color CrimsonRed => Color.FromArgb(220, 20, 60);
    }
}
