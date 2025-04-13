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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            MainPageExitControlMinimizeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            MainPageExitControlBox = new Guna.UI2.WinForms.Guna2ControlBox();
            SuspendLayout();



            // 
            // CountdownTimerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(815, 450);
            Controls.Add(MainPageExitControlBox);
            Controls.Add(MainPageExitControlMinimizeButton);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CountdownTimerForm";
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ControlBox MainPageExitControlMinimizeButton;
        private Guna.UI2.WinForms.Guna2ControlBox MainPageExitControlBox;
    }
}