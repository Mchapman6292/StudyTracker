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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            MainPageExitControlMinimizeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            MainPageExitControlBox = new Guna.UI2.WinForms.Guna2ControlBox();
            DisplayMessageBox = new Guna.UI2.WinForms.Guna2MessageDialog();
            SuspendLayout();
            // 
            // MainPageExitControlMinimizeButton
            // 
            MainPageExitControlMinimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MainPageExitControlMinimizeButton.CustomizableEdges = customizableEdges5;
            MainPageExitControlMinimizeButton.FillColor = Color.FromArgb(139, 152, 166);
            MainPageExitControlMinimizeButton.IconColor = Color.White;
            MainPageExitControlMinimizeButton.Location = new Point(0, 0);
            MainPageExitControlMinimizeButton.Name = "MainPageExitControlMinimizeButton";
            MainPageExitControlMinimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            MainPageExitControlMinimizeButton.Size = new Size(45, 29);
            MainPageExitControlMinimizeButton.TabIndex = 1;
            // 
            // MainPageExitControlBox
            // 
            MainPageExitControlBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MainPageExitControlBox.CustomizableEdges = customizableEdges7;
            MainPageExitControlBox.FillColor = Color.FromArgb(139, 152, 166);
            MainPageExitControlBox.IconColor = Color.White;
            MainPageExitControlBox.Location = new Point(0, 0);
            MainPageExitControlBox.Name = "MainPageExitControlBox";
            MainPageExitControlBox.ShadowDecoration.CustomizableEdges = customizableEdges8;
            MainPageExitControlBox.Size = new Size(45, 29);
            MainPageExitControlBox.TabIndex = 0;
            // 
            // DisplayMessageBox
            // 
            DisplayMessageBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            DisplayMessageBox.Caption = null;
            DisplayMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.None;
            DisplayMessageBox.Parent = null;
            DisplayMessageBox.Style = Guna.UI2.WinForms.MessageDialogStyle.Default;
            DisplayMessageBox.Text = null;
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
        private Guna.UI2.WinForms.Guna2MessageDialog DisplayMessageBox;
    }
}