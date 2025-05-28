using System.Drawing;

namespace CodingTracker.View.FormService.ColourServices
{
    public static class ColorService
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
        public static Color RowDeletionCrimsonRed => Color.FromArgb(220, 20, 60);




        // MainPage colors

        public static Color MainPagePanelPurple => Color.FromArgb(100, 90, 210);
        public static Color MainPagePanelAqua => Color.FromArgb(110, 213, 228);

        public static Color MainPagePanelVioletPurple => Color.FromArgb(170, 116, 243);
        public static Color MainPagePanelPink => Color.FromArgb(252, 124, 180);
        public static Color MainPagePanelTurquoise => Color.Turquoise;
        public static Color MainPagePanelLightPink => Color.FromArgb(242, 130, 220);


        // MainPAge ColorPanels

        // Session gradient colors
        public static Color SessionSlateStart => Color.FromArgb(40, 40, 60);
        public static Color SessionSlateEnd => Color.FromArgb(30, 30, 50);

        public static Color SessionBlueStart => Color.FromArgb(100, 200, 250);    // Blue-Cyan blend
        public static Color SessionBlueEnd => Color.FromArgb(100, 200, 250);

        public static Color SessionRoseStart => Color.FromArgb(150, 180, 240);    // Purple-Blue blend
        public static Color SessionRoseEnd => Color.FromArgb(150, 180, 240);

        public static Color SessionAmberStart => Color.FromArgb(200, 150, 220);   // Pink-Purple blend
        public static Color SessionAmberEnd => Color.FromArgb(200, 150, 220);

        public static Color SessionCoralStart => Color.FromArgb(255, 120, 200);   // Light Pink
        public static Color SessionCoralEnd => Color.FromArgb(255, 120, 200);

        public static Color SessionEmeraldStart => Color.FromArgb(255, 81, 195);  // Primary Pink
        public static Color SessionEmeraldEnd => Color.FromArgb(255, 81, 195);




    }
}
