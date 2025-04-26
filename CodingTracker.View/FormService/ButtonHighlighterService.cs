using Guna.UI2.WinForms;


namespace CodingTracker.View.FormService.ButtonHighlighterServices
{
    public interface IButtonHighlighterService
    {
        void HighlightButtonWithHoverColour(Guna2GradientButton button);
        void SetFillColorToTransparent(Guna2GradientButton button);
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
            button.FillColor2 = Color.FromArgb(255, 77, 165);  // Pink/magenta shade 
        }

        public void SetFillColorToTransparent(Guna2GradientButton button)
        {
            button.FillColor = Color.Transparent;  
            button.FillColor2 = Color.Transparent;
        }


    }
}
