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
            CustomizableEdges customizableEdges37 = new CustomizableEdges();
            CustomizableEdges customizableEdges38 = new CustomizableEdges();
            CustomizableEdges customizableEdges27 = new CustomizableEdges();
            CustomizableEdges customizableEdges28 = new CustomizableEdges();
            CustomizableEdges customizableEdges29 = new CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditSessionPage));
            CustomizableEdges customizableEdges30 = new CustomizableEdges();
            CustomizableEdges customizableEdges31 = new CustomizableEdges();
            CustomizableEdges customizableEdges32 = new CustomizableEdges();
            CustomizableEdges customizableEdges33 = new CustomizableEdges();
            CustomizableEdges customizableEdges34 = new CustomizableEdges();
            CustomizableEdges customizableEdges35 = new CustomizableEdges();
            CustomizableEdges customizableEdges36 = new CustomizableEdges();
            CustomizableEdges customizableEdges43 = new CustomizableEdges();
            CustomizableEdges customizableEdges44 = new CustomizableEdges();
            CustomizableEdges customizableEdges39 = new CustomizableEdges();
            CustomizableEdges customizableEdges40 = new CustomizableEdges();
            CustomizableEdges customizableEdges41 = new CustomizableEdges();
            CustomizableEdges customizableEdges42 = new CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            CustomizableEdges customizableEdges45 = new CustomizableEdges();
            CustomizableEdges customizableEdges46 = new CustomizableEdges();
            CustomizableEdges customizableEdges47 = new CustomizableEdges();
            CustomizableEdges customizableEdges48 = new CustomizableEdges();
            CustomizableEdges customizableEdges49 = new CustomizableEdges();
            CustomizableEdges customizableEdges50 = new CustomizableEdges();
            CustomizableEdges customizableEdges51 = new CustomizableEdges();
            CustomizableEdges customizableEdges52 = new CustomizableEdges();
            topPanel = new Guna.UI2.WinForms.Guna2Panel();
            editSessionPageTimePicker = new Guna.UI2.WinForms.Guna2DateTimePicker();
            CodingSessionPageHomeButton = new Guna.UI2.WinForms.Guna2GradientButton();
            EditSessionPageComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            EditSessionPageSortByLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            MainPageExitControlMinimizeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            closeButton = new Guna.UI2.WinForms.Guna2ControlBox();
            EditSessionPageSessionsLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            EditSessionsPageSessionsLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
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
            newSessionButton = new CustomGradientButton();
            dashboardButton = new CustomGradientButton();
            sessionsButton = new CustomGradientButton();
            logoutButton = new CustomGradientButton();
            githubPictureBox = new FontAwesome.Sharp.IconPictureBox();
            guna2HtmlLabel6 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            editSessionAnimationWindow = new Guna.UI2.WinForms.Guna2AnimateWindow(components);
            topPanel.SuspendLayout();
            EditSessionPageMainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)editSessionPageDataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)githubPictureBox).BeginInit();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.Controls.Add(editSessionPageTimePicker);
            topPanel.Controls.Add(CodingSessionPageHomeButton);
            topPanel.Controls.Add(EditSessionPageComboBox);
            topPanel.Controls.Add(EditSessionPageSortByLabel);
            topPanel.Controls.Add(MainPageExitControlMinimizeButton);
            topPanel.Controls.Add(closeButton);
            topPanel.CustomizableEdges = customizableEdges37;
            topPanel.Location = new Point(12, 0);
            topPanel.Name = "topPanel";
            topPanel.ShadowDecoration.CustomizableEdges = customizableEdges38;
            topPanel.Size = new Size(1186, 75);
            topPanel.TabIndex = 0;
            // 
            // editSessionPageTimePicker
            // 
            editSessionPageTimePicker.BackColor = Color.FromArgb(44, 45, 65);
            editSessionPageTimePicker.BorderRadius = 10;
            editSessionPageTimePicker.Checked = true;
            editSessionPageTimePicker.CustomizableEdges = customizableEdges27;
            editSessionPageTimePicker.FillColor = Color.FromArgb(35, 34, 50);
            editSessionPageTimePicker.Font = new Font("Segoe UI", 9F);
            editSessionPageTimePicker.ForeColor = Color.FromArgb(255, 200, 230);
            editSessionPageTimePicker.Format = DateTimePickerFormat.Long;
            editSessionPageTimePicker.Location = new Point(751, -1);
            editSessionPageTimePicker.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            editSessionPageTimePicker.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            editSessionPageTimePicker.Name = "editSessionPageTimePicker";
            editSessionPageTimePicker.ShadowDecoration.CustomizableEdges = customizableEdges28;
            editSessionPageTimePicker.Size = new Size(200, 36);
            editSessionPageTimePicker.TabIndex = 24;
            editSessionPageTimePicker.Value = new DateTime(2025, 2, 23, 10, 14, 42, 360);
            editSessionPageTimePicker.ValueChanged += EditSessionPageTimePicker_ValueChanged;
            // 
            // CodingSessionPageHomeButton
            // 
            CodingSessionPageHomeButton.CustomizableEdges = customizableEdges29;
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
            CodingSessionPageHomeButton.Location = new Point(1175, 0);
            CodingSessionPageHomeButton.Name = "CodingSessionPageHomeButton";
            CodingSessionPageHomeButton.ShadowDecoration.CustomizableEdges = customizableEdges30;
            CodingSessionPageHomeButton.Size = new Size(45, 29);
            CodingSessionPageHomeButton.TabIndex = 32;
            CodingSessionPageHomeButton.Click += CodingSessionPageHomeButton_Click;
            // 
            // EditSessionPageComboBox
            // 
            EditSessionPageComboBox.BackColor = Color.FromArgb(35, 34, 50);
            EditSessionPageComboBox.BorderColor = Color.FromArgb(55, 54, 70);
            EditSessionPageComboBox.CustomizableEdges = customizableEdges31;
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
            EditSessionPageComboBox.ShadowDecoration.CustomizableEdges = customizableEdges32;
            EditSessionPageComboBox.Size = new Size(224, 36);
            EditSessionPageComboBox.TabIndex = 31;
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
            MainPageExitControlMinimizeButton.CustomizableEdges = customizableEdges33;
            MainPageExitControlMinimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            MainPageExitControlMinimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            MainPageExitControlMinimizeButton.HoverState.IconColor = Color.White;
            MainPageExitControlMinimizeButton.IconColor = Color.White;
            MainPageExitControlMinimizeButton.Location = new Point(1099, 0);
            MainPageExitControlMinimizeButton.Name = "MainPageExitControlMinimizeButton";
            MainPageExitControlMinimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges34;
            MainPageExitControlMinimizeButton.Size = new Size(45, 29);
            MainPageExitControlMinimizeButton.TabIndex = 27;
            MainPageExitControlMinimizeButton.Click += MainPageExitControlMinimizeButton_Click;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.CustomClick = true;
            closeButton.CustomizableEdges = customizableEdges35;
            closeButton.FillColor = Color.FromArgb(25, 24, 40);
            closeButton.HoverState.IconColor = Color.White;
            closeButton.IconColor = Color.White;
            closeButton.Location = new Point(1141, 0);
            closeButton.Name = "closeButton";
            closeButton.ShadowDecoration.CustomizableEdges = customizableEdges36;
            closeButton.Size = new Size(45, 29);
            closeButton.TabIndex = 26;
            // 
            // EditSessionPageSessionsLabel
            // 
            EditSessionPageSessionsLabel.AutoSize = false;
            EditSessionPageSessionsLabel.BackColor = Color.Transparent;
            EditSessionPageSessionsLabel.Font = new Font("Century Gothic", 12F);
            EditSessionPageSessionsLabel.ForeColor = Color.FromArgb(255, 200, 230);
            EditSessionPageSessionsLabel.Location = new Point(37, 634);
            EditSessionPageSessionsLabel.Name = "EditSessionPageSessionsLabel";
            EditSessionPageSessionsLabel.Size = new Size(90, 32);
            EditSessionPageSessionsLabel.TabIndex = 29;
            EditSessionPageSessionsLabel.Text = "Sessions";
            // 
            // EditSessionsPageSessionsLabel
            // 
            EditSessionsPageSessionsLabel.AutoSize = false;
            EditSessionsPageSessionsLabel.BackColor = Color.Transparent;
            EditSessionsPageSessionsLabel.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold);
            EditSessionsPageSessionsLabel.ForeColor = Color.FromArgb(255, 200, 230);
            EditSessionsPageSessionsLabel.Location = new Point(3, 634);
            EditSessionsPageSessionsLabel.Name = "EditSessionsPageSessionsLabel";
            EditSessionsPageSessionsLabel.Size = new Size(28, 32);
            EditSessionsPageSessionsLabel.TabIndex = 28;
            EditSessionsPageSessionsLabel.Text = "28";
            // 
            // EditSessionPageMainPanel
            // 
            EditSessionPageMainPanel.Controls.Add(deleteSessionButton);
            EditSessionPageMainPanel.Controls.Add(toggleEditSessionsButton);
            EditSessionPageMainPanel.Controls.Add(editSessionPageDataGridView);
            EditSessionPageMainPanel.Controls.Add(EditSessionsPageSessionsLabel);
            EditSessionPageMainPanel.Controls.Add(EditSessionPageSessionsLabel);
            EditSessionPageMainPanel.CustomizableEdges = customizableEdges43;
            EditSessionPageMainPanel.Location = new Point(12, 81);
            EditSessionPageMainPanel.Name = "EditSessionPageMainPanel";
            EditSessionPageMainPanel.ShadowDecoration.CustomizableEdges = customizableEdges44;
            EditSessionPageMainPanel.Size = new Size(1189, 702);
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
            deleteSessionButton.CustomizableEdges = customizableEdges39;
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
            deleteSessionButton.Location = new Point(950, 621);
            deleteSessionButton.Name = "deleteSessionButton";
            deleteSessionButton.ShadowDecoration.CustomizableEdges = customizableEdges40;
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
            toggleEditSessionsButton.CustomizableEdges = customizableEdges41;
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
            toggleEditSessionsButton.Location = new Point(811, 621);
            toggleEditSessionsButton.Name = "toggleEditSessionsButton";
            toggleEditSessionsButton.ShadowDecoration.CustomizableEdges = customizableEdges42;
            toggleEditSessionsButton.Size = new Size(105, 33);
            toggleEditSessionsButton.TabIndex = 23;
            toggleEditSessionsButton.CheckedChanged += TestEditSessionButton2_CheckedChanged;
            // 
            // editSessionPageDataGridView
            // 
            editSessionPageDataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(44, 45, 65);
            dataGridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlLight;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(255, 81, 195);
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            editSessionPageDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            editSessionPageDataGridView.BackgroundColor = Color.FromArgb(35, 34, 50);
            editSessionPageDataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(44, 45, 65);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = Color.HotPink;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(44, 45, 65);
            dataGridViewCellStyle6.SelectionForeColor = Color.HotPink;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            editSessionPageDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            editSessionPageDataGridView.ColumnHeadersHeight = 45;
            editSessionPageDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            editSessionPageDataGridView.Columns.AddRange(new DataGridViewColumn[] { StudyProject, Duration, StartDate, StartTime, EndDate, EndTime });
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(44, 45, 65);
            dataGridViewCellStyle7.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle7.ForeColor = SystemColors.ControlLight;
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(255, 81, 195);
            dataGridViewCellStyle7.SelectionForeColor = Color.White;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            editSessionPageDataGridView.DefaultCellStyle = dataGridViewCellStyle7;
            editSessionPageDataGridView.GridColor = Color.FromArgb(70, 71, 117);
            editSessionPageDataGridView.Location = new Point(-3, 0);
            editSessionPageDataGridView.MultiSelect = false;
            editSessionPageDataGridView.Name = "editSessionPageDataGridView";
            editSessionPageDataGridView.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.FromArgb(35, 34, 50);
            dataGridViewCellStyle8.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle8.ForeColor = SystemColors.ControlLight;
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(168, 228, 255);
            dataGridViewCellStyle8.SelectionForeColor = Color.FromArgb(35, 34, 50);
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            editSessionPageDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            editSessionPageDataGridView.RowHeadersVisible = false;
            editSessionPageDataGridView.RowHeadersWidth = 51;
            editSessionPageDataGridView.RowTemplate.DividerHeight = 1;
            editSessionPageDataGridView.RowTemplate.Height = 45;
            editSessionPageDataGridView.ShowCellErrors = false;
            editSessionPageDataGridView.ShowCellToolTips = false;
            editSessionPageDataGridView.ShowEditingIcon = false;
            editSessionPageDataGridView.ShowRowErrors = false;
            editSessionPageDataGridView.Size = new Size(1189, 604);
            editSessionPageDataGridView.TabIndex = 22;
            editSessionPageDataGridView.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Dark;
            editSessionPageDataGridView.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(44, 45, 65);
            editSessionPageDataGridView.ThemeStyle.AlternatingRowsStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            editSessionPageDataGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = SystemColors.ControlLight;
            editSessionPageDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.FromArgb(255, 81, 195);
            editSessionPageDataGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.White;
            editSessionPageDataGridView.ThemeStyle.BackColor = Color.FromArgb(35, 34, 50);
            editSessionPageDataGridView.ThemeStyle.GridColor = Color.FromArgb(70, 71, 117);
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
            // newSessionButton
            // 
            newSessionButton.Animated = true;
            newSessionButton.BorderColor = Color.HotPink;
            newSessionButton.BorderRadius = 15;
            newSessionButton.BorderThickness = 1;
            newSessionButton.CustomizableEdges = customizableEdges45;
            newSessionButton.EnableHoverRipple = true;
            newSessionButton.FillColor = Color.FromArgb(35, 34, 50);
            newSessionButton.FillColor2 = Color.FromArgb(35, 34, 50);
            newSessionButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            newSessionButton.ForeColor = Color.FloralWhite;
            newSessionButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            newSessionButton.Location = new Point(12, 172);
            newSessionButton.Name = "newSessionButton";
            newSessionButton.ShadowDecoration.CustomizableEdges = customizableEdges46;
            newSessionButton.Size = new Size(160, 40);
            newSessionButton.TabIndex = 42;
            newSessionButton.Text = "New Session";
            // 
            // dashboardButton
            // 
            dashboardButton.Animated = true;
            dashboardButton.BorderColor = Color.HotPink;
            dashboardButton.BorderRadius = 15;
            dashboardButton.BorderThickness = 1;
            dashboardButton.CustomizableEdges = customizableEdges47;
            dashboardButton.DisabledState.BorderColor = Color.DarkGray;
            dashboardButton.DisabledState.CustomBorderColor = Color.DarkGray;
            dashboardButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            dashboardButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            dashboardButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            dashboardButton.EnableHoverRipple = true;
            dashboardButton.FillColor = Color.FromArgb(35, 34, 50);
            dashboardButton.FillColor2 = Color.FromArgb(35, 34, 50);
            dashboardButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dashboardButton.ForeColor = Color.FloralWhite;
            dashboardButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            dashboardButton.Location = new Point(12, 101);
            dashboardButton.Name = "dashboardButton";
            dashboardButton.ShadowDecoration.CustomizableEdges = customizableEdges48;
            dashboardButton.Size = new Size(160, 40);
            dashboardButton.TabIndex = 0;
            dashboardButton.Text = "Dashboard";
            // 
            // sessionsButton
            // 
            sessionsButton.Animated = true;
            sessionsButton.BorderRadius = 15;
            sessionsButton.CustomizableEdges = customizableEdges49;
            sessionsButton.DisabledState.BorderColor = Color.DarkGray;
            sessionsButton.DisabledState.CustomBorderColor = Color.DarkGray;
            sessionsButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            sessionsButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            sessionsButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            sessionsButton.EnableHoverRipple = true;
            sessionsButton.FillColor = Color.FromArgb(255, 81, 195);
            sessionsButton.FillColor2 = Color.FromArgb(168, 228, 255);
            sessionsButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            sessionsButton.ForeColor = Color.White;
            sessionsButton.Location = new Point(12, 247);
            sessionsButton.Name = "sessionsButton";
            sessionsButton.ShadowDecoration.CustomizableEdges = customizableEdges50;
            sessionsButton.Size = new Size(160, 40);
            sessionsButton.TabIndex = 29;
            sessionsButton.Text = "Sessions";
            // 
            // logoutButton
            // 
            logoutButton.Animated = true;
            logoutButton.BorderRadius = 15;
            logoutButton.CustomizableEdges = customizableEdges51;
            logoutButton.DisabledState.BorderColor = Color.DarkGray;
            logoutButton.DisabledState.CustomBorderColor = Color.DarkGray;
            logoutButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            logoutButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            logoutButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            logoutButton.EnableHoverRipple = true;
            logoutButton.FillColor = Color.Transparent;
            logoutButton.FillColor2 = Color.Transparent;
            logoutButton.Font = new Font("Segoe UI", 10F);
            logoutButton.ForeColor = Color.FromArgb(206, 212, 218);
            logoutButton.Location = new Point(12, 668);
            logoutButton.Name = "logoutButton";
            logoutButton.ShadowDecoration.CustomizableEdges = customizableEdges52;
            logoutButton.Size = new Size(160, 40);
            logoutButton.TabIndex = 3;
            logoutButton.Text = "Logout";
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
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(1266, 780);
            Controls.Add(EditSessionPageMainPanel);
            Controls.Add(topPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "EditSessionPage";
            Text = "EditSessionForm";
            Load += EditSessionPage_Load;
            topPanel.ResumeLayout(false);
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
        private Guna.UI2.WinForms.Guna2HtmlLabel EditSessionsPageSessionsLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel EditSessionPageSortByLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel EditSessionPageSessionsLabel;
        private Guna.UI2.WinForms.Guna2ComboBox EditSessionPageComboBox;
        private Guna.UI2.WinForms.Guna2GradientButton CodingSessionPageHomeButton;
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
    }
}