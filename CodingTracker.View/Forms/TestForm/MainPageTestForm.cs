using CodingTracker.View.Forms.Services.SharedFormServices.CustomGradientButtons;
using Guna.Charts.WinForms;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodingTracker.View.Forms.TestForm
{
    public partial class MainPageTestForm : Form
    {
        public MainPageTestForm()
        {
            InitializeComponent();
        }










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
