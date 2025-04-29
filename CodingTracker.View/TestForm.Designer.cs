namespace CodingTracker.View
{
    partial class TestForm
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            EditSessionExitControlBox = new Guna.UI2.WinForms.Guna2ControlBox();
            TestBorderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            MainPageExitControlMinimizeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            CodingSessionPageHomeButton = new Guna.UI2.WinForms.Guna2GradientButton();
            SuspendLayout();
            // 
            // EditSessionExitControlBox
            // 
            EditSessionExitControlBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            EditSessionExitControlBox.CustomizableEdges = customizableEdges1;
            EditSessionExitControlBox.FillColor = Color.FromArgb(25, 24, 40);
            EditSessionExitControlBox.HoverState.IconColor = Color.White;
            EditSessionExitControlBox.IconColor = Color.White;
            EditSessionExitControlBox.Location = new Point(341, 0);
            EditSessionExitControlBox.Name = "EditSessionExitControlBox";
            EditSessionExitControlBox.ShadowDecoration.CustomizableEdges = customizableEdges2;
            EditSessionExitControlBox.Size = new Size(45, 29);
            EditSessionExitControlBox.TabIndex = 27;
            // 
            // TestBorderlessForm
            // 
            TestBorderlessForm.ContainerControl = this;
            TestBorderlessForm.DockIndicatorTransparencyValue = 0.6D;
            TestBorderlessForm.TransparentWhileDrag = true;
            // 
            // MainPageExitControlMinimizeButton
            // 
            MainPageExitControlMinimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MainPageExitControlMinimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            MainPageExitControlMinimizeButton.CustomizableEdges = customizableEdges5;
            MainPageExitControlMinimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            MainPageExitControlMinimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            MainPageExitControlMinimizeButton.HoverState.IconColor = Color.White;
            MainPageExitControlMinimizeButton.IconColor = Color.White;
            MainPageExitControlMinimizeButton.Location = new Point(301, 0);
            MainPageExitControlMinimizeButton.Name = "MainPageExitControlMinimizeButton";
            MainPageExitControlMinimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            MainPageExitControlMinimizeButton.Size = new Size(45, 29);
            MainPageExitControlMinimizeButton.TabIndex = 28;
            // 
            // CodingSessionPageHomeButton
            // 
            CodingSessionPageHomeButton.CustomizableEdges = customizableEdges3;
            CodingSessionPageHomeButton.DisabledState.BorderColor = Color.DarkGray;
            CodingSessionPageHomeButton.DisabledState.CustomBorderColor = Color.DarkGray;
            CodingSessionPageHomeButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            CodingSessionPageHomeButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            CodingSessionPageHomeButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            CodingSessionPageHomeButton.FillColor = Color.FromArgb(25, 24, 40);
            CodingSessionPageHomeButton.FillColor2 = Color.FromArgb(25, 24, 40);
            CodingSessionPageHomeButton.Font = new Font("Segoe UI", 9F);
            CodingSessionPageHomeButton.ForeColor = Color.White;
            CodingSessionPageHomeButton.Image = (Image)resources.GetObject("CodingSessionPageHomeButton.Image");
            CodingSessionPageHomeButton.Location = new Point(267, 0);
            CodingSessionPageHomeButton.Name = "CodingSessionPageHomeButton";
            CodingSessionPageHomeButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
            CodingSessionPageHomeButton.Size = new Size(45, 29);
            CodingSessionPageHomeButton.TabIndex = 33;
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 211);
            Controls.Add(CodingSessionPageHomeButton);
            Controls.Add(MainPageExitControlMinimizeButton);
            Controls.Add(EditSessionExitControlBox);
            FormBorderStyle = FormBorderStyle.None;
            Name = "TestForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TestForm";
            Load += TestForm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ControlBox EditSessionExitControlBox;
        private Guna.UI2.WinForms.Guna2BorderlessForm TestBorderlessForm;
        private Guna.UI2.WinForms.Guna2ControlBox MainPageExitControlMinimizeButton;
        private Guna.UI2.WinForms.Guna2GradientButton CodingSessionPageHomeButton;
    }
}