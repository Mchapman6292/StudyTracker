using CodingTracker.Common.DataInterfaces.Repositories;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;


namespace CodingTracker.View.Forms.Services.MainPageService.DonutChartManagers
{
    public interface IMainPagePieChartManager
    {
        ISeries[] ReturnPieChartISeries();
        int ReturnPieChartInitialRotation();
        void SetPieChartSettings(LiveChartsCore.SkiaSharpView.WinForms.PieChart starRatingsPieChart);
    }


    public class MainPagePieChartManager : IMainPagePieChartManager
    {
        private readonly ICodingSessionRepository _codingSessionRepository;


        private SKPoint start = new SKPoint(0.5f, 0);
        private SKPoint end = new SKPoint(0.5f, 1);

        private int radiusInner = 35;
        private int pushOutValue = 2;

        /// This sets the startting point of the segments to 12 o'clock https://livecharts.dev/docs/winui/2.0.0-rc1/PieChart.Pie%20chart%20control
        private int PieChartInitialRotation = 270;





        public ISeries[] Series { get; set; }


        public MainPagePieChartManager(ICodingSessionRepository codingSessionRepository)
        {
            _codingSessionRepository = codingSessionRepository;

        }


        private void PopulateSeries(PieSeries<double> oneStarSeries, PieSeries<double> twoStarSeries, PieSeries<double> threeStarSeries, PieSeries<double> fourStarSeries, PieSeries<double> fiveStarSeries)
        {
            Series = new ISeries[]
            {
                oneStarSeries, twoStarSeries, threeStarSeries, fourStarSeries, fiveStarSeries
            };
        }


        public ISeries[] ReturnPieChartISeries()
        {
            if(Series == null || !Series.Any())
            {
                throw new InvalidOperationException($"MainPagePieChartManager series is null, must be initialized & populated before it is returned.");
            }

            return Series;
        }

        public int ReturnPieChartInitialRotation()
        {
            return PieChartInitialRotation;
        }

        public void SetPieChartSettings(LiveChartsCore.SkiaSharpView.WinForms.PieChart starRatingsPieChart)
        {
            starRatingsPieChart.InitialRotation = 270;

        }

    

        private async Task InitializeISeriesAsync()
        {
            Dictionary<int, int> sessionStarRatings = await _codingSessionRepository.GetStarRatingAndCount();

            int oneStarCount = sessionStarRatings.GetValueOrDefault(1);
            int twoStarCount = sessionStarRatings.GetValueOrDefault(2);
            int threeStarCount = sessionStarRatings.GetValueOrDefault(3);
            int fourStarCount = sessionStarRatings.GetValueOrDefault(4);
            int fiveStarCount = sessionStarRatings.GetValueOrDefault(5);



            SKPoint start = new SKPoint(0.5f, 0);
            SKPoint end = new SKPoint(0.5f, 1);


            SKColor oneStarColor = new SKColor(40, 100, 120);
            SKColor oneStarColor2 = new SKColor(80, 200, 220);
            SKColor[] colorsOneStar = { oneStarColor, oneStarColor2 };

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


            PieSeries<double> oneStarSeries = new PieSeries<double>
            {
                Values = new List<double> { oneStarCount },
                Name = $"One Star",
                InnerRadius = radiusInner,
                Fill = new LinearGradientPaint(colorsOneStar, start, end, [0.5f, 0.1f], SKShaderTileMode.Repeat),
                Pushout = pushOutValue
            };


            var twoStarSeries = new PieSeries<double>
            {
                Values = new List<double> { twoStarCount },
                Name = $"Two star",
                InnerRadius = radiusInner,
                Fill = new LinearGradientPaint(colorsTwoStar, start, end, [0.5f, 0.1f], SKShaderTileMode.Repeat),
                Pushout = pushOutValue,
                Stroke = strokeTwoStar,
            };

            var threeStarSeries = new PieSeries<double>
            {
                Values = new List<double> { threeStarCount },
                Name = "Three star",
                InnerRadius = radiusInner,
                Fill = new LinearGradientPaint(coloursThreeStar, start, end, [0.5f, 0.1f], SKShaderTileMode.Repeat),
                Pushout = pushOutValue,
                Stroke = strokeThreeStar

            };

            var fourStarSeries = new PieSeries<double>
            {
                Values = new List<double> { fourStarCount },
                Name = "Four Star",
                InnerRadius = radiusInner,
                Fill = new LinearGradientPaint(colorsFourStar, start, end, [0.5f, 0.1f], SKShaderTileMode.Repeat),
                Pushout = pushOutValue

            };

            var fiveStarSeries = new PieSeries<double>
            {
                Values = new List<double> { fiveStarCount },
                Name = "Five Star",
                InnerRadius = radiusInner,
                Fill = new LinearGradientPaint(colorsFiveStar, start, end, [0.5f, 0.1f], SKShaderTileMode.Repeat),
                Pushout = pushOutValue
            };

        }

    }






    }

