namespace CodingTracker.View.PopUpFormService
{
    partial class TimerDisplayForm
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
            // MainPageExitControlMinimizeButton
            // 
            MainPageExitControlMinimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MainPageExitControlMinimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            MainPageExitControlMinimizeButton.Cursor = Cursors.Hand;
            MainPageExitControlMinimizeButton.CustomizableEdges = customizableEdges1;
            MainPageExitControlMinimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            MainPageExitControlMinimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            MainPageExitControlMinimizeButton.HoverState.IconColor = Color.White;
            MainPageExitControlMinimizeButton.IconColor = Color.White;
            MainPageExitControlMinimizeButton.Location = new Point(730, -1);
            MainPageExitControlMinimizeButton.Name = "MainPageExitControlMinimizeButton";
            MainPageExitControlMinimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
            MainPageExitControlMinimizeButton.Size = new Size(45, 29);
            MainPageExitControlMinimizeButton.TabIndex = 27;
            MainPageExitControlMinimizeButton.Click += MainPageExitControlMinimizeButton_Click;
            // 
            // MainPageExitControlBox
            // 
            MainPageExitControlBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MainPageExitControlBox.Cursor = Cursors.Hand;
            MainPageExitControlBox.CustomizableEdges = customizableEdges3;
            MainPageExitControlBox.FillColor = Color.FromArgb(25, 24, 40);
            MainPageExitControlBox.HoverState.IconColor = Color.White;
            MainPageExitControlBox.IconColor = Color.White;
            MainPageExitControlBox.Location = new Point(771, -1);
            MainPageExitControlBox.Name = "MainPageExitControlBox";
            MainPageExitControlBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            MainPageExitControlBox.Size = new Size(45, 29);
            MainPageExitControlBox.TabIndex = 28;
            // 
            // TimerDisplayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(815, 450);
            Controls.Add(MainPageExitControlBox);
            Controls.Add(MainPageExitControlMinimizeButton);
            FormBorderStyle = FormBorderStyle.None;
            Name = "TimerDisplayForm";
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2ControlBox MainPageExitControlMinimizeButton;
        private Guna.UI2.WinForms.Guna2ControlBox MainPageExitControlBox;
    }
}