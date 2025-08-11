using CodingTracker.View.Forms.Services.SharedFormServices.CustomGradientButtons;
using Guna.UI2.WinForms.Suite;

namespace CodingTracker.View
{
    partial class EditSessionPage
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
            CustomizableEdges customizableEdges9 = new CustomizableEdges();
            CustomizableEdges customizableEdges10 = new CustomizableEdges();
            CustomizableEdges customizableEdges1 = new CustomizableEdges();
            CustomizableEdges customizableEdges2 = new CustomizableEdges();
            CustomizableEdges customizableEdges3 = new CustomizableEdges();
            CustomizableEdges customizableEdges4 = new CustomizableEdges();
            CustomizableEdges customizableEdges5 = new CustomizableEdges();
            CustomizableEdges customizableEdges6 = new CustomizableEdges();
            CustomizableEdges customizableEdges7 = new CustomizableEdges();
            CustomizableEdges customizableEdges8 = new CustomizableEdges();
            CustomizableEdges customizableEdges15 = new CustomizableEdges();
            CustomizableEdges customizableEdges16 = new CustomizableEdges();
            CustomizableEdges customizableEdges11 = new CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditSessionPage));
            CustomizableEdges customizableEdges12 = new CustomizableEdges();
            CustomizableEdges customizableEdges13 = new CustomizableEdges();
            CustomizableEdges customizableEdges14 = new CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            topPanel = new Guna.UI2.WinForms.Guna2Panel();
            newHomeButton = new FontAwesome.Sharp.IconPictureBox();
            editSessionPageTimePicker = new Guna.UI2.WinForms.Guna2DateTimePicker();
            editSessionsPageSessionsLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            EditSessionPageComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            EditSessionPageSessionsLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            EditSessionPageSortByLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            MainPageExitControlMinimizeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            closeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            EditSessionPageMainPanel = new Guna.UI2.WinForms.Guna2Panel();
            deleteSessionButton = new Guna.UI2.WinForms.Guna2GradientButton();
            toggleEditSessionsButton = new Guna.UI2.WinForms.Guna2GradientButton();
            editSessionPageDataGridView = new Guna.UI2.WinForms.Guna2DataGridView();
            StudyProject = new DataGridViewTextBoxColumn();
            Duration = new DataGridViewTextBoxColumn();
            StartDate = new DataGridViewTextBoxColumn();
            StartTime = new DataGridViewTextBoxColumn();
            EndDate = new DataGridViewTextBoxColumn();
            EndTime = new DataGridViewTextBoxColumn();
            DisplayMessageBox = new Guna.UI2.WinForms.Guna2MessageDialog();
            EditSessionPageNotificationPaint = new Guna.UI2.WinForms.Guna2NotificationPaint(components);
            githubPictureBox = new FontAwesome.Sharp.IconPictureBox();
            guna2HtmlLabel6 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            editSessionAnimationWindow = new Guna.UI2.WinForms.Guna2AnimateWindow(components);
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)newHomeButton).BeginInit();
            EditSessionPageMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)editSessionPageDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)githubPictureBox).BeginInit();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.FromArgb(25, 24, 40);
            topPanel.Controls.Add(newHomeButton);
            topPanel.Controls.Add(editSessionPageTimePicker);
            topPanel.Controls.Add(editSessionsPageSessionsLabel);
            topPanel.Controls.Add(EditSessionPageComboBox);
            topPanel.Controls.Add(EditSessionPageSessionsLabel);
            topPanel.Controls.Add(EditSessionPageSortByLabel);
            topPanel.Controls.Add(MainPageExitControlMinimizeButton);
            topPanel.Controls.Add(closeButton);
            topPanel.CustomizableEdges = customizableEdges9;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.ShadowDecoration.CustomizableEdges = customizableEdges10;
            topPanel.Size = new Size(1188, 75);
            topPanel.TabIndex = 0;
            // 
            // newHomeButton
            // 
            newHomeButton.BackColor = Color.FromArgb(25, 24, 40);
            newHomeButton.ForeColor = Color.FromArgb(255, 160, 210);
            newHomeButton.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            newHomeButton.IconColor = Color.FromArgb(255, 160, 210);
            newHomeButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            newHomeButton.IconSize = 29;
            newHomeButton.Location = new Point(1010, 2);
            newHomeButton.Margin = new Padding(3, 2, 3, 2);
            newHomeButton.Name = "newHomeButton";
            newHomeButton.Size = new Size(45, 29);
            newHomeButton.SizeMode = PictureBoxSizeMode.CenterImage;
            newHomeButton.TabIndex = 47;
            newHomeButton.TabStop = false;
            newHomeButton.Click += newHomeButton_Click;
            // 
            // editSessionPageTimePicker
            // 
            editSessionPageTimePicker.BackColor = Color.FromArgb(26, 26, 46);
            editSessionPageTimePicker.BorderRadius = 10;
            editSessionPageTimePicker.Checked = true;
            editSessionPageTimePicker.CustomizableEdges = customizableEdges1;
            editSessionPageTimePicker.FillColor = Color.FromArgb(26, 26, 46);
            editSessionPageTimePicker.Font = new Font("Segoe UI", 9F);
            editSessionPageTimePicker.ForeColor = Color.FromArgb(255, 200, 230);
            editSessionPageTimePicker.Format = DateTimePickerFormat.Long;
            editSessionPageTimePicker.Location = new Point(666, -3);
            editSessionPageTimePicker.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            editSessionPageTimePicker.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            editSessionPageTimePicker.Name = "editSessionPageTimePicker";
            editSessionPageTimePicker.ShadowDecoration.CustomizableEdges = customizableEdges2;
            editSessionPageTimePicker.Size = new Size(200, 36);
            editSessionPageTimePicker.TabIndex = 24;
            editSessionPageTimePicker.Value = new DateTime(2025, 2, 23, 10, 14, 42, 360);
            editSessionPageTimePicker.ValueChanged += EditSessionPageTimePicker_ValueChanged;
            // 
            // editSessionsPageSessionsLabel
            // 
            editSessionsPageSessionsLabel.AutoSize = false;
            editSessionsPageSessionsLabel.BackColor = Color.Transparent;
            editSessionsPageSessionsLabel.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            editSessionsPageSessionsLabel.ForeColor = Color.FromArgb(255, 200, 230);
            editSessionsPageSessionsLabel.Location = new Point(4, 4);
            editSessionsPageSessionsLabel.Name = "editSessionsPageSessionsLabel";
            editSessionsPageSessionsLabel.Size = new Size(28, 32);
            editSessionsPageSessionsLabel.TabIndex = 28;
            editSessionsPageSessionsLabel.Text = null;
            // 
            // EditSessionPageComboBox
            // 
            EditSessionPageComboBox.BackColor = Color.FromArgb(35, 34, 50);
            EditSessionPageComboBox.BorderColor = Color.FromArgb(55, 54, 70);
            EditSessionPageComboBox.CustomizableEdges = customizableEdges3;
            EditSessionPageComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            EditSessionPageComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            EditSessionPageComboBox.FillColor = Color.FromArgb(35, 34, 50);
            EditSessionPageComboBox.FocusedColor = Color.FromArgb(94, 148, 255);
            EditSessionPageComboBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            EditSessionPageComboBox.Font = new Font("Segoe UI", 10F);
            EditSessionPageComboBox.ForeColor = Color.Silver;
            EditSessionPageComboBox.ItemHeight = 30;
            EditSessionPageComboBox.Location = new Point(360, 0);
            EditSessionPageComboBox.Name = "EditSessionPageComboBox";
            EditSessionPageComboBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            EditSessionPageComboBox.Size = new Size(224, 36);
            EditSessionPageComboBox.TabIndex = 31;
            // 
            // EditSessionPageSessionsLabel
            // 
            EditSessionPageSessionsLabel.AutoSize = false;
            EditSessionPageSessionsLabel.BackColor = Color.Transparent;
            EditSessionPageSessionsLabel.Font = new Font("Century Gothic", 12F);
            EditSessionPageSessionsLabel.ForeColor = Color.FromArgb(255, 200, 230);
            EditSessionPageSessionsLabel.Location = new Point(38, 4);
            EditSessionPageSessionsLabel.Name = "EditSessionPageSessionsLabel";
            EditSessionPageSessionsLabel.Size = new Size(90, 32);
            EditSessionPageSessionsLabel.TabIndex = 29;
            EditSessionPageSessionsLabel.Text = "Sessions";
            // 
            // EditSessionPageSortByLabel
            // 
            EditSessionPageSortByLabel.AutoSize = false;
            EditSessionPageSortByLabel.BackColor = Color.Transparent;
            EditSessionPageSortByLabel.Font = new Font("Century Gothic", 9F);
            EditSessionPageSortByLabel.ForeColor = Color.FromArgb(255, 200, 230);
            EditSessionPageSortByLabel.Location = new Point(292, 3);
            EditSessionPageSortByLabel.Name = "EditSessionPageSortByLabel";
            EditSessionPageSortByLabel.Size = new Size(71, 30);
            EditSessionPageSortByLabel.TabIndex = 30;
            EditSessionPageSortByLabel.Text = "Sort By";
            EditSessionPageSortByLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // MainPageExitControlMinimizeButton
            // 
            MainPageExitControlMinimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            MainPageExitControlMinimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            MainPageExitControlMinimizeButton.CustomizableEdges = customizableEdges5;
            MainPageExitControlMinimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            MainPageExitControlMinimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            MainPageExitControlMinimizeButton.HoverState.IconColor = Color.White;
            MainPageExitControlMinimizeButton.IconColor = Color.FromArgb(255, 160, 210);
            MainPageExitControlMinimizeButton.Location = new Point(1052, 2);
            MainPageExitControlMinimizeButton.Name = "MainPageExitControlMinimizeButton";
            MainPageExitControlMinimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            MainPageExitControlMinimizeButton.Size = new Size(45, 29);
            MainPageExitControlMinimizeButton.TabIndex = 27;
            MainPageExitControlMinimizeButton.Click += MainPageExitControlMinimizeButton_Click;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.CustomClick = true;
            closeButton.CustomizableEdges = customizableEdges7;
            closeButton.FillColor = Color.FromArgb(25, 24, 40);
            closeButton.HoverState.IconColor = Color.White;
            closeButton.IconColor = Color.FromArgb(255, 160, 210);
            closeButton.Location = new Point(1096, 2);
            closeButton.Name = "closeButton";
            closeButton.ShadowDecoration.CustomizableEdges = customizableEdges8;
            closeButton.Size = new Size(45, 29);
            closeButton.TabIndex = 26;
            // 
            // EditSessionPageMainPanel
            // 
            EditSessionPageMainPanel.BackColor = Color.FromArgb(26, 26, 46);
            EditSessionPageMainPanel.Controls.Add(deleteSessionButton);
            EditSessionPageMainPanel.Controls.Add(toggleEditSessionsButton);
            EditSessionPageMainPanel.Controls.Add(editSessionPageDataGridView);
            EditSessionPageMainPanel.CustomizableEdges = customizableEdges15;
            EditSessionPageMainPanel.Location = new Point(0, 73);
            EditSessionPageMainPanel.Name = "EditSessionPageMainPanel";
            EditSessionPageMainPanel.ShadowDecoration.CustomizableEdges = customizableEdges16;
            EditSessionPageMainPanel.Size = new Size(1188, 707);
            EditSessionPageMainPanel.TabIndex = 1;
            // 
            // deleteSessionButton
            // 
            deleteSessionButton.Animated = true;
            deleteSessionButton.AutoRoundedCorners = true;
            deleteSessionButton.BackColor = Color.FromArgb(35, 34, 50);
            deleteSessionButton.BorderColor = Color.FromArgb(128, 127, 145);
            deleteSessionButton.BorderRadius = 15;
            deleteSessionButton.BorderThickness = 2;
            deleteSessionButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.ToogleButton;
            deleteSessionButton.CustomizableEdges = customizableEdges11;
            deleteSessionButton.DisabledState.BorderColor = Color.DarkGray;
            deleteSessionButton.DisabledState.CustomBorderColor = Color.DarkGray;
            deleteSessionButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            deleteSessionButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            deleteSessionButton.FillColor = Color.FromArgb(35, 34, 50);
            deleteSessionButton.FillColor2 = Color.FromArgb(35, 34, 50);
            deleteSessionButton.Font = new Font("Segoe UI", 9F);
            deleteSessionButton.ForeColor = Color.White;
            deleteSessionButton.Image = (Image)resources.GetObject("deleteSessionButton.Image");
            deleteSessionButton.ImageSize = new Size(40, 30);
            deleteSessionButton.Location = new Point(992, 625);
            deleteSessionButton.Name = "deleteSessionButton";
            deleteSessionButton.ShadowDecoration.CustomizableEdges = customizableEdges12;
            deleteSessionButton.Size = new Size(105, 33);
            deleteSessionButton.TabIndex = 25;
            deleteSessionButton.Click += EditSessionPageDeleteButton_Click;
            // 
            // toggleEditSessionsButton
            // 
            toggleEditSessionsButton.Animated = true;
            toggleEditSessionsButton.AutoRoundedCorners = true;
            toggleEditSessionsButton.BackColor = Color.FromArgb(35, 34, 50);
            toggleEditSessionsButton.BorderColor = Color.FromArgb(128, 127, 145);
            toggleEditSessionsButton.BorderRadius = 15;
            toggleEditSessionsButton.BorderThickness = 2;
            toggleEditSessionsButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.ToogleButton;
            toggleEditSessionsButton.CustomizableEdges = customizableEdges13;
            toggleEditSessionsButton.DisabledState.BorderColor = Color.DarkGray;
            toggleEditSessionsButton.DisabledState.CustomBorderColor = Color.DarkGray;
            toggleEditSessionsButton.DisabledState.FillColor = Color.FromArgb(255, 81, 195);
            toggleEditSessionsButton.DisabledState.ForeColor = Color.FromArgb(168, 228, 255);
            toggleEditSessionsButton.FillColor = Color.FromArgb(35, 34, 50);
            toggleEditSessionsButton.FillColor2 = Color.FromArgb(35, 34, 50);
            toggleEditSessionsButton.Font = new Font("Segoe UI", 9F);
            toggleEditSessionsButton.ForeColor = Color.White;
            toggleEditSessionsButton.Image = (Image)resources.GetObject("toggleEditSessionsButton.Image");
            toggleEditSessionsButton.ImageSize = new Size(40, 40);
            toggleEditSessionsButton.Location = new Point(835, 625);
            toggleEditSessionsButton.Name = "toggleEditSessionsButton";
            toggleEditSessionsButton.ShadowDecoration.CustomizableEdges = customizableEdges14;
            toggleEditSessionsButton.Size = new Size(105, 33);
            toggleEditSessionsButton.TabIndex = 23;
            toggleEditSessionsButton.CheckedChanged += TestEditSessionButton2_CheckedChanged;
            // 
            // editSessionPageDataGridView
            // 
            editSessionPageDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(44, 45, 65);
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlLight;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(255, 81, 195);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            editSessionPageDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            editSessionPageDataGridView.BackgroundColor = Color.FromArgb(44, 45, 65);
            editSessionPageDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(44, 45, 65);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.HotPink;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(44, 45, 65);
            dataGridViewCellStyle2.SelectionForeColor = Color.HotPink;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            editSessionPageDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            editSessionPageDataGridView.ColumnHeadersHeight = 45;
            editSessionPageDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            editSessionPageDataGridView.Columns.AddRange(new DataGridViewColumn[] { StudyProject, Duration, StartDate, StartTime, EndDate, EndTime });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(44, 45, 65);
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlLight;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(255, 81, 195);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            editSessionPageDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            editSessionPageDataGridView.GridColor = Color.FromArgb(26, 26, 46);
            editSessionPageDataGridView.Location = new Point(24, 8);
            editSessionPageDataGridView.MultiSelect = false;
            editSessionPageDataGridView.Name = "editSessionPageDataGridView";
            editSessionPageDataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(35, 34, 50);
            dataGridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlLight;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(168, 228, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(35, 34, 50);
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            editSessionPageDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            editSessionPageDataGridView.RowHeadersVisible = false;
            editSessionPageDataGridView.RowHeadersWidth = 51;
            editSessionPageDataGridView.RowTemplate.DividerHeight = 1;
            editSessionPageDataGridView.RowTemplate.Height = 45;
            editSessionPageDataGridView.ShowCellErrors = false;
            editSessionPageDataGridView.ShowCellToolTips = false;
            editSessionPageDataGridView.ShowEditingIcon = false;
            editSessionPageDataGridView.ShowRowErrors = false;
            editSessionPageDataGridView.Size = new Size(1129, 604);
            editSessionPageDataGridView.TabIndex = 22;
            editSessionPageDataGridView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Dark;
            editSessionPageDataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(44, 45, 65);
            editSessionPageDataGridView.ThemeStyle.AlternatingRowsStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editSessionPageDataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = SystemColors.ControlLight;
            editSessionPageDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.FromArgb(255, 81, 195);
            editSessionPageDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.White;
            editSessionPageDataGridView.ThemeStyle.BackColor = Color.FromArgb(44, 45, 65);
            editSessionPageDataGridView.ThemeStyle.GridColor = Color.FromArgb(26, 26, 46);
            editSessionPageDataGridView.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(44, 45, 65);
            editSessionPageDataGridView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            editSessionPageDataGridView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            editSessionPageDataGridView.ThemeStyle.HeaderStyle.ForeColor = Color.HotPink;
            editSessionPageDataGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            editSessionPageDataGridView.ThemeStyle.HeaderStyle.Height = 45;
            editSessionPageDataGridView.ThemeStyle.ReadOnly = false;
            editSessionPageDataGridView.ThemeStyle.RowsStyle.BackColor = Color.FromArgb(44, 45, 65);
            editSessionPageDataGridView.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.None;
            editSessionPageDataGridView.ThemeStyle.RowsStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editSessionPageDataGridView.ThemeStyle.RowsStyle.ForeColor = SystemColors.ControlLight;
            editSessionPageDataGridView.ThemeStyle.RowsStyle.Height = 45;
            editSessionPageDataGridView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(255, 81, 195);
            editSessionPageDataGridView.ThemeStyle.RowsStyle.SelectionForeColor = Color.White;
            editSessionPageDataGridView.CellClick += EditModeDataGridView_CellClick;
            // 
            // StudyProject
            // 
            StudyProject.FillWeight = 101.522842F;
            StudyProject.HeaderText = "Study Project";
            StudyProject.MinimumWidth = 6;
            StudyProject.Name = "StudyProject";
            // 
            // Duration
            // 
            Duration.HeaderText = "Duration";
            Duration.MinimumWidth = 6;
            Duration.Name = "Duration";
            // 
            // StartDate
            // 
            StartDate.HeaderText = "Start Date";
            StartDate.MinimumWidth = 6;
            StartDate.Name = "StartDate";
            // 
            // StartTime
            // 
            StartTime.FillWeight = 99.4923859F;
            StartTime.HeaderText = "Start Time";
            StartTime.MinimumWidth = 6;
            StartTime.Name = "StartTime";
            // 
            // EndDate
            // 
            EndDate.FillWeight = 99.4923859F;
            EndDate.HeaderText = "End Date";
            EndDate.MinimumWidth = 6;
            EndDate.Name = "EndDate";
            // 
            // EndTime
            // 
            EndTime.HeaderText = "End Time";
            EndTime.MinimumWidth = 6;
            EndTime.Name = "EndTime";
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
            // EditSessionPageNotificationPaint
            // 
            EditSessionPageNotificationPaint.Alignment = Guna.UI2.WinForms.Enums.CustomContentAlignment.MiddleCenter;
            EditSessionPageNotificationPaint.FillColor = Color.FromArgb(64, 63, 79);
            EditSessionPageNotificationPaint.Location = new Point(67, 7);
            EditSessionPageNotificationPaint.Offset = new Point(30, 0);
            EditSessionPageNotificationPaint.Size = new Size(30, 18);
            EditSessionPageNotificationPaint.TargetControl = toggleEditSessionsButton;
            EditSessionPageNotificationPaint.Text = "Off";
            // 
            // githubPictureBox
            // 
            githubPictureBox.BackColor = Color.FromArgb(35, 34, 50);
            githubPictureBox.ForeColor = Color.FromArgb(204, 84, 144);
            githubPictureBox.IconChar = FontAwesome.Sharp.IconChar.CodePullRequest;
            githubPictureBox.IconColor = Color.FromArgb(204, 84, 144);
            githubPictureBox.IconFont = FontAwesome.Sharp.IconFont.Auto;
            githubPictureBox.IconSize = 34;
            githubPictureBox.Location = new Point(68, 39);
            githubPictureBox.Margin = new Padding(3, 2, 3, 2);
            githubPictureBox.Name = "githubPictureBox";
            githubPictureBox.Size = new Size(35, 34);
            githubPictureBox.TabIndex = 41;
            githubPictureBox.TabStop = false;
            // 
            // guna2HtmlLabel6
            // 
            guna2HtmlLabel6.Anchor = AnchorStyles.None;
            guna2HtmlLabel6.BackColor = Color.Transparent;
            guna2HtmlLabel6.Font = new Font("Century Gothic", 18F, FontStyle.Bold);
            guna2HtmlLabel6.ForeColor = Color.FromArgb(204, 84, 144);
            guna2HtmlLabel6.Location = new Point(3, 3);
            guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            guna2HtmlLabel6.Size = new Size(175, 30);
            guna2HtmlLabel6.TabIndex = 33;
            guna2HtmlLabel6.Text = "CodingTracker";
            guna2HtmlLabel6.TextAlignment = ContentAlignment.TopCenter;
            // 
            // editSessionAnimationWindow
            // 
            editSessionAnimationWindow.TargetForm = this;
            // 
            // EditSessionPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 26, 46);
            ClientSize = new Size(1188, 780);
            Controls.Add(EditSessionPageMainPanel);
            Controls.Add(topPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EditSessionPage";
            Text = "EditSessionForm";
            Load += EditSessionPage_Load;
            topPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)newHomeButton).EndInit();
            EditSessionPageMainPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)editSessionPageDataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)githubPictureBox).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel topPanel;
        private Guna.UI2.WinForms.Guna2ControlBox closeButton;
        private Guna.UI2.WinForms.Guna2ControlBox MainPageExitControlMinimizeButton;
        private Guna.UI2.WinForms.Guna2Panel EditSessionPageMainPanel;
        private Guna.UI2.WinForms.Guna2DataGridView editSessionPageDataGridView;
        private Guna.UI2.WinForms.Guna2HtmlLabel editSessionsPageSessionsLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel EditSessionPageSortByLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel EditSessionPageSessionsLabel;
        private Guna.UI2.WinForms.Guna2ComboBox EditSessionPageComboBox;
        private Guna.UI2.WinForms.Guna2MessageDialog DisplayMessageBox;
        private Guna.UI2.WinForms.Guna2GradientButton toggleEditSessionsButton;
        private Guna.UI2.WinForms.Guna2NotificationPaint EditSessionPageNotificationPaint;
        private Guna.UI2.WinForms.Guna2DateTimePicker editSessionPageTimePicker;
        private Guna.UI2.WinForms.Guna2GradientButton deleteSessionButton;
        private CustomGradientButton dashboardButton;
        private CustomGradientButton sessionsButton;
        private CustomGradientButton logoutButton;
        private CustomGradientButton newSessionButton;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel6;
        private FontAwesome.Sharp.IconPictureBox githubPictureBox;
        private Guna.UI2.WinForms.Guna2AnimateWindow editSessionAnimationWindow;
        private DataGridViewTextBoxColumn StudyProject;
        private DataGridViewTextBoxColumn Duration;
        private DataGridViewTextBoxColumn StartDate;
        private DataGridViewTextBoxColumn StartTime;
        private DataGridViewTextBoxColumn EndDate;
        private DataGridViewTextBoxColumn EndTime;
        private FontAwesome.Sharp.IconPictureBox newHomeButton;
    }
}