using Guna.UI2.WinForms;


namespace CodingTracker.View.Forms.Services.SharedFormServices
{
    public interface IButtonHighlighterService
    {
        void HighlightButtonWithHoverColour(Guna2GradientButton button);
        void SetFillColorToTransparent(Guna2GradientButton button);
        void FillButtonWithBrightPink(Guna2GradientButton button);

        void TestFillButton(Guna2GradientButton button);

        void SetButtonHoverColors(Guna2GradientButton button);
        void SetButtonBackColorAndBorderColor(Guna2GradientButton button);
    }
    public class ButtonHighlighterService : IButtonHighlighterService
    {
        private Color LastHoverFillColor { get; }
        private Color LastHoverFillColor2 { get; }


        public void UpdateLastHoverFillColors(Guna2GradientButton button)
        {

        }

        public void HighlightButtonWithHoverColour(Guna2GradientButton button)
        {
            button.FillColor = Color.FromArgb(94, 148, 255);   // Medium blue shade 
            button.FillColor2 = Color.FromArgb(64, 224, 208);  // Pink/magenta shade 
        }

        public void SetFillColorToTransparent(Guna2GradientButton button)
        {
            button.FillColor = Color.Transparent;
            button.FillColor2 = Color.Transparent;
        }

        public void FillButtonWithBrightPink(Guna2GradientButton button)
        {
            button.FillColor = Color.FromArgb(175, 30, 130);
            button.FillColor2 = Color.FromArgb(175, 30, 130);
        }


        public void TestFillButton(Guna2GradientButton button)
        {
            button.FillColor = Color.FromArgb(255, 81, 195);
            button.FillColor2 = Color.FromArgb(168, 228, 255);
        }


        public void SetButtonHoverColors(Guna2GradientButton button)
        {
            button.HoverState.FillColor = Color.FromArgb(94, 148, 255);
            button.HoverState.FillColor2 = Color.FromArgb(64, 224, 208);
        }

        public void SetButtonBackColorAndBorderColor(Guna2GradientButton button)
        {
            button.BorderColor = Color.FromArgb(100, 255, 100, 200);  // Light pink glow
            button.BorderThickness = 1;
        }

    }
}
