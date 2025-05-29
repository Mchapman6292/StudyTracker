namespace CodingTracker.View
{
    partial class WaveVisualizationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Timer decayTimer = new System.Windows.Forms.Timer();

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
            SuspendLayout();
            // 
            // WaveVisualizationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(800, 600);
            FormBorderStyle = FormBorderStyle.None;
            Name = "WaveVisualizationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Wave Visualization Test - Press 1-5 for intensity levels, Space to toggle";
            ResumeLayout(false);
        }
        #endregion
    }
}