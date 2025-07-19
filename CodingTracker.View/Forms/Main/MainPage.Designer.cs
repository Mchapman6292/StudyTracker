using CodingTracker.View.Forms.Services.SharedFormServices.CustomGradientButtons;
using CodingTracker.View.Properties;
using Guna.Charts.WinForms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Windows.Forms;



namespace CodingTracker.View
{
    partial class MainPage
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
            CustomizableEdges customizableEdges1 = new CustomizableEdges();
            CustomizableEdges customizableEdges2 = new CustomizableEdges();
            CustomizableEdges customizableEdges3 = new CustomizableEdges();
            CustomizableEdges customizableEdges4 = new CustomizableEdges();
            CustomizableEdges customizableEdges5 = new CustomizableEdges();
            CustomizableEdges customizableEdges6 = new CustomizableEdges();
            CustomizableEdges customizableEdges7 = new CustomizableEdges();
            CustomizableEdges customizableEdges8 = new CustomizableEdges();
            CustomizableEdges customizableEdges9 = new CustomizableEdges();
            CustomizableEdges customizableEdges10 = new CustomizableEdges();
            CustomizableEdges customizableEdges33 = new CustomizableEdges();
            CustomizableEdges customizableEdges34 = new CustomizableEdges();
            CustomizableEdges customizableEdges11 = new CustomizableEdges();
            CustomizableEdges customizableEdges12 = new CustomizableEdges();
            CustomizableEdges customizableEdges17 = new CustomizableEdges();
            CustomizableEdges customizableEdges18 = new CustomizableEdges();
            CustomizableEdges customizableEdges13 = new CustomizableEdges();
            CustomizableEdges customizableEdges14 = new CustomizableEdges();
            CustomizableEdges customizableEdges15 = new CustomizableEdges();
            CustomizableEdges customizableEdges16 = new CustomizableEdges();
            CustomizableEdges customizableEdges31 = new CustomizableEdges();
            CustomizableEdges customizableEdges32 = new CustomizableEdges();
            CustomizableEdges customizableEdges19 = new CustomizableEdges();
            CustomizableEdges customizableEdges20 = new CustomizableEdges();
            CustomizableEdges customizableEdges21 = new CustomizableEdges();
            CustomizableEdges customizableEdges22 = new CustomizableEdges();
            CustomizableEdges customizableEdges23 = new CustomizableEdges();
            CustomizableEdges customizableEdges24 = new CustomizableEdges();
            CustomizableEdges customizableEdges25 = new CustomizableEdges();
            CustomizableEdges customizableEdges26 = new CustomizableEdges();
            CustomizableEdges customizableEdges27 = new CustomizableEdges();
            CustomizableEdges customizableEdges28 = new CustomizableEdges();
            CustomizableEdges customizableEdges29 = new CustomizableEdges();
            CustomizableEdges customizableEdges30 = new CustomizableEdges();
            CustomizableEdges customizableEdges35 = new CustomizableEdges();
            CustomizableEdges customizableEdges36 = new CustomizableEdges();
            CustomizableEdges customizableEdges41 = new CustomizableEdges();
            CustomizableEdges customizableEdges42 = new CustomizableEdges();
            CustomizableEdges customizableEdges37 = new CustomizableEdges();
            CustomizableEdges customizableEdges38 = new CustomizableEdges();
            CustomizableEdges customizableEdges39 = new CustomizableEdges();
            CustomizableEdges customizableEdges40 = new CustomizableEdges();
            closeButton = new Guna2ControlBox();
            minimizeButton = new Guna2ControlBox();
            TodayTotalPanel = new Guna2GradientPanel();
            todayTotalLabel = new Guna2HtmlLabel();
            guna2GradientPanel3 = new Guna2GradientPanel();
            WeekTotalLabel = new Guna2HtmlLabel();
            averageSessionPanel = new Guna2GradientPanel();
            guna2HtmlLabel9 = new Guna2HtmlLabel();
            AverageSessionLabel = new Guna2HtmlLabel();
            doughnutDataset = new GunaDoughnutDataset();
            bottomHalfParentPanel = new Guna2Panel();
            guna2Separator1 = new Guna2Separator();
            starRatingPanel = new Guna2GradientPanel();
            iconPictureBox4 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox3 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox2 = new FontAwesome.Sharp.IconPictureBox();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            starRatingPictureBox = new FontAwesome.Sharp.IconPictureBox();
            guna2HtmlLabel7 = new Guna2HtmlLabel();
            starRatingAverageLabel = new Guna2HtmlLabel();
            guna2HtmlLabel8 = new Guna2HtmlLabel();
            starRatingTotalLabel = new Guna2HtmlLabel();
            starRatingsTitleLabel = new Guna2HtmlLabel();
            starRatingsPieChart = new LiveChartsCore.SkiaSharpView.WinForms.PieChart();
            guna2GradientPanel10 = new Guna2GradientPanel();
            mossPictureBoxGif = new Guna2PictureBox();
            readyToBeginLabel = new Guna2HtmlLabel();
            startSessionButton = new CustomGradientButton();
            guna2HtmlLabel2 = new Guna2HtmlLabel();
            Last28DaysPanel = new Guna2GradientPanel();
            last7DaysLabel = new Guna2HtmlLabel();
            viewSessionButton = new CustomGradientButton();
            guna2HtmlLabel13 = new Guna2HtmlLabel();
            recentActivityLabel = new Guna2HtmlLabel();
            activityParentPanel = new Guna2GradientPanel();
            zeroMinutesLegendPanel = new Guna2GradientPanel();
            guna2HtmlLabel5 = new Guna2HtmlLabel();
            guna2HtmlLabel4 = new Guna2HtmlLabel();
            lessThanOneHourLegendPanel = new Guna2GradientPanel();
            guna2HtmlLabel3 = new Guna2HtmlLabel();
            betweenTwoAndFourHoursLegendPanel = new Guna2GradientPanel();
            guna2HtmlLabel1 = new Guna2HtmlLabel();
            betweenOneAndTwoHoursLegendPanel = new Guna2GradientPanel();
            todayTotalParentPanel = new Guna2GradientPanel();
            gunaAnimationWindow = new Guna2AnimateWindow(components);
            guna2HtmlLabel6 = new Guna2HtmlLabel();
            topHalfSeperator = new Guna2Separator();
            githubPictureBox = new FontAwesome.Sharp.IconPictureBox();
            mainPageDragControl = new Guna2DragControl(components);
            statsPanelsParentPanel = new Guna2GradientPanel();
            averageSessionContainerPanel = new Guna2GradientPanel();
            guna2GradientPanel7 = new Guna2GradientPanel();
            guna2AnimateWindow1 = new Guna2AnimateWindow(components);
            iconPictureBox5 = new FontAwesome.Sharp.IconPictureBox();
            TodayTotalPanel.SuspendLayout();
            guna2GradientPanel3.SuspendLayout();
            averageSessionPanel.SuspendLayout();
            bottomHalfParentPanel.SuspendLayout();
            starRatingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)starRatingPictureBox).BeginInit();
            guna2GradientPanel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mossPictureBoxGif).BeginInit();
            Last28DaysPanel.SuspendLayout();
            todayTotalParentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)githubPictureBox).BeginInit();
            statsPanelsParentPanel.SuspendLayout();
            averageSessionContainerPanel.SuspendLayout();
            guna2GradientPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox5).BeginInit();
            SuspendLayout();
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.Cursor = Cursors.Hand;
            closeButton.CustomClick = true;
            closeButton.CustomizableEdges = customizableEdges1;
            closeButton.FillColor = Color.FromArgb(25, 24, 40);
            closeButton.HoverState.IconColor = Color.White;
            closeButton.IconColor = Color.White;
            closeButton.Location = new Point(1254, 0);
            closeButton.Name = "closeButton";
            closeButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
            closeButton.Size = new Size(45, 34);
            closeButton.TabIndex = 25;
            closeButton.Click += CloseButton_Click;
            // 
            // minimizeButton
            // 
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            minimizeButton.Cursor = Cursors.Hand;
            minimizeButton.CustomizableEdges = customizableEdges3;
            minimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            minimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            minimizeButton.HoverState.IconColor = Color.White;
            minimizeButton.IconColor = Color.White;
            minimizeButton.Location = new Point(1212, 0);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
            minimizeButton.Size = new Size(45, 34);
            minimizeButton.TabIndex = 26;
            // 
            // TodayTotalPanel
            // 
            TodayTotalPanel.Anchor = AnchorStyles.None;
            TodayTotalPanel.BackColor = Color.FromArgb(44, 45, 65);
            TodayTotalPanel.BorderColor = Color.FromArgb(25, 255, 255, 255);
            TodayTotalPanel.BorderRadius = 20;
            TodayTotalPanel.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom;
            TodayTotalPanel.Controls.Add(todayTotalLabel);
            TodayTotalPanel.CustomBorderColor = Color.FromArgb(35, 34, 50);
            TodayTotalPanel.CustomizableEdges = customizableEdges5;
            TodayTotalPanel.FillColor = Color.FromArgb(255, 182, 218);
            TodayTotalPanel.FillColor2 = Color.FromArgb(168, 228, 255);
            TodayTotalPanel.ForeColor = Color.FromArgb(44, 45, 65);
            TodayTotalPanel.Location = new Point(18, 14);
            TodayTotalPanel.Name = "TodayTotalPanel";
            TodayTotalPanel.ShadowDecoration.BorderRadius = 33;
            TodayTotalPanel.ShadowDecoration.Color = Color.FromArgb(25, 255, 255, 255);
            TodayTotalPanel.ShadowDecoration.CustomizableEdges = customizableEdges6;
            TodayTotalPanel.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            TodayTotalPanel.Size = new Size(232, 116);
            TodayTotalPanel.TabIndex = 28;
            // 
            // todayTotalLabel
            // 
            todayTotalLabel.AutoSize = false;
            todayTotalLabel.BackColor = Color.Transparent;
            todayTotalLabel.IsContextMenuEnabled = false;
            todayTotalLabel.Location = new Point(27, 15);
            todayTotalLabel.Name = "todayTotalLabel";
            todayTotalLabel.Size = new Size(186, 88);
            todayTotalLabel.TabIndex = 2;
            todayTotalLabel.Text = null;
            // 
            // guna2GradientPanel3
            // 
            guna2GradientPanel3.Anchor = AnchorStyles.None;
            guna2GradientPanel3.BackColor = Color.Transparent;
            guna2GradientPanel3.BorderRadius = 30;
            guna2GradientPanel3.Controls.Add(WeekTotalLabel);
            guna2GradientPanel3.CustomBorderColor = Color.Transparent;
            guna2GradientPanel3.CustomizableEdges = customizableEdges7;
            guna2GradientPanel3.FillColor = Color.FromArgb(180, 190, 254);
            guna2GradientPanel3.FillColor2 = Color.FromArgb(252, 124, 180);
            guna2GradientPanel3.ForeColor = Color.Transparent;
            guna2GradientPanel3.Location = new Point(18, 14);
            guna2GradientPanel3.Name = "guna2GradientPanel3";
            guna2GradientPanel3.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2GradientPanel3.Size = new Size(232, 116);
            guna2GradientPanel3.TabIndex = 29;
            // 
            // WeekTotalLabel
            // 
            WeekTotalLabel.AutoSize = false;
            WeekTotalLabel.BackColor = Color.Transparent;
            WeekTotalLabel.IsContextMenuEnabled = false;
            WeekTotalLabel.Location = new Point(16, 15);
            WeekTotalLabel.Name = "WeekTotalLabel";
            WeekTotalLabel.Size = new Size(186, 88);
            WeekTotalLabel.TabIndex = 1;
            WeekTotalLabel.Text = null;
            // 
            // averageSessionPanel
            // 
            averageSessionPanel.Anchor = AnchorStyles.None;
            averageSessionPanel.BackColor = Color.Transparent;
            averageSessionPanel.BorderRadius = 30;
            averageSessionPanel.Controls.Add(guna2HtmlLabel9);
            averageSessionPanel.Controls.Add(AverageSessionLabel);
            averageSessionPanel.CustomizableEdges = customizableEdges9;
            averageSessionPanel.FillColor = Color.FromArgb(168, 228, 255);
            averageSessionPanel.FillColor2 = Color.FromArgb(242, 130, 220);
            averageSessionPanel.ForeColor = Color.Transparent;
            averageSessionPanel.Location = new Point(18, 14);
            averageSessionPanel.Name = "averageSessionPanel";
            averageSessionPanel.ShadowDecoration.CustomizableEdges = customizableEdges10;
            averageSessionPanel.Size = new Size(232, 116);
            averageSessionPanel.TabIndex = 30;
            // 
            // guna2HtmlLabel9
            // 
            guna2HtmlLabel9.AutoSize = false;
            guna2HtmlLabel9.BackColor = Color.Transparent;
            guna2HtmlLabel9.IsContextMenuEnabled = false;
            guna2HtmlLabel9.Location = new Point(-423, 0);
            guna2HtmlLabel9.Name = "guna2HtmlLabel9";
            guna2HtmlLabel9.Size = new Size(186, 88);
            guna2HtmlLabel9.TabIndex = 1;
            guna2HtmlLabel9.Text = null;
            // 
            // AverageSessionLabel
            // 
            AverageSessionLabel.AutoSize = false;
            AverageSessionLabel.BackColor = Color.Transparent;
            AverageSessionLabel.IsContextMenuEnabled = false;
            AverageSessionLabel.Location = new Point(16, 15);
            AverageSessionLabel.Name = "AverageSessionLabel";
            AverageSessionLabel.Size = new Size(186, 88);
            AverageSessionLabel.TabIndex = 3;
            AverageSessionLabel.Text = null;
            // 
            // doughnutDataset
            // 
            doughnutDataset.FillColors.AddRange(new Color[] { Color.FromArgb(255, 81, 195), Color.FromArgb(255, 120, 200), Color.FromArgb(200, 150, 220), Color.FromArgb(150, 180, 240), Color.FromArgb(100, 200, 250), Color.FromArgb(100, 220, 220), Color.FromArgb(168, 228, 255) });
            doughnutDataset.Label = "Doughnut1";
            // 
            // bottomHalfParentPanel
            // 
            bottomHalfParentPanel.BackColor = Color.Transparent;
            bottomHalfParentPanel.Controls.Add(guna2Separator1);
            bottomHalfParentPanel.Controls.Add(starRatingPanel);
            bottomHalfParentPanel.Controls.Add(guna2GradientPanel10);
            bottomHalfParentPanel.Controls.Add(Last28DaysPanel);
            bottomHalfParentPanel.CustomizableEdges = customizableEdges33;
            bottomHalfParentPanel.Location = new Point(0, 226);
            bottomHalfParentPanel.Name = "bottomHalfParentPanel";
            bottomHalfParentPanel.ShadowDecoration.CustomizableEdges = customizableEdges34;
            bottomHalfParentPanel.Size = new Size(1298, 557);
            bottomHalfParentPanel.TabIndex = 29;
            // 
            // guna2Separator1
            // 
            guna2Separator1.FillColor = Color.FromArgb(115, 80, 160);
            guna2Separator1.Location = new Point(0, 0);
            guna2Separator1.Margin = new Padding(3, 2, 3, 2);
            guna2Separator1.Name = "guna2Separator1";
            guna2Separator1.Size = new Size(1300, 10);
            guna2Separator1.TabIndex = 48;
            // 
            // starRatingPanel
            // 
            starRatingPanel.BackColor = Color.Transparent;
            starRatingPanel.BorderRadius = 25;
            starRatingPanel.Controls.Add(iconPictureBox4);
            starRatingPanel.Controls.Add(iconPictureBox3);
            starRatingPanel.Controls.Add(iconPictureBox2);
            starRatingPanel.Controls.Add(iconPictureBox1);
            starRatingPanel.Controls.Add(starRatingPictureBox);
            starRatingPanel.Controls.Add(guna2HtmlLabel7);
            starRatingPanel.Controls.Add(starRatingAverageLabel);
            starRatingPanel.Controls.Add(guna2HtmlLabel8);
            starRatingPanel.Controls.Add(starRatingTotalLabel);
            starRatingPanel.Controls.Add(starRatingsTitleLabel);
            starRatingPanel.Controls.Add(starRatingsPieChart);
            starRatingPanel.CustomizableEdges = customizableEdges11;
            starRatingPanel.FillColor = Color.FromArgb(44, 45, 65);
            starRatingPanel.FillColor2 = Color.FromArgb(44, 45, 65);
            starRatingPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            starRatingPanel.Location = new Point(868, 24);
            starRatingPanel.Name = "starRatingPanel";
            starRatingPanel.ShadowDecoration.BorderRadius = 12;
            starRatingPanel.ShadowDecoration.Color = Color.FromArgb(35, 34, 50);
            starRatingPanel.ShadowDecoration.CustomizableEdges = customizableEdges12;
            starRatingPanel.ShadowDecoration.Enabled = true;
            starRatingPanel.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            starRatingPanel.Size = new Size(340, 366);
            starRatingPanel.TabIndex = 40;
            // 
            // iconPictureBox4
            // 
            iconPictureBox4.BackColor = Color.Transparent;
            iconPictureBox4.ForeColor = Color.FromArgb(255, 160, 210);
            iconPictureBox4.IconChar = FontAwesome.Sharp.IconChar.Star;
            iconPictureBox4.IconColor = Color.FromArgb(255, 160, 210);
            iconPictureBox4.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox4.IconSize = 20;
            iconPictureBox4.Location = new Point(104, 37);
            iconPictureBox4.Name = "iconPictureBox4";
            iconPictureBox4.Size = new Size(20, 20);
            iconPictureBox4.TabIndex = 50;
            iconPictureBox4.TabStop = false;
            // 
            // iconPictureBox3
            // 
            iconPictureBox3.BackColor = Color.Transparent;
            iconPictureBox3.ForeColor = Color.FromArgb(255, 160, 210);
            iconPictureBox3.IconChar = FontAwesome.Sharp.IconChar.Star;
            iconPictureBox3.IconColor = Color.FromArgb(255, 160, 210);
            iconPictureBox3.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox3.IconSize = 20;
            iconPictureBox3.Location = new Point(84, 37);
            iconPictureBox3.Name = "iconPictureBox3";
            iconPictureBox3.Size = new Size(20, 20);
            iconPictureBox3.TabIndex = 49;
            iconPictureBox3.TabStop = false;
            // 
            // iconPictureBox2
            // 
            iconPictureBox2.BackColor = Color.Transparent;
            iconPictureBox2.ForeColor = Color.FromArgb(255, 160, 210);
            iconPictureBox2.IconChar = FontAwesome.Sharp.IconChar.Star;
            iconPictureBox2.IconColor = Color.FromArgb(255, 160, 210);
            iconPictureBox2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox2.IconSize = 20;
            iconPictureBox2.Location = new Point(62, 37);
            iconPictureBox2.Name = "iconPictureBox2";
            iconPictureBox2.Size = new Size(20, 20);
            iconPictureBox2.TabIndex = 48;
            iconPictureBox2.TabStop = false;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.Transparent;
            iconPictureBox1.ForeColor = Color.FromArgb(255, 160, 210);
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.Star;
            iconPictureBox1.IconColor = Color.FromArgb(255, 160, 210);
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 20;
            iconPictureBox1.Location = new Point(42, 37);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(20, 20);
            iconPictureBox1.TabIndex = 47;
            iconPictureBox1.TabStop = false;
            // 
            // starRatingPictureBox
            // 
            starRatingPictureBox.BackColor = Color.Transparent;
            starRatingPictureBox.ForeColor = Color.FromArgb(255, 160, 210);
            starRatingPictureBox.IconChar = FontAwesome.Sharp.IconChar.Star;
            starRatingPictureBox.IconColor = Color.FromArgb(255, 160, 210);
            starRatingPictureBox.IconFont = FontAwesome.Sharp.IconFont.Auto;
            starRatingPictureBox.IconSize = 20;
            starRatingPictureBox.Location = new Point(21, 37);
            starRatingPictureBox.Name = "starRatingPictureBox";
            starRatingPictureBox.Size = new Size(20, 20);
            starRatingPictureBox.TabIndex = 46;
            starRatingPictureBox.TabStop = false;
            // 
            // guna2HtmlLabel7
            // 
            guna2HtmlLabel7.AutoSize = false;
            guna2HtmlLabel7.BackColor = Color.Transparent;
            guna2HtmlLabel7.Font = new Font("Segoe UI", 10F);
            guna2HtmlLabel7.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel7.Location = new Point(231, 320);
            guna2HtmlLabel7.Name = "guna2HtmlLabel7";
            guna2HtmlLabel7.Size = new Size(102, 24);
            guna2HtmlLabel7.TabIndex = 5;
            guna2HtmlLabel7.Text = "Average Rating";
            guna2HtmlLabel7.TextAlignment = ContentAlignment.TopCenter;
            // 
            // starRatingAverageLabel
            // 
            starRatingAverageLabel.BackColor = Color.Transparent;
            starRatingAverageLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            starRatingAverageLabel.ForeColor = Color.FromArgb(168, 228, 255);
            starRatingAverageLabel.Location = new Point(276, 280);
            starRatingAverageLabel.Name = "starRatingAverageLabel";
            starRatingAverageLabel.Size = new Size(30, 27);
            starRatingAverageLabel.TabIndex = 2;
            starRatingAverageLabel.Text = "4.2";
            starRatingAverageLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel8
            // 
            guna2HtmlLabel8.AutoSize = false;
            guna2HtmlLabel8.BackColor = Color.Transparent;
            guna2HtmlLabel8.Font = new Font("Segoe UI", 10F);
            guna2HtmlLabel8.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel8.Location = new Point(11, 320);
            guna2HtmlLabel8.Name = "guna2HtmlLabel8";
            guna2HtmlLabel8.Size = new Size(102, 24);
            guna2HtmlLabel8.TabIndex = 6;
            guna2HtmlLabel8.Text = "Total Sessions";
            guna2HtmlLabel8.TextAlignment = ContentAlignment.TopCenter;
            // 
            // starRatingTotalLabel
            // 
            starRatingTotalLabel.BackColor = Color.Transparent;
            starRatingTotalLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            starRatingTotalLabel.ForeColor = Color.FromArgb(168, 228, 255);
            starRatingTotalLabel.Location = new Point(38, 287);
            starRatingTotalLabel.Name = "starRatingTotalLabel";
            starRatingTotalLabel.Size = new Size(36, 27);
            starRatingTotalLabel.TabIndex = 3;
            starRatingTotalLabel.Text = "147 ";
            starRatingTotalLabel.TextAlignment = ContentAlignment.TopCenter;
            // 
            // starRatingsTitleLabel
            // 
            starRatingsTitleLabel.Anchor = AnchorStyles.None;
            starRatingsTitleLabel.AutoSize = false;
            starRatingsTitleLabel.BackColor = Color.Transparent;
            starRatingsTitleLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            starRatingsTitleLabel.ForeColor = Color.HotPink;
            starRatingsTitleLabel.Location = new Point(0, 7);
            starRatingsTitleLabel.Name = "starRatingsTitleLabel";
            starRatingsTitleLabel.Size = new Size(124, 25);
            starRatingsTitleLabel.TabIndex = 1;
            starRatingsTitleLabel.Text = "Star Ratings";
            starRatingsTitleLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // starRatingsPieChart
            // 
            starRatingsPieChart.AutoValidate = AutoValidate.EnableAllowFocusChange;
            starRatingsPieChart.BackColor = Color.FromArgb(44, 45, 65);
            starRatingsPieChart.Font = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            starRatingsPieChart.InitialRotation = 0D;
            starRatingsPieChart.IsClockwise = true;
            starRatingsPieChart.Location = new Point(60, 81);
            starRatingsPieChart.MaxAngle = 360D;
            starRatingsPieChart.MaxValue = double.NaN;
            starRatingsPieChart.MinValue = 0D;
            starRatingsPieChart.Name = "starRatingsPieChart";
            starRatingsPieChart.Size = new Size(215, 173);
            starRatingsPieChart.TabIndex = 6;
            // 
            // guna2GradientPanel10
            // 
            guna2GradientPanel10.BackColor = Color.Transparent;
            guna2GradientPanel10.BorderColor = Color.FromArgb(44, 45, 65);
            guna2GradientPanel10.BorderRadius = 25;
            guna2GradientPanel10.Controls.Add(mossPictureBoxGif);
            guna2GradientPanel10.Controls.Add(readyToBeginLabel);
            guna2GradientPanel10.Controls.Add(startSessionButton);
            guna2GradientPanel10.Controls.Add(guna2HtmlLabel2);
            guna2GradientPanel10.CustomizableEdges = customizableEdges17;
            guna2GradientPanel10.FillColor = Color.FromArgb(44, 45, 65);
            guna2GradientPanel10.FillColor2 = Color.FromArgb(44, 45, 65);
            guna2GradientPanel10.ForeColor = Color.FromArgb(44, 45, 65);
            guna2GradientPanel10.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            guna2GradientPanel10.Location = new Point(832, 407);
            guna2GradientPanel10.Name = "guna2GradientPanel10";
            guna2GradientPanel10.ShadowDecoration.BorderRadius = 12;
            guna2GradientPanel10.ShadowDecoration.Color = Color.FromArgb(44, 45, 65);
            guna2GradientPanel10.ShadowDecoration.CustomizableEdges = customizableEdges18;
            guna2GradientPanel10.ShadowDecoration.Enabled = true;
            guna2GradientPanel10.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            guna2GradientPanel10.Size = new Size(445, 122);
            guna2GradientPanel10.TabIndex = 47;
            // 
            // mossPictureBoxGif
            // 
            mossPictureBoxGif.CustomizableEdges = customizableEdges13;
            mossPictureBoxGif.Image = Resources.The_IT_Crowd_Intro_BitMap;
            mossPictureBoxGif.ImageRotate = 0F;
            mossPictureBoxGif.Location = new Point(362, 16);
            mossPictureBoxGif.Name = "mossPictureBoxGif";
            mossPictureBoxGif.ShadowDecoration.CustomizableEdges = customizableEdges14;
            mossPictureBoxGif.Size = new Size(80, 75);
            mossPictureBoxGif.SizeMode = PictureBoxSizeMode.Zoom;
            mossPictureBoxGif.TabIndex = 9;
            mossPictureBoxGif.TabStop = false;
            mossPictureBoxGif.Visible = false;
            // 
            // readyToBeginLabel
            // 
            readyToBeginLabel.Anchor = AnchorStyles.None;
            readyToBeginLabel.BackColor = Color.Transparent;
            readyToBeginLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            readyToBeginLabel.ForeColor = Color.HotPink;
            readyToBeginLabel.Location = new Point(129, 3);
            readyToBeginLabel.Name = "readyToBeginLabel";
            readyToBeginLabel.Size = new Size(135, 25);
            readyToBeginLabel.TabIndex = 36;
            readyToBeginLabel.Text = "Ready To Begin?";
            readyToBeginLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // startSessionButton
            // 
            startSessionButton.Animated = true;
            startSessionButton.BackColor = Color.Transparent;
            startSessionButton.BorderColor = Color.FromArgb(35, 34, 50);
            startSessionButton.BorderRadius = 15;
            startSessionButton.Cursor = Cursors.Hand;
            startSessionButton.CustomizableEdges = customizableEdges15;
            startSessionButton.DisabledState.BorderColor = Color.DarkGray;
            startSessionButton.DisabledState.CustomBorderColor = Color.DarkGray;
            startSessionButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            startSessionButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            startSessionButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            startSessionButton.EnableHoverRipple = true;
            startSessionButton.FillColor = Color.FromArgb(255, 81, 195);
            startSessionButton.FillColor2 = Color.FromArgb(168, 228, 255);
            startSessionButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            startSessionButton.ForeColor = Color.FloralWhite;
            startSessionButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            startSessionButton.HoverState.FillColor = Color.FromArgb(255, 81, 195);
            startSessionButton.HoverState.FillColor2 = Color.FromArgb(168, 228, 255);
            startSessionButton.Location = new Point(74, 58);
            startSessionButton.Name = "startSessionButton";
            startSessionButton.ShadowDecoration.CustomizableEdges = customizableEdges16;
            startSessionButton.Size = new Size(268, 44);
            startSessionButton.TabIndex = 34;
            startSessionButton.Text = " Start Session";
            startSessionButton.Click += startSessionButton_Click;
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.Anchor = AnchorStyles.None;
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Enabled = false;
            guna2HtmlLabel2.Font = new Font("Segoe UI", 10F);
            guna2HtmlLabel2.ForeColor = SystemColors.ControlLight;
            guna2HtmlLabel2.Location = new Point(126, 33);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(149, 19);
            guna2HtmlLabel2.TabIndex = 37;
            guna2HtmlLabel2.Text = "Keep building that streak";
            guna2HtmlLabel2.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // Last28DaysPanel
            // 
            Last28DaysPanel.Anchor = AnchorStyles.None;
            Last28DaysPanel.BackColor = Color.Transparent;
            Last28DaysPanel.BorderColor = Color.FromArgb(25, 255, 255, 255);
            Last28DaysPanel.BorderRadius = 25;
            Last28DaysPanel.Controls.Add(last7DaysLabel);
            Last28DaysPanel.Controls.Add(viewSessionButton);
            Last28DaysPanel.Controls.Add(guna2HtmlLabel13);
            Last28DaysPanel.Controls.Add(recentActivityLabel);
            Last28DaysPanel.Controls.Add(activityParentPanel);
            Last28DaysPanel.Controls.Add(zeroMinutesLegendPanel);
            Last28DaysPanel.Controls.Add(guna2HtmlLabel5);
            Last28DaysPanel.Controls.Add(guna2HtmlLabel4);
            Last28DaysPanel.Controls.Add(lessThanOneHourLegendPanel);
            Last28DaysPanel.Controls.Add(guna2HtmlLabel3);
            Last28DaysPanel.Controls.Add(betweenTwoAndFourHoursLegendPanel);
            Last28DaysPanel.Controls.Add(guna2HtmlLabel1);
            Last28DaysPanel.Controls.Add(betweenOneAndTwoHoursLegendPanel);
            Last28DaysPanel.CustomizableEdges = customizableEdges31;
            Last28DaysPanel.FillColor = Color.FromArgb(44, 45, 65);
            Last28DaysPanel.FillColor2 = Color.FromArgb(44, 45, 65);
            Last28DaysPanel.ForeColor = SystemColors.ControlText;
            Last28DaysPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            Last28DaysPanel.Location = new Point(34, 24);
            Last28DaysPanel.Name = "Last28DaysPanel";
            Last28DaysPanel.ShadowDecoration.Color = Color.FromArgb(35, 34, 50);
            Last28DaysPanel.ShadowDecoration.CustomizableEdges = customizableEdges32;
            Last28DaysPanel.ShadowDecoration.Depth = 50;
            Last28DaysPanel.ShadowDecoration.Enabled = true;
            Last28DaysPanel.Size = new Size(754, 505);
            Last28DaysPanel.TabIndex = 6;
            // 
            // last7DaysLabel
            // 
            last7DaysLabel.Anchor = AnchorStyles.None;
            last7DaysLabel.BackColor = Color.Transparent;
            last7DaysLabel.Enabled = false;
            last7DaysLabel.Font = new Font("Segoe UI", 10F);
            last7DaysLabel.ForeColor = SystemColors.ControlLight;
            last7DaysLabel.Location = new Point(11, 38);
            last7DaysLabel.Name = "last7DaysLabel";
            last7DaysLabel.Size = new Size(68, 19);
            last7DaysLabel.TabIndex = 10;
            last7DaysLabel.Text = "Last 7 days";
            last7DaysLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // viewSessionButton
            // 
            viewSessionButton.Animated = true;
            viewSessionButton.AnimatedGIF = true;
            viewSessionButton.BackColor = Color.Transparent;
            viewSessionButton.BorderColor = Color.FromArgb(35, 34, 50);
            viewSessionButton.BorderRadius = 15;
            viewSessionButton.Cursor = Cursors.Hand;
            viewSessionButton.CustomizableEdges = customizableEdges19;
            viewSessionButton.DisabledState.BorderColor = Color.DarkGray;
            viewSessionButton.DisabledState.CustomBorderColor = Color.DarkGray;
            viewSessionButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            viewSessionButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            viewSessionButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            viewSessionButton.EnableHoverRipple = true;
            viewSessionButton.FillColor = Color.FromArgb(255, 81, 195);
            viewSessionButton.FillColor2 = Color.FromArgb(168, 228, 255);
            viewSessionButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            viewSessionButton.ForeColor = Color.FloralWhite;
            viewSessionButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            viewSessionButton.HoverState.FillColor = Color.FromArgb(255, 81, 195);
            viewSessionButton.HoverState.FillColor2 = Color.FromArgb(168, 228, 255);
            viewSessionButton.Location = new Point(228, 430);
            viewSessionButton.Name = "viewSessionButton";
            viewSessionButton.ShadowDecoration.CustomizableEdges = customizableEdges20;
            viewSessionButton.Size = new Size(268, 44);
            viewSessionButton.TabIndex = 35;
            viewSessionButton.Text = "View Sessions";
            viewSessionButton.Click += viewSessionButton_Click;
            // 
            // guna2HtmlLabel13
            // 
            guna2HtmlLabel13.Anchor = AnchorStyles.None;
            guna2HtmlLabel13.BackColor = Color.Transparent;
            guna2HtmlLabel13.Enabled = false;
            guna2HtmlLabel13.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            guna2HtmlLabel13.ForeColor = Color.FromArgb(168, 228, 255);
            guna2HtmlLabel13.Location = new Point(297, 390);
            guna2HtmlLabel13.Name = "guna2HtmlLabel13";
            guna2HtmlLabel13.Size = new Size(135, 22);
            guna2HtmlLabel13.TabIndex = 39;
            guna2HtmlLabel13.Text = "Want to see more?";
            guna2HtmlLabel13.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // recentActivityLabel
            // 
            recentActivityLabel.Anchor = AnchorStyles.None;
            recentActivityLabel.BackColor = Color.Transparent;
            recentActivityLabel.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            recentActivityLabel.ForeColor = Color.HotPink;
            recentActivityLabel.Location = new Point(11, 7);
            recentActivityLabel.Name = "recentActivityLabel";
            recentActivityLabel.Size = new Size(124, 25);
            recentActivityLabel.TabIndex = 9;
            recentActivityLabel.Text = "Recent Activity";
            recentActivityLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // activityParentPanel
            // 
            activityParentPanel.BackColor = Color.Transparent;
            activityParentPanel.BorderColor = Color.FromArgb(70, 71, 117);
            activityParentPanel.BorderRadius = 8;
            activityParentPanel.BorderThickness = 1;
            activityParentPanel.CustomBorderThickness = new Padding(0, 0, 0, 4);
            customizableEdges21.TopLeft = false;
            customizableEdges21.TopRight = false;
            activityParentPanel.CustomizableEdges = customizableEdges21;
            activityParentPanel.FillColor = Color.FromArgb(44, 45, 65);
            activityParentPanel.FillColor2 = Color.FromArgb(44, 45, 65);
            activityParentPanel.Location = new Point(22, 81);
            activityParentPanel.Name = "activityParentPanel";
            activityParentPanel.ShadowDecoration.CustomizableEdges = customizableEdges22;
            activityParentPanel.Size = new Size(697, 297);
            activityParentPanel.TabIndex = 0;
            // 
            // zeroMinutesLegendPanel
            // 
            zeroMinutesLegendPanel.BackColor = Color.Transparent;
            zeroMinutesLegendPanel.BorderRadius = 4;
            zeroMinutesLegendPanel.CustomizableEdges = customizableEdges23;
            zeroMinutesLegendPanel.FillColor = Color.FromArgb(20, 60, 80);
            zeroMinutesLegendPanel.FillColor2 = Color.FromArgb(40, 100, 120);
            zeroMinutesLegendPanel.Location = new Point(249, 45);
            zeroMinutesLegendPanel.Name = "zeroMinutesLegendPanel";
            zeroMinutesLegendPanel.ShadowDecoration.CustomizableEdges = customizableEdges24;
            zeroMinutesLegendPanel.Size = new Size(23, 15);
            zeroMinutesLegendPanel.TabIndex = 1;
            // 
            // guna2HtmlLabel5
            // 
            guna2HtmlLabel5.BackColor = Color.Transparent;
            guna2HtmlLabel5.Font = new Font("Segoe UI", 9F);
            guna2HtmlLabel5.ForeColor = Color.Silver;
            guna2HtmlLabel5.Location = new Point(277, 45);
            guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            guna2HtmlLabel5.Size = new Size(33, 17);
            guna2HtmlLabel5.TabIndex = 2;
            guna2HtmlLabel5.Text = "0 min";
            // 
            // guna2HtmlLabel4
            // 
            guna2HtmlLabel4.BackColor = Color.Transparent;
            guna2HtmlLabel4.Font = new Font("Segoe UI", 9F);
            guna2HtmlLabel4.ForeColor = Color.Silver;
            guna2HtmlLabel4.Location = new Point(523, 45);
            guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            guna2HtmlLabel4.Size = new Size(31, 17);
            guna2HtmlLabel4.TabIndex = 8;
            guna2HtmlLabel4.Text = "2-4hr";
            // 
            // lessThanOneHourLegendPanel
            // 
            lessThanOneHourLegendPanel.BackColor = Color.Transparent;
            lessThanOneHourLegendPanel.BorderRadius = 4;
            lessThanOneHourLegendPanel.CustomizableEdges = customizableEdges25;
            lessThanOneHourLegendPanel.FillColor = Color.FromArgb(40, 140, 160);
            lessThanOneHourLegendPanel.FillColor2 = Color.FromArgb(80, 200, 220);
            lessThanOneHourLegendPanel.Location = new Point(329, 45);
            lessThanOneHourLegendPanel.Name = "lessThanOneHourLegendPanel";
            lessThanOneHourLegendPanel.ShadowDecoration.CustomizableEdges = customizableEdges26;
            lessThanOneHourLegendPanel.Size = new Size(23, 15);
            lessThanOneHourLegendPanel.TabIndex = 3;
            // 
            // guna2HtmlLabel3
            // 
            guna2HtmlLabel3.BackColor = Color.Transparent;
            guna2HtmlLabel3.Font = new Font("Segoe UI", 9F);
            guna2HtmlLabel3.ForeColor = Color.Silver;
            guna2HtmlLabel3.Location = new Point(357, 45);
            guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            guna2HtmlLabel3.Size = new Size(20, 17);
            guna2HtmlLabel3.TabIndex = 4;
            guna2HtmlLabel3.Text = "< 1hr";
            // 
            // betweenTwoAndFourHoursLegendPanel
            // 
            betweenTwoAndFourHoursLegendPanel.BackColor = Color.Transparent;
            betweenTwoAndFourHoursLegendPanel.BorderColor = Color.FromArgb(255, 140, 200);
            betweenTwoAndFourHoursLegendPanel.BorderRadius = 4;
            betweenTwoAndFourHoursLegendPanel.BorderThickness = 1;
            betweenTwoAndFourHoursLegendPanel.CustomizableEdges = customizableEdges27;
            betweenTwoAndFourHoursLegendPanel.FillColor = Color.FromArgb(180, 100, 200);
            betweenTwoAndFourHoursLegendPanel.FillColor2 = Color.FromArgb(255, 120, 180);
            betweenTwoAndFourHoursLegendPanel.Location = new Point(492, 45);
            betweenTwoAndFourHoursLegendPanel.Name = "betweenTwoAndFourHoursLegendPanel";
            betweenTwoAndFourHoursLegendPanel.ShadowDecoration.Color = Color.FromArgb(255, 120, 200);
            betweenTwoAndFourHoursLegendPanel.ShadowDecoration.CustomizableEdges = customizableEdges28;
            betweenTwoAndFourHoursLegendPanel.ShadowDecoration.Enabled = true;
            betweenTwoAndFourHoursLegendPanel.ShadowDecoration.Shadow = new Padding(2);
            betweenTwoAndFourHoursLegendPanel.Size = new Size(23, 15);
            betweenTwoAndFourHoursLegendPanel.TabIndex = 7;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 9F);
            guna2HtmlLabel1.ForeColor = Color.Silver;
            guna2HtmlLabel1.Location = new Point(437, 45);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(31, 17);
            guna2HtmlLabel1.TabIndex = 6;
            guna2HtmlLabel1.Text = "1-2hr";
            // 
            // betweenOneAndTwoHoursLegendPanel
            // 
            betweenOneAndTwoHoursLegendPanel.BackColor = Color.Transparent;
            betweenOneAndTwoHoursLegendPanel.BorderRadius = 4;
            betweenOneAndTwoHoursLegendPanel.CustomizableEdges = customizableEdges29;
            betweenOneAndTwoHoursLegendPanel.FillColor = Color.FromArgb(80, 160, 200);
            betweenOneAndTwoHoursLegendPanel.FillColor2 = Color.FromArgb(140, 120, 220);
            betweenOneAndTwoHoursLegendPanel.ImeMode = ImeMode.NoControl;
            betweenOneAndTwoHoursLegendPanel.Location = new Point(409, 45);
            betweenOneAndTwoHoursLegendPanel.Name = "betweenOneAndTwoHoursLegendPanel";
            betweenOneAndTwoHoursLegendPanel.ShadowDecoration.Color = Color.FromArgb(140, 140, 255);
            betweenOneAndTwoHoursLegendPanel.ShadowDecoration.CustomizableEdges = customizableEdges30;
            betweenOneAndTwoHoursLegendPanel.ShadowDecoration.Enabled = true;
            betweenOneAndTwoHoursLegendPanel.ShadowDecoration.Shadow = new Padding(1);
            betweenOneAndTwoHoursLegendPanel.Size = new Size(23, 15);
            betweenOneAndTwoHoursLegendPanel.TabIndex = 5;
            // 
            // todayTotalParentPanel
            // 
            todayTotalParentPanel.BackColor = Color.Transparent;
            todayTotalParentPanel.BorderColor = Color.FromArgb(44, 45, 65);
            todayTotalParentPanel.BorderRadius = 25;
            todayTotalParentPanel.Controls.Add(TodayTotalPanel);
            todayTotalParentPanel.CustomizableEdges = customizableEdges35;
            todayTotalParentPanel.FillColor = Color.FromArgb(44, 45, 65);
            todayTotalParentPanel.FillColor2 = Color.FromArgb(44, 45, 65);
            todayTotalParentPanel.ForeColor = Color.FromArgb(44, 45, 65);
            todayTotalParentPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            todayTotalParentPanel.Location = new Point(34, 15);
            todayTotalParentPanel.Name = "todayTotalParentPanel";
            todayTotalParentPanel.ShadowDecoration.BorderRadius = 12;
            todayTotalParentPanel.ShadowDecoration.Color = Color.FromArgb(44, 45, 65);
            todayTotalParentPanel.ShadowDecoration.CustomizableEdges = customizableEdges36;
            todayTotalParentPanel.ShadowDecoration.Enabled = true;
            todayTotalParentPanel.Size = new Size(270, 144);
            todayTotalParentPanel.TabIndex = 43;
            // 
            // gunaAnimationWindow
            // 
            gunaAnimationWindow.AnimationType = Guna2AnimateWindow.AnimateWindowType.AW_BLEND;
            gunaAnimationWindow.TargetForm = this;
            // 
            // guna2HtmlLabel6
            // 
            guna2HtmlLabel6.Anchor = AnchorStyles.None;
            guna2HtmlLabel6.AutoSize = false;
            guna2HtmlLabel6.BackColor = Color.Transparent;
            guna2HtmlLabel6.Font = new Font("Century Gothic", 20F, FontStyle.Bold);
            guna2HtmlLabel6.ForeColor = Color.FromArgb(204, 84, 144);
            guna2HtmlLabel6.Location = new Point(46, 0);
            guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            guna2HtmlLabel6.Size = new Size(203, 34);
            guna2HtmlLabel6.TabIndex = 33;
            guna2HtmlLabel6.Text = "CodingTracker";
            guna2HtmlLabel6.TextAlignment = ContentAlignment.TopCenter;
            // 
            // topHalfSeperator
            // 
            topHalfSeperator.FillColor = Color.FromArgb(25, 255, 255, 255);
            topHalfSeperator.FillStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            topHalfSeperator.Location = new Point(0, 34);
            topHalfSeperator.Margin = new Padding(3, 2, 3, 2);
            topHalfSeperator.Name = "topHalfSeperator";
            topHalfSeperator.Size = new Size(1300, 10);
            topHalfSeperator.TabIndex = 40;
            // 
            // githubPictureBox
            // 
            githubPictureBox.BackColor = Color.FromArgb(35, 34, 50);
            githubPictureBox.ForeColor = Color.FromArgb(255, 160, 210);
            githubPictureBox.IconChar = FontAwesome.Sharp.IconChar.CodePullRequest;
            githubPictureBox.IconColor = Color.FromArgb(255, 160, 210);
            githubPictureBox.IconFont = FontAwesome.Sharp.IconFont.Auto;
            githubPictureBox.IconSize = 34;
            githubPictureBox.Location = new Point(0, 0);
            githubPictureBox.Margin = new Padding(3, 2, 3, 2);
            githubPictureBox.Name = "githubPictureBox";
            githubPictureBox.Size = new Size(35, 34);
            githubPictureBox.TabIndex = 41;
            githubPictureBox.TabStop = false;
            // 
            // mainPageDragControl
            // 
            mainPageDragControl.DockIndicatorTransparencyValue = 0.6D;
            mainPageDragControl.TargetControl = this;
            mainPageDragControl.UseTransparentDrag = true;
            // 
            // statsPanelsParentPanel
            // 
            statsPanelsParentPanel.Controls.Add(todayTotalParentPanel);
            statsPanelsParentPanel.Controls.Add(averageSessionContainerPanel);
            statsPanelsParentPanel.Controls.Add(guna2GradientPanel7);
            statsPanelsParentPanel.CustomizableEdges = customizableEdges41;
            statsPanelsParentPanel.Location = new Point(0, 34);
            statsPanelsParentPanel.Name = "statsPanelsParentPanel";
            statsPanelsParentPanel.ShadowDecoration.Color = Color.FromArgb(44, 45, 65);
            statsPanelsParentPanel.ShadowDecoration.CustomizableEdges = customizableEdges42;
            statsPanelsParentPanel.Size = new Size(1300, 196);
            statsPanelsParentPanel.TabIndex = 44;
            // 
            // averageSessionContainerPanel
            // 
            averageSessionContainerPanel.BackColor = Color.Transparent;
            averageSessionContainerPanel.BorderColor = Color.FromArgb(44, 45, 65);
            averageSessionContainerPanel.BorderRadius = 25;
            averageSessionContainerPanel.Controls.Add(averageSessionPanel);
            averageSessionContainerPanel.CustomizableEdges = customizableEdges37;
            averageSessionContainerPanel.FillColor = Color.FromArgb(44, 45, 65);
            averageSessionContainerPanel.FillColor2 = Color.FromArgb(44, 45, 65);
            averageSessionContainerPanel.ForeColor = Color.FromArgb(44, 45, 65);
            averageSessionContainerPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            averageSessionContainerPanel.Location = new Point(958, 15);
            averageSessionContainerPanel.Name = "averageSessionContainerPanel";
            averageSessionContainerPanel.ShadowDecoration.BorderRadius = 12;
            averageSessionContainerPanel.ShadowDecoration.Color = Color.FromArgb(44, 45, 65);
            averageSessionContainerPanel.ShadowDecoration.CustomizableEdges = customizableEdges38;
            averageSessionContainerPanel.ShadowDecoration.Enabled = true;
            averageSessionContainerPanel.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            averageSessionContainerPanel.Size = new Size(270, 144);
            averageSessionContainerPanel.TabIndex = 45;
            // 
            // guna2GradientPanel7
            // 
            guna2GradientPanel7.BackColor = Color.Transparent;
            guna2GradientPanel7.BorderColor = Color.FromArgb(44, 45, 65);
            guna2GradientPanel7.BorderRadius = 25;
            guna2GradientPanel7.Controls.Add(guna2GradientPanel3);
            guna2GradientPanel7.CustomizableEdges = customizableEdges39;
            guna2GradientPanel7.FillColor = Color.FromArgb(44, 45, 65);
            guna2GradientPanel7.FillColor2 = Color.FromArgb(44, 45, 65);
            guna2GradientPanel7.ForeColor = Color.FromArgb(44, 45, 65);
            guna2GradientPanel7.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            guna2GradientPanel7.Location = new Point(489, 15);
            guna2GradientPanel7.Name = "guna2GradientPanel7";
            guna2GradientPanel7.ShadowDecoration.BorderRadius = 12;
            guna2GradientPanel7.ShadowDecoration.Color = Color.FromArgb(44, 45, 65);
            guna2GradientPanel7.ShadowDecoration.CustomizableEdges = customizableEdges40;
            guna2GradientPanel7.ShadowDecoration.Enabled = true;
            guna2GradientPanel7.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            guna2GradientPanel7.Size = new Size(270, 144);
            guna2GradientPanel7.TabIndex = 44;
            // 
            // iconPictureBox5
            // 
            iconPictureBox5.BackColor = Color.FromArgb(35, 34, 50);
            iconPictureBox5.ForeColor = Color.FromArgb(255, 160, 210);
            iconPictureBox5.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            iconPictureBox5.IconColor = Color.FromArgb(255, 160, 210);
            iconPictureBox5.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox5.IconSize = 34;
            iconPictureBox5.Location = new Point(1173, 0);
            iconPictureBox5.Margin = new Padding(3, 2, 3, 2);
            iconPictureBox5.Name = "iconPictureBox5";
            iconPictureBox5.Size = new Size(35, 34);
            iconPictureBox5.TabIndex = 45;
            iconPictureBox5.TabStop = false;
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(1300, 781);
            Controls.Add(iconPictureBox5);
            Controls.Add(statsPanelsParentPanel);
            Controls.Add(githubPictureBox);
            Controls.Add(topHalfSeperator);
            Controls.Add(guna2HtmlLabel6);
            Controls.Add(bottomHalfParentPanel);
            Controls.Add(minimizeButton);
            Controls.Add(closeButton);
            ForeColor = Color.FromArgb(35, 34, 50);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainPage";
            TodayTotalPanel.ResumeLayout(false);
            guna2GradientPanel3.ResumeLayout(false);
            averageSessionPanel.ResumeLayout(false);
            bottomHalfParentPanel.ResumeLayout(false);
            starRatingPanel.ResumeLayout(false);
            starRatingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)starRatingPictureBox).EndInit();
            guna2GradientPanel10.ResumeLayout(false);
            guna2GradientPanel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mossPictureBoxGif).EndInit();
            Last28DaysPanel.ResumeLayout(false);
            Last28DaysPanel.PerformLayout();
            todayTotalParentPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)githubPictureBox).EndInit();
            statsPanelsParentPanel.ResumeLayout(false);
            averageSessionContainerPanel.ResumeLayout(false);
            guna2GradientPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox5).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ControlBox closeButton;
        private Guna.UI2.WinForms.Guna2ControlBox minimizeButton;
        private Panel panel2;
        private Guna.UI2.WinForms.Guna2GradientPanel TodayTotalPanel;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel3;
        private Guna.UI2.WinForms.Guna2Panel bottomHalfParentPanel;
        private Guna.UI2.WinForms.Guna2AnimateWindow gunaAnimationWindow;
        private Guna.UI2.WinForms.Guna2HtmlLabel WeekTotalLabel;
        private GunaDoughnutDataset doughnutDataset;
        private Guna2GradientPanel zeroPanel;
        private Guna2HtmlLabel zeroLabel;
        private Guna2GradientPanel underOnePanel;
        private Guna2HtmlLabel underOneLabel;
        private Guna2GradientPanel oneToTwoPanel;
        private Guna2HtmlLabel oneToTwoLabel;
        private Guna2GradientPanel betweenTwoAndFourHoursLegendPanel;
        private Guna2HtmlLabel twoToFourLabel;
        private Guna2GradientPanel averageSessionPanel;
        private Guna2HtmlLabel AverageSessionLabel;
        private Guna2GradientPanel Last28DaysPanel;
        private Guna2HtmlLabel todayTotalLabel;
        private Guna2HtmlLabel starRatingsTitleLabel;


        private Guna2HtmlLabel starRatingAverageLabel;
        private Guna2HtmlLabel starRatingTotalLabel;
        private Guna2GradientPanel activityParentPanel;
        private Guna2GradientPanel zeroMinutesLegendPanel;
        private Guna2HtmlLabel guna2HtmlLabel5;
        private Guna2HtmlLabel guna2HtmlLabel4;
        private Guna2GradientPanel lessThanOneHourLegendPanel;
        private Guna2HtmlLabel guna2HtmlLabel3;
        private Guna2HtmlLabel guna2HtmlLabel1;
        private Guna2GradientPanel betweenOneAndTwoHoursLegendPanel;
        private Guna2HtmlLabel recentActivityLabel;
        private Guna2HtmlLabel guna2HtmlLabel6;
        private Guna2HtmlLabel last7DaysLabel;
        private Guna2HtmlLabel guna2HtmlLabel8;
        private Guna2HtmlLabel guna2HtmlLabel7;
        private CustomGradientButton viewSessionButton;
        private CustomGradientButton startSessionButton;
        private Guna2HtmlLabel readyToBeginLabel;
        private Guna2HtmlLabel guna2HtmlLabel9;
        private Guna2HtmlLabel guna2HtmlLabel2;
        private Guna2PictureBox mossPictureBoxGif;
        private Guna2Separator topHalfSeperator;
        private FontAwesome.Sharp.IconPictureBox githubPictureBox;
        private Guna2HtmlLabel guna2HtmlLabel13;
        private Guna2DragControl mainPageDragControl;
        private LiveChartsCore.SkiaSharpView.WinForms.PieChart starRatingsPieChart;
        private Guna2GradientPanel starRatingPanel;
        private Guna2GradientPanel todayTotalParentPanel;
        private Guna2GradientPanel statsPanelsParentPanel;
        private Guna2GradientPanel averageSessionContainerPanel;
        private Guna2GradientPanel guna2GradientPanel7;
        private Guna2GradientPanel guna2GradientPanel10;
        private Guna2AnimateWindow guna2AnimateWindow1;
        private Guna2Separator guna2Separator1;
        private FontAwesome.Sharp.IconPictureBox starRatingPictureBox;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox2;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox4;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox3;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox5;
    }
}