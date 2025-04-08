namespace CodingTracker.View.TimerDisplayService
{
    partial class CountdownTimerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            TestProgressBar = new Guna.UI2.WinForms.Guna2CircleProgressBar();
            SuspendLayout();
            // 
            // TestProgressBar
            // 
            TestProgressBar.FillColor = Color.FromArgb(200, 213, 218, 223);
            TestProgressBar.Font = new Font("Segoe UI", 12F);
            TestProgressBar.ForeColor = Color.White;
            TestProgressBar.Location = new Point(646, 124);
            TestProgressBar.Minimum = 0;
            TestProgressBar.Name = "TestProgressBar";
            TestProgressBar.ShadowDecoration.CustomizableEdges = customizableEdges1;
            TestProgressBar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            TestProgressBar.Size = new Size(130, 130);
            TestProgressBar.TabIndex = 0;
            TestProgressBar.Text = "guna2CircleProgressBar1";
            // 
            // CountdownTimerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(800, 450);
            Controls.Add(TestProgressBar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CountdownTimerForm";
            Text = "CountdownTimerForm";
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleProgressBar TestProgressBar;
    }
}