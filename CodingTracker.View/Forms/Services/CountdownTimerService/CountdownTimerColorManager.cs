using Guna.UI2.WinForms;

namespace CodingTracker.View.Forms.Services.CountdownTimerService.CountdownTimerColorManagers
{
    public interface ICountdownTimerColorManager
    {
        void ApplyColorTransitionToGuna2HtmlLabel(Guna2HtmlLabel label, Color fromColor, Color toColor);

        (Color MainColor, Color SecondaryColor) GetProgressColors(double progress);
    }

    public  class CountdownTimerColorManager : ICountdownTimerColorManager
    {




        /// The method creates a timer with a 5ms interval, which will update the color multiple times per second.
        public void ApplyColorTransitionToGuna2HtmlLabel(Guna2HtmlLabel label, Color fromColor, Color toColor)
        {
            System.Windows.Forms.Timer transitionTimer = new System.Windows.Forms.Timer();
            transitionTimer.Interval = 5;

            int steps = 10;
            int currentStep = 0;

            transitionTimer.Tick += (s, e) =>
            {
                currentStep++;
                double progress = (double)currentStep / steps;

                int r = (int)(fromColor.R + (toColor.R - fromColor.R) * progress);
                int g = (int)(fromColor.G + (toColor.G - fromColor.G) * progress);
                int b = (int)(fromColor.B + (toColor.B - fromColor.B) * progress);

                label.ForeColor = Color.FromArgb(r, g, b);

                if (currentStep >= steps)
                {
                    transitionTimer.Stop();
                    transitionTimer.Dispose();
                    label.ForeColor = toColor;
                }
            };

            transitionTimer.Start();
        }



        public (Color MainColor, Color SecondaryColor) GetProgressColors(double progress)
        {

            Color pinkColor = Color.FromArgb(255, 81, 195);    // Starting vibrant pink
            Color blueColor = Color.FromArgb(168, 228, 255);   // Light blue transition
            Color purpleColor = Color.FromArgb(108, 99, 180);  // Purple (from 6-9 o'clock in image)
            Color greenColor = Color.FromArgb(83, 179, 129);   // Green (from image, at start TimerLocation)

            Color mainColor, secondaryColor;

            if (progress < 0.25) // First quadrant (0-3 o'clock)
            {
                // From pink to blue
                double adjustedProgress = progress * 4;
                mainColor = InterpolateColor(pinkColor, blueColor, adjustedProgress);
                secondaryColor = AdjustBrightness(mainColor, 0.9);
            }
            else if (progress < 0.5) // Second quadrant (3-6 o'clock)
            {
                // From blue to purple
                double adjustedProgress = (progress - 0.25) * 4;
                mainColor = InterpolateColor(blueColor, purpleColor, adjustedProgress);
                secondaryColor = AdjustBrightness(mainColor, 0.9); // Slightly darker
            }
            else if (progress < 0.75)
            {
                // Maintain purple color in this region
                mainColor = purpleColor;
                secondaryColor = AdjustBrightness(purpleColor, 0.9);
            }
            else
            {
                // From purple to green (completing the circle)
                double adjustedProgress = (progress - 0.75) * 4;
                mainColor = InterpolateColor(purpleColor, greenColor, adjustedProgress);
                secondaryColor = AdjustBrightness(mainColor, 1.1);
            }

            return (mainColor, secondaryColor);
        }

        /// <summary>
        /// Helper method to interpolate between two colors
        private Color InterpolateColor(Color fromColor, Color toColor, double progress)
        {
            int r = (int)(fromColor.R + (toColor.R - fromColor.R) * progress);
            int g = (int)(fromColor.G + (toColor.G - fromColor.G) * progress);
            int b = (int)(fromColor.B + (toColor.B - fromColor.B) * progress);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Helper method to adjust brightness of a color

        private Color AdjustBrightness(Color color, double factor)
        {
            return Color.FromArgb(
                Math.Min(255, (int)(color.R * factor)),
                Math.Min(255, (int)(color.G * factor)),
                Math.Min(255, (int)(color.B * factor))
            );
        }


    }
}


