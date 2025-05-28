using CodingTracker.View.Forms.Services.SharedFormServices;
using System.Drawing.Drawing2D;
using CodingTracker.View.Forms.Services.MainPageService.DoughnutSegments;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         

namespace CodingTracker.View.Forms
{
    public partial class TestForm : Form
    {
        private System.Windows.Forms.Timer progressTimer;
        private DateTime startTime;
        private bool isPaused = false;
        private TimeSpan pausedElapsedTime;
        private double timerDurationSeconds = 60;
        private readonly IButtonHighlighterService _buttonHighligherService;
        private readonly INotificationManager _notificationManager;

        // Doughnut chart data
        private List<DoughnutSegment> chartSegments;
        private Point chartCenter;
        private int outerRadius = 120;
        private int innerRadius = 60;

        // Animation properties
        private int hoveredSegmentIndex = -1;
        private float hoverAnimationProgress = 0f;
        private System.Windows.Forms.Timer hoverAnimationTimer;
        private bool isHoverAnimating = false;

        public TestForm(IButtonHighlighterService buttonHighlighterService, INotificationManager notificationManager)
        {
            InitializeComponent();
            _buttonHighligherService = buttonHighlighterService;
            _notificationManager = notificationManager;
            this.Load += TestForm_Load;

            // Set up chart data
            InitializeChartData();

            // Enable double buffering for smooth drawing
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            // Subscribe to main panel paint event
            mainPanel.Paint += MainPanel_Paint;

            // Set up mouse events for hover detection
            mainPanel.MouseMove += MainPanel_MouseMove;
            mainPanel.MouseLeave += MainPanel_MouseLeave;

            // Set up hover animation timer
            hoverAnimationTimer = new System.Windows.Forms.Timer();
            hoverAnimationTimer.Interval = 16; // ~60 FPS
            hoverAnimationTimer.Tick += HoverAnimationTimer_Tick;
        }

        private void InitializeChartData()
        {
            chartSegments = new List<DoughnutSegment>
            {
                new DoughnutSegment { StartAngle = 0f, SweepAngle = 126f, Color1 = Color.FromArgb(100, 220, 220), Color2 = Color.FromArgb(64, 224, 208), Label = "Coding", Value = 35 },
                new DoughnutSegment { StartAngle = 126f, SweepAngle = 90f, Color1 = Color.FromArgb(255, 105, 180), Color2 = Color.FromArgb(255, 81, 195), Label = "Debugging", Value = 25 },
                new DoughnutSegment { StartAngle = 216f, SweepAngle = 72f, Color1 = Color.FromArgb(138, 43, 226), Color2 = Color.FromArgb(147, 112, 219), Label = "Research", Value = 20 },
                new DoughnutSegment { StartAngle = 288f, SweepAngle = 54f, Color1 = Color.FromArgb(123, 104, 238), Color2 = Color.FromArgb(72, 209, 204), Label = "Testing", Value = 15 },
                new DoughnutSegment { StartAngle = 342f, SweepAngle = 18f, Color1 = Color.FromArgb(255, 215, 0), Color2 = Color.FromArgb(255, 140, 0), Label = "Meetings", Value = 5 }
            };

            // Calculate chart center based on form size
            chartCenter = new Point(650, 360); // Center of 1300x720 form
        }

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.CompositingQuality = CompositingQuality.HighQuality;

            DrawDoughnutChart(g);
        }

        private void DrawDoughnutChart(Graphics g)
        {
            foreach (var segment in chartSegments)
            {
                DrawDoughnutSegment(g, segment);
            }

            // Draw center circle to create the "hole"
            DrawCenterHole(g);

            // Optional: Draw labels
            DrawSegmentLabels(g);
        }

        private void DrawDoughnutSegment(Graphics g, DoughnutSegment segment)
        {
            int segmentIndex = chartSegments.IndexOf(segment);
            bool isHovered = segmentIndex == hoveredSegmentIndex;

            // Calculate animation effects
            float scaleMultiplier = 1f;
            float glowIntensity = 1f;

            if (isHovered && hoverAnimationProgress > 0)
            {
                scaleMultiplier = 1f + (hoverAnimationProgress * 0.1f); // 10% scale increase
                glowIntensity = 1f + (hoverAnimationProgress * 1.5f); // Stronger glow
            }

            // Calculate animated radius
            int animatedOuterRadius = (int)(outerRadius * scaleMultiplier);
            int animatedInnerRadius = innerRadius; // Keep inner radius constant

            // Create gradient brush for the segment
            using (var brush = CreateSegmentBrush(segment, scaleMultiplier))
            using (var pen = new Pen(brush, animatedOuterRadius - animatedInnerRadius))
            {
                // Calculate the radius for drawing (midpoint between inner and outer)
                int drawRadius = (animatedOuterRadius + animatedInnerRadius) / 2;

                // Create rectangle for the arc
                Rectangle arcRect = new Rectangle(
                    chartCenter.X - drawRadius,
                    chartCenter.Y - drawRadius,
                    drawRadius * 2,
                    drawRadius * 2
                );

                // Draw the arc segment
                g.DrawArc(pen, arcRect, segment.StartAngle, segment.SweepAngle);
            }

            // Add enhanced glow effect for hovered segments
            if (isHovered)
            {
                DrawEnhancedSegmentGlow(g, segment, glowIntensity, scaleMultiplier);
            }
            else
            {
                DrawSegmentGlow(g, segment);
            }
        }

        private LinearGradientBrush CreateSegmentBrush(DoughnutSegment segment, float scaleMultiplier = 1f)
        {
            // Calculate gradient direction based on segment angle
            float midAngle = segment.StartAngle + (segment.SweepAngle / 2);
            double radians = midAngle * Math.PI / 180;

            Point startPoint = new Point(
                chartCenter.X + (int)(Math.Cos(radians) * innerRadius),
                chartCenter.Y + (int)(Math.Sin(radians) * innerRadius)
            );

            Point endPoint = new Point(
                chartCenter.X + (int)(Math.Cos(radians) * outerRadius * scaleMultiplier),
                chartCenter.Y + (int)(Math.Sin(radians) * outerRadius * scaleMultiplier)
            );

            return new LinearGradientBrush(startPoint, endPoint, segment.Color1, segment.Color2);
        }

        private void DrawSegmentGlow(Graphics g, DoughnutSegment segment)
        {
            // Create a subtle glow effect
            using (var glowBrush = CreateSegmentBrush(segment))
            using (var glowPen = new Pen(Color.FromArgb(30, segment.Color1), (outerRadius - innerRadius) + 10))
            {
                int glowRadius = (outerRadius + innerRadius) / 2;
                Rectangle glowRect = new Rectangle(
                    chartCenter.X - glowRadius,
                    chartCenter.Y - glowRadius,
                    glowRadius * 2,
                    glowRadius * 2
                );

                g.DrawArc(glowPen, glowRect, segment.StartAngle, segment.SweepAngle);
            }
        }

        private void DrawCenterHole(Graphics g)
        {
            // Draw the center circle with the same color as the background
            using (var centerBrush = new SolidBrush(Color.FromArgb(35, 34, 50)))
            {
                Rectangle centerRect = new Rectangle(
                    chartCenter.X - innerRadius,
                    chartCenter.Y - innerRadius,
                    innerRadius * 2,
                    innerRadius * 2
                );

                g.FillEllipse(centerBrush, centerRect);
            }

            // Add a subtle border to the center hole
            using (var borderPen = new Pen(Color.FromArgb(50, 255, 255, 255), 2))
            {
                Rectangle borderRect = new Rectangle(
                    chartCenter.X - innerRadius,
                    chartCenter.Y - innerRadius,
                    innerRadius * 2,
                    innerRadius * 2
                );

                g.DrawEllipse(borderPen, borderRect);
            }
        }

        private void DrawSegmentLabels(Graphics g)
        {
            using (var font = new Font("Segoe UI", 10, FontStyle.Regular))
            using (var textBrush = new SolidBrush(Color.White))
            {
                foreach (var segment in chartSegments)
                {
                    // Calculate label position
                    float midAngle = segment.StartAngle + (segment.SweepAngle / 2);
                    double radians = midAngle * Math.PI / 180;

                    int labelRadius = outerRadius + 30; // Position outside the chart
                    Point labelPos = new Point(
                        chartCenter.X + (int)(Math.Cos(radians) * labelRadius),
                        chartCenter.Y + (int)(Math.Sin(radians) * labelRadius)
                    );

                    // Create label text
                    string labelText = $"{segment.Label}\n{segment.Value}%";

                    // Measure text for centering
                    SizeF textSize = g.MeasureString(labelText, font);
                    Point adjustedPos = new Point(
                        labelPos.X - (int)(textSize.Width / 2),
                        labelPos.Y - (int)(textSize.Height / 2)
                    );

                    // Draw label
                    g.DrawString(labelText, font, textBrush, adjustedPos);
                }
            }
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            // Force a repaint to show the chart
            mainPanel.Invalidate();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            // Example: Update chart data and refresh
            chartSegments[0].Value = new Random().Next(10, 40);
            chartSegments[0].SweepAngle = chartSegments[0].Value * 3.6f; // Convert percentage to degrees
            mainPanel.Invalidate();
        }

        private void TestButton_MouseEnter(object sender, EventArgs e)
        {
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
        }

        // Animation and hover detection methods
        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            int newHoveredIndex = GetSegmentAtPoint(e.Location);

            if (newHoveredIndex != hoveredSegmentIndex)
            {
                hoveredSegmentIndex = newHoveredIndex;
                StartHoverAnimation();
            }
        }

        private void MainPanel_MouseLeave(object sender, EventArgs e)
        {
            hoveredSegmentIndex = -1;
            StartHoverAnimation();
        }

        private int GetSegmentAtPoint(Point mousePos)
        {
            // Calculate distance from center
            double deltaX = mousePos.X - chartCenter.X;
            double deltaY = mousePos.Y - chartCenter.Y;
            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            // Check if mouse is within the doughnut area
            if (distance < innerRadius || distance > outerRadius + 20) // +20 for hover tolerance
                return -1;

            // Calculate angle
            double angle = Math.Atan2(deltaY, deltaX) * 180 / Math.PI;
            if (angle < 0) angle += 360; // Normalize to 0-360

            // Find which segment contains this angle
            for (int i = 0; i < chartSegments.Count; i++)
            {
                var segment = chartSegments[i];
                float endAngle = segment.StartAngle + segment.SweepAngle;

                if (angle >= segment.StartAngle && angle <= endAngle)
                    return i;

                // Handle wrap-around case (360° -> 0°)
                if (segment.StartAngle > endAngle) // Crosses 0°
                {
                    if (angle >= segment.StartAngle || angle <= endAngle)
                        return i;
                }
            }

            return -1;
        }

        private void StartHoverAnimation()
        {
            if (!isHoverAnimating)
            {
                isHoverAnimating = true;
                hoverAnimationTimer.Start();
            }
        }

        private void HoverAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (hoveredSegmentIndex >= 0)
            {
                // Animate in
                hoverAnimationProgress = Math.Min(1f, hoverAnimationProgress + 0.1f);
            }
            else
            {
                // Animate out
                hoverAnimationProgress = Math.Max(0f, hoverAnimationProgress - 0.1f);
            }

            // Stop animation when complete
            if ((hoveredSegmentIndex >= 0 && hoverAnimationProgress >= 1f) ||
                (hoveredSegmentIndex < 0 && hoverAnimationProgress <= 0f))
            {
                isHoverAnimating = false;
                hoverAnimationTimer.Stop();
            }

            // Trigger repaint
            mainPanel.Invalidate();
        }

        private void DrawEnhancedSegmentGlow(Graphics g, DoughnutSegment segment, float intensity, float scaleMultiplier)
        {
            // Create multiple glow layers for enhanced effect
            for (int i = 3; i >= 1; i--)
            {
                using (var glowBrush = CreateSegmentBrush(segment, scaleMultiplier))
                {
                    int alpha = (int)(20 * intensity / i); // Varying alpha for layers
                    using (var glowPen = new Pen(Color.FromArgb(alpha, segment.Color1), (outerRadius - innerRadius) + (i * 8)))
                    {
                        int glowRadius = (int)((outerRadius + innerRadius) / 2 * scaleMultiplier);
                        Rectangle glowRect = new Rectangle(
                            chartCenter.X - glowRadius,
                            chartCenter.Y - glowRadius,
                            glowRadius * 2,
                            glowRadius * 2
                        );

                        g.DrawArc(glowPen, glowRect, segment.StartAngle, segment.SweepAngle);
                    }
                }
            }
        }



    }
}