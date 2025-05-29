namespace CodingTracker.View
{
    partial class WaveVisualizationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Button increaseIntensityButton;
        private Button decreaseIntensityButton;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            increaseIntensityButton = new Button();
            decreaseIntensityButton = new Button();
            SuspendLayout();
            // 
            // increaseIntensityButton
            // 
            increaseIntensityButton.BackColor = Color.FromArgb(70, 71, 117);
            increaseIntensityButton.FlatStyle = FlatStyle.Flat;
            increaseIntensityButton.ForeColor = Color.White;
            increaseIntensityButton.Location = new Point(700, 20);
            increaseIntensityButton.Name = "increaseIntensityButton";
            increaseIntensityButton.Size = new Size(75, 30);
            increaseIntensityButton.TabIndex = 0;
            increaseIntensityButton.Text = "Increase";
            increaseIntensityButton.UseVisualStyleBackColor = false;
            // 
            // decreaseIntensityButton
            // 
            decreaseIntensityButton.BackColor = Color.FromArgb(70, 71, 117);
            decreaseIntensityButton.FlatStyle = FlatStyle.Flat;
            decreaseIntensityButton.ForeColor = Color.White;
            decreaseIntensityButton.Location = new Point(615, 20);
            decreaseIntensityButton.Name = "decreaseIntensityButton";
            decreaseIntensityButton.Size = new Size(75, 30);
            decreaseIntensityButton.TabIndex = 1;
            decreaseIntensityButton.Text = "Decrease";
            decreaseIntensityButton.UseVisualStyleBackColor = false;
            // 
            // WaveVisualizationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(800, 600);
            Controls.Add(increaseIntensityButton);
            Controls.Add(decreaseIntensityButton);
            FormBorderStyle = FormBorderStyle.None;
            Name = "WaveVisualizationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Wave Visualization Test - Press 1-5 for intensity levels, Space to toggle";
            ResumeLayout(false);
        }
        #endregion
    }
}