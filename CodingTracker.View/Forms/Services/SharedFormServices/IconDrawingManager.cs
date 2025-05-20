using Guna.UI2.WinForms;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CodingTracker.View.Forms.Services.SharedFormServices.IconDrawingManager
{

    public interface IIconDrawingManager
    {
        void AddPauseIcon(Guna2GradientButton button, Color? iconColor = null, bool addBorder = true, Color? borderColor = null);
    }

    public class IconDrawingManager : IIconDrawingManager
    {


        /// <summary>
        /// Adds a pause icon to a Guna2GradientButton with proper centering and optional border
        /// </summary>
        /// <param name="button">The button to add the pause icon to</param>
        /// <param name="iconColor">Color of the icon (default: white)</param>
        /// <param name="addBorder">Whether to add a contrasting border</param>
        /// <param name="borderColor">Color of the border (default: black)</param>
        public void AddPauseIcon(Guna2GradientButton button, Color? iconColor = null, bool addBorder = true, Color? borderColor = null)
        {
            // Clear any existing shapes from the button
            ClearShapesFromButton(button);

            Color fillColor = iconColor ?? Color.White;
            Color outlineColor = borderColor ?? Color.Black;

            // Calculate center positions based on button size
            int centerX = button.Width / 2;
            int centerY = button.Height / 2;

            // Dimensions for the pause bars
            int barWidth = 4;
            int barHeight = 10;
            int barSpacing = 4;

            // Calculate positions to center the pause icon
            int leftBarX = centerX - barWidth - barSpacing / 2;
            int rightBarX = centerX + barSpacing / 2;
            int barY = centerY - barHeight / 2;

            // If adding a border, create outline bars first (slightly larger)
            if (addBorder)
            {
                // Left bar outline
                Guna2Shapes leftBarOutline = new Guna2Shapes();
                leftBarOutline.Size = new Size(barWidth + 2, barHeight + 2);
                leftBarOutline.Location = new Point(leftBarX - 1, barY - 1);
                leftBarOutline.BorderThickness = 0;
                leftBarOutline.FillColor = outlineColor;
                leftBarOutline.Shape = Guna.UI2.WinForms.Enums.ShapeType.Rectangle;
                leftBarOutline.BackColor = Color.Transparent;

                // Right bar outline
                Guna2Shapes rightBarOutline = new Guna2Shapes();
                rightBarOutline.Size = new Size(barWidth + 2, barHeight + 2);
                rightBarOutline.Location = new Point(rightBarX - 1, barY - 1);
                rightBarOutline.BorderThickness = 0;
                rightBarOutline.FillColor = outlineColor;

                rightBarOutline.Shape = Guna.UI2.WinForms.Enums.ShapeType.Rectangle;
                rightBarOutline.BackColor = Color.Transparent;

                // Add the outlines first (so they appear behind the main shapes)
                button.Controls.Add(leftBarOutline);
                button.Controls.Add(rightBarOutline);
            }

            // Create left pause bar
            Guna2Shapes leftBar = new Guna2Shapes();
            leftBar.Size = new Size(barWidth, barHeight);
            leftBar.Location = new Point(leftBarX, barY);
            leftBar.BorderThickness = 0;
            leftBar.FillColor = fillColor;
            leftBar.Shape = Guna.UI2.WinForms.Enums.ShapeType.Rectangle;
            leftBar.BackColor = Color.Transparent;

            // Create right pause bar
            Guna2Shapes rightBar = new Guna2Shapes();
            rightBar.Size = new Size(barWidth, barHeight);
            rightBar.Location = new Point(rightBarX, barY);
            rightBar.BorderThickness = 0;
            rightBar.FillColor = fillColor;
            rightBar.Shape = Guna.UI2.WinForms.Enums.ShapeType.Rectangle;
            rightBar.BackColor = Color.Transparent;

            // Add the pause bars on top
            button.Controls.Add(leftBar);
            button.Controls.Add(rightBar);
        }

        // Helper method to remove existing shapes from a button
        public void ClearShapesFromButton(Control button)
        {
            // Create a list to hold controls to remove
            List<Control> controlsToRemove = new List<Control>();

            // Find all Guna2Shapes controls
            foreach (Control control in button.Controls)
            {
                if (control is Guna2Shapes)
                {
                    controlsToRemove.Add(control);
                }
            }

            // Remove the controls
            foreach (Control control in controlsToRemove)
            {
                button.Controls.Remove(control);
                control.Dispose();
            }
        }
    }
}