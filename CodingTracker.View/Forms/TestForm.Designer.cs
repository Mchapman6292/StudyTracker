using Guna.UI2.WinForms;
using CodingTracker.View.Forms.Services.SharedFormServices.CustomGradientButtons;
namespace CodingTracker.View.Forms
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        // Control declarations
        private Guna.UI2.WinForms.Guna2BorderlessForm borderlessForm;
        private Guna.UI2.WinForms.Guna2Panel mainPanel;
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            borderlessForm = new Guna2BorderlessForm(components);
            mainPanel = new Guna2Panel();
            testAcitivtyControllerPanel = new Guna2GradientPanel();
            testButton = new CustomGradientButton();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // borderlessForm
            // 
            borderlessForm.BorderRadius = 12;
            borderlessForm.ContainerControl = this;
            borderlessForm.DockIndicatorTransparencyValue = 0.6D;
            borderlessForm.TransparentWhileDrag = true;
            // 
            // mainPanel
            // 
            mainPanel.Anchor = AnchorStyles.None;
            mainPanel.BackColor = Color.Transparent;
            mainPanel.BorderColor = Color.FromArgb(225, 225, 225);
            mainPanel.BorderRadius = 12;
            mainPanel.Controls.Add(testButton);
            mainPanel.Controls.Add(testAcitivtyControllerPanel);
            mainPanel.CustomizableEdges = customizableEdges5;
            mainPanel.FillColor = Color.FromArgb(35, 34, 50);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 80);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges6;
            mainPanel.ShadowDecoration.Depth = 20;
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Shadow = new Padding(3, 3, 7, 7);
            mainPanel.Size = new Size(1300, 720);
            mainPanel.TabIndex = 0;
            // 
            // testAcitivtyControllerPanel
            // 
            testAcitivtyControllerPanel.BackColor = Color.Transparent;
            testAcitivtyControllerPanel.BorderColor = Color.FromArgb(70, 71, 117);
            testAcitivtyControllerPanel.BorderRadius = 8;
            testAcitivtyControllerPanel.BorderThickness = 1;
            testAcitivtyControllerPanel.CustomizableEdges = customizableEdges3;
            testAcitivtyControllerPanel.FillColor = Color.FromArgb(45, 46, 60);
            testAcitivtyControllerPanel.Location = new Point(160, 240);
            testAcitivtyControllerPanel.Name = "testAcitivtyControllerPanel";
            testAcitivtyControllerPanel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            testAcitivtyControllerPanel.Size = new Size(843, 390);
            testAcitivtyControllerPanel.TabIndex = 1;
            // 
            // testButton
            // 
            testButton.CustomizableEdges = customizableEdges1;
            testButton.DisabledState.BorderColor = Color.DarkGray;
            testButton.DisabledState.CustomBorderColor = Color.DarkGray;
            testButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            testButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            testButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            testButton.Font = new Font("Segoe UI", 9F);
            testButton.ForeColor = Color.White;
            testButton.Location = new Point(679, 97);
            testButton.Name = "testButton";
            testButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
            testButton.Size = new Size(180, 45);
            testButton.TabIndex = 2;
            testButton.Text = "Test Button";
            testButton.Click += testButton_Click;
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(1300, 720);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "TestForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Modern Timer Test";
            mainPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna2GradientPanel testAcitivtyControllerPanel;
        private CustomGradientButton testButton;
    }
}