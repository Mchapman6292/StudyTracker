using Guna.UI2.WinForms;


namespace CodingTracker.View.Forms.Services.SharedFormServices
{
    public interface IButtonHighlighterService
    {


        void SetButtonHoverColors(Guna2GradientButton button);
        void SetButtonBackColorAndBorderColor(Guna2GradientButton button);
    }
    public class ButtonHighlighterService : IButtonHighlighterService
    {





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
