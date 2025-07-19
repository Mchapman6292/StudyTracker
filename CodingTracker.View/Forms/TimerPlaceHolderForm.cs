using CodingTracker.View.Forms.Services.AnimatedTimerService;
using SkiaSharp;

namespace CodingTracker.View.Forms
{
    public partial class TimerPlaceHolderForm : Form
    {
        public TimerPlaceHolderForm()
        {
            InitializeComponent();
        }

        // This is used only to help visualize the column design and placement using a designer, instead of having to run in debug and adjust locations every time...
        public Dictionary<ColumnUnitType, SKPoint> GetPlaceHolderColumnLocations()
        {
            return new Dictionary<ColumnUnitType, SKPoint>
            {
                { ColumnUnitType.SecondsSingleDigits, new SKPoint(secondsSinglePanel.Location.X, secondsSinglePanel.Location.Y) },
                { ColumnUnitType.SecondsLeadingDigit, new SKPoint(secondsLeadingPanel.Location.X, secondsLeadingPanel.Location.Y) },
                { ColumnUnitType.MinutesSingleDigits, new SKPoint(minutesSinglePanel.Location.X, minutesSinglePanel.Location.Y) },
                { ColumnUnitType.MinutesLeadingDigits, new SKPoint(minutesLeadingPanel.Location.X, minutesLeadingPanel.Location.Y) },
                { ColumnUnitType.HoursSinglesDigits, new SKPoint(hoursSinglePanel.Location.X, hoursSinglePanel.Location.Y) },
                { ColumnUnitType.HoursLeadingDigits, new SKPoint(hoursLeadingPanel.Location.X, hoursLeadingPanel.Location.Y) }
            };
        }
    }
}
