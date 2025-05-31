using Guna.UI2.WinForms;

namespace CodingTracker.View.Forms.Services.MainPageService
{
    public enum SessionDurationEnum
    {
        Zero,
        UnderOneHour,
        UnderTwoHours,
        TwoToFourHours,
        FourHoursPlus
    }

    public interface ILast28DaysPanelSetting
    {
        Dictionary<SessionDurationEnum, Guna2GradientPanel> ReturnDurationPanelSettingsDict();
    }

    public class Last28DaysPanelSetting
    {
        public Dictionary<SessionDurationEnum, Guna2GradientPanel> DurationPanelSettings;

        public Last28DaysPanelSetting()
        {
            DurationPanelSettings = new Dictionary<SessionDurationEnum, Guna2GradientPanel>
            {
                [SessionDurationEnum.Zero] = CreateZeroDurationPanel(),
                [SessionDurationEnum.UnderOneHour] = CreateUnderOneHourPanel(),
                [SessionDurationEnum.UnderTwoHours] = CreateUnderTwoHoursPanel(),
                [SessionDurationEnum.TwoToFourHours] = CreateTwoToFourHoursPanel(),
                [SessionDurationEnum.FourHoursPlus] = CreateFourHoursPlusPanel()
            };
        }

        public Dictionary<SessionDurationEnum, Guna2GradientPanel> ReturnDurationPanelSettingsDict()
        {
            return DurationPanelSettings;
        }

        private Guna2GradientPanel CreateZeroDurationPanel()
        {
            return new Guna2GradientPanel
            {
                BackColor = Color.Transparent,
                BorderRadius = 4,
                FillColor = Color.FromArgb(50, 25, 50),
                FillColor2 = Color.FromArgb(50, 50, 80),
                Size = new Size(23, 15)
            };
        }

        private Guna2GradientPanel CreateUnderOneHourPanel()
        {
            return new Guna2GradientPanel
            {
                BackColor = Color.Transparent,
                BorderRadius = 4,
                FillColor = Color.FromArgb(120, 60, 120),
                FillColor2 = Color.FromArgb(100, 120, 160),
                Size = new Size(23, 15)
            };
        }

        private Guna2GradientPanel CreateUnderTwoHoursPanel()
        {
            var panel = new Guna2GradientPanel
            {
                BackColor = Color.Transparent,
                BorderRadius = 4,
                FillColor = Color.FromArgb(180, 90, 160),
                FillColor2 = Color.FromArgb(120, 180, 200),
                Size = new Size(23, 15)
            };
            panel.ShadowDecoration.Enabled = true;
            panel.ShadowDecoration.Color = Color.FromArgb(255, 100, 220);
            panel.ShadowDecoration.Shadow = new Padding(1);
            return panel;
        }

        private Guna2GradientPanel CreateTwoToFourHoursPanel()
        {
            var panel = new Guna2GradientPanel
            {
                BackColor = Color.Transparent,
                BorderRadius = 4,
                FillColor = Color.FromArgb(220, 120, 180),
                FillColor2 = Color.FromArgb(140, 200, 230),
                BorderColor = Color.FromArgb(168, 228, 255),
                BorderThickness = 1,
                Size = new Size(23, 15)
            };
            panel.ShadowDecoration.Enabled = true;
            panel.ShadowDecoration.Color = Color.FromArgb(168, 228, 255);
            panel.ShadowDecoration.Shadow = new Padding(2);
            return panel;
        }

        private Guna2GradientPanel CreateFourHoursPlusPanel()
        {
            var panel = new Guna2GradientPanel
            {
                BackColor = Color.Transparent,
                BorderColor = Color.White,
                BorderRadius = 4,
                BorderThickness = 1,
                FillColor = Color.FromArgb(255, 100, 220),
                FillColor2 = Color.FromArgb(200, 250, 255),
                Size = new Size(23, 15)
            };
            panel.ShadowDecoration.Enabled = true;
            panel.ShadowDecoration.Color = Color.White;
            panel.ShadowDecoration.Shadow = new Padding(3);
            return panel;
        }
    }
}