using CodingTracker.Business.MainPageService.PanelColourAssigners;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using CodingTracker.View.Forms.Services.MainPageService;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Factories;
using CodingTracker.View.Forms.Services.MainPageService.RecentActivityService.Panels;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.Controller.SessionVisualizationControllers;
using CodingTracker.View.Forms.Services.MainPageService.SessionVisualizationService.PanelHelpers;
using CodingTracker.View.Forms.Services.SharedFormServices;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using SkiaSharp.Views.Desktop;


namespace CodingTracker.View.Forms
{
    public partial class TestForm : Form
    {
        private readonly IButtonHighlighterService _buttonHighligherService;
        private readonly INotificationManager _notificationManager;
        private readonly IDurationPanelFactory _durationPanelFactory;
        private readonly IDurationParentPanelFactory _durationParentPanelFactory;
        private readonly ISessionContainerPanelFactory _sessionContainerPanelFactory;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly ISessionVisualizationController _sessionVisualizationController;
        private readonly IPanelColourAssigner _panelColorAssigner;
        private readonly ILast28DayPanelSettings _last28DayPanelSettings;
        private readonly ILabelAssignment _labelAssignment;
        private readonly IDurationParentPanelPositionManager _durationPanelPositionManager;
        private readonly IAnimatedTimerRenderer _animatedTimerRenderer;







   
        private async void TestForm_Load(object sender, EventArgs e)
        {
            await PopulatestarRatingDoughnutChart();

        }

        private async void TestForm_Shwon(object sender, EventArgs e)
        {




        }


        private async Task InitializeTestPanelAsync()
        {
            List<DurationParentPanel> dppList = await _sessionVisualizationController.CreateDurationParentPanelsWithDataAsync(testAcitivtyControllerPanel);

            if (dppList.Count > 0)
            {
                foreach (var dpp in dppList)
                {
                    _sessionVisualizationController.LogDurationParentPanel(dpp);
                    testAcitivtyControllerPanel.Controls.Add(dpp);


                }
            }
            _durationPanelPositionManager.SetPanelPositionsAfterContainerAdd(dppList);


        }



        private void LogControlZOrder(Control container, string containerName)
        {
            var message = $"{containerName} contains {container.Controls.Count} controls:\n";

            for (int i = 0; i < container.Controls.Count; i++)
            {
                var control = container.Controls[i];
                message += $"  [{i}] {control.GetType().Name} - {control.Name}\n";
            }

            _notificationManager.ShowNotificationDialog(this, message);
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            CheckSessionInParent();

        }

        private void CheckSessionInParent()
        {
            int sessionTrue = 0;
            var dppList = testAcitivtyControllerPanel.Controls.OfType<DurationParentPanel>().ToList();

            foreach (var dpp in dppList)
            {
                bool inPanel = dpp.SessionContainerPanel.ReturnIsInDurationParentPanelControls();

                dpp.BackColor = Color.Green;
                dpp.ForeColor = Color.Green;

                if (inPanel)
                {
                    sessionTrue++;
                }
            }
            _notificationManager.ShowNotificationDialog(this, $"SessionContainers in ParentPanels: {sessionTrue}");
        }



        private async Task TestPopulateChart()
        {
            Dictionary<int, int> sessionStarRatings = await _codingSessionRepository.GetStarRatingAndCount();

        }



        private SKColor[] InitializeDoughnutColours()
        {
            return new SKColor[] { new SKColor(0, 0, 0), new SKColor(255, 255, 255) };
        }

        private async Task PopulatestarRatingDoughnutChart()
        {
            Dictionary<int, int> sessionStarRatings = await _codingSessionRepository.GetStarRatingAndCount();
            SKColor[] colorsOneStar = InitializeDoughnutColours();

            SKPoint start = new SKPoint(0.5f, 0);
            SKPoint end = new SKPoint(0.5f, 1);

            SKColor twoStarColor = new SKColor(85, 170, 255);
            SKColor twoStarColor2 = new SKColor(255, 255, 86);
            SolidColorPaint strokeTwoStar = new SolidColorPaint(twoStarColor);

            SKColor[] colorsTwoStar = new SKColor[] { twoStarColor, twoStarColor2 };


            SKColor threeStarColor = new SKColor(80, 160, 200);
            SKColor threeStarColor2 = new SKColor(140, 120, 220);
            SolidColorPaint strokeThreeStar = new SolidColorPaint(threeStarColor);

            SKColor[] coloursThreeStar = [threeStarColor, threeStarColor2];

            SKColor fourStarColor = new SKColor(180, 100, 200);
            SKColor fourStarColor2 = new SKColor(255, 120, 180);
            SKColor[] colorsFourStar = [fourStarColor, fourStarColor2];

            SKColor fiveStarColor = new SKColor(120, 220, 160);
            SKColor fiveStarColor2 = new SKColor(100, 180, 255);
            SKColor[] colorsFiveStar = new SKColor[] { fiveStarColor, fiveStarColor2 };


            int radiusInner = 35;
            int pushOutValue = 2;




            int oneStarCount = 3;
            int twoStarCount = sessionStarRatings.GetValueOrDefault(2);
            int threeStarCount = sessionStarRatings.GetValueOrDefault(3);
            int fourStarCount = sessionStarRatings.GetValueOrDefault(4);
            int fiveStarCount = sessionStarRatings.GetValueOrDefault(5);


            List<ISeries> pieSeriesList = new List<ISeries>();



            var oneStarSeries = new PieSeries<double>
            {
                Values = new List<double> { oneStarCount },
                Name = $"★",
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsSize = 16,
                DataLabelsPosition = PolarLabelsPosition.Middle,
                InnerRadius = radiusInner,
                Fill = new RadialGradientPaint(colorsOneStar, null, 1f),
                Pushout = pushOutValue



            };

            var twoStarSeries = new PieSeries<double>
            {
                Values = new List<double> { twoStarCount },
                Name = $"★★",
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsSize = 16,
                DataLabelsPosition = PolarLabelsPosition.Middle,
                InnerRadius = radiusInner,
                /*
                Fill = new RadialGradientPaint(colorsTwoStar, null, 1f),
                */
                Fill = new LinearGradientPaint(colorsTwoStar, start, end, [0.5f, 0.1f], SKShaderTileMode.Repeat),
                Pushout = pushOutValue,
                Stroke = strokeTwoStar

            };

            var threeStarSeries = new PieSeries<double>
            {
                Values = new List<double> { threeStarCount },
                Name = "★★★",
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsSize = 16,
                DataLabelsPosition = PolarLabelsPosition.Middle,
                InnerRadius = radiusInner,
                Fill = new LinearGradientPaint(coloursThreeStar, start, end, [0.5f, 0.1f], SKShaderTileMode.Repeat),
                Pushout = pushOutValue,
                Stroke = strokeThreeStar

            };

            var fourStarSeries = new PieSeries<double>
            {
                Values = new List<double> { fourStarCount },
                Name = "★★★★",
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsSize = 16,
                DataLabelsPosition = PolarLabelsPosition.Middle,
                InnerRadius = radiusInner,
                Fill = new LinearGradientPaint(colorsFourStar, start, end, [0.5f, 0.1f], SKShaderTileMode.Repeat),
                Pushout = pushOutValue

            };

            var fiveStarSeries = new PieSeries<double>
            {
                Values = new List<double> { fiveStarCount },
                Name = "★★★★★",
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsSize = 16,
                DataLabelsPosition = PolarLabelsPosition.Middle,
                InnerRadius = radiusInner,
                Fill = new LinearGradientPaint(colorsFiveStar, start, end, [0.5f, 0.1f], SKShaderTileMode.Repeat),
                Pushout = pushOutValue
            };

            pieSeriesList.AddRange(new[] { oneStarSeries, twoStarSeries, threeStarSeries, fourStarSeries, fiveStarSeries });


            starRatingsPieChart.Series = pieSeriesList;

        }



        private void PaintRectangle(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Black);

            var segments = new List<AnimatedTimerSegment>();
            for (int i = 0; i <= 9; i++)
            {
                segments.Add(new AnimatedTimerSegment(i));
            }

            var column = new AnimatedTimerColumn(segments, new SKPoint(50, 50));
            _animatedTimerRenderer.DrawAllSegments(column, canvas);
        }
        private void newTestButton_Click(object sender, EventArgs e)
        {
                
        }
    }

}