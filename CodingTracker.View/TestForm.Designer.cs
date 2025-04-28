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
            EditSessionExitControlBox = new Guna.UI2.WinForms.Guna2ControlBox();
            TestBorderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            SuspendLayout();
            // 
            // EditSessionExitControlBox
            // 
            EditSessionExitControlBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            EditSessionExitControlBox.CustomizableEdges = customizableEdges1;
            EditSessionExitControlBox.FillColor = Color.FromArgb(25, 24, 40);
            EditSessionExitControlBox.HoverState.IconColor = Color.White;
            EditSessionExitControlBox.IconColor = Color.White;
            EditSessionExitControlBox.Location = new Point(342, -1);
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
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 211);
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
    }
}