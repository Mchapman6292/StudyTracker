using Guna.UI2.WinForms;

namespace CodingTracker.View.Forms.Services.MainPageService
{
    public enum SessionDurationEnum
    {
        Zero,
        UnderOneHour,     // 0-59 minutes
        OneToTwoHours,    // 1-1.99 hours  
        TwoToFourHours,   // 2-3.99 hours
        FourHoursPlus     // 4+ hours
    }



    public interface ILast28DayPanelSettings
    {
        Dictionary<SessionDurationEnum, Guna2GradientPanel> ReturnDurationPanelSettingsDict();
        SessionDurationEnum ConvertDurationSecondsToSessionDurationEnum(int durationSeconds);
    }

    public class Last28Panelsettings : ILast28DayPanelSettings
    {
        public Dictionary<SessionDurationEnum, Guna2GradientPanel> DurationPanelSettings;



        public Last28Panelsettings()
        {
            DurationPanelSettings = new Dictionary<SessionDurationEnum, Guna2GradientPanel>
            {
                [SessionDurationEnum.Zero] = CreateZeroDurationPanel(),
                [SessionDurationEnum.UnderOneHour] = CreateUnderOneHourPanel(),
                [SessionDurationEnum.OneToTwoHours] = CreateUnderTwoHoursPanel(),
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
                FillColor = Color.FromArgb(20, 60, 80),
                FillColor2 = Color.FromArgb(40, 100, 120),
                Size = new Size(23, 15)
            };
        }

        private Guna2GradientPanel CreateUnderOneHourPanel()
        {
            return new Guna2GradientPanel
            {
                BackColor = Color.Transparent,
                BorderRadius = 4,
                FillColor = Color.FromArgb(40, 140, 160),
                FillColor2 = Color.FromArgb(80, 200, 220),
                Size = new Size(23, 15)
            };
        }

        private Guna2GradientPanel CreateUnderTwoHoursPanel()
        {
            var panel = new Guna2GradientPanel
            {
                BackColor = Color.Transparent,
                BorderRadius = 4,
                FillColor = Color.FromArgb(80, 160, 200),
                FillColor2 = Color.FromArgb(140, 120, 220),
                Size = new Size(23, 15)
            };
            panel.ShadowDecoration.Enabled = true;
            panel.ShadowDecoration.Color = Color.FromArgb(140, 140, 255);
            panel.ShadowDecoration.Shadow = new Padding(1);
            return panel;
        }

        private Guna2GradientPanel CreateTwoToFourHoursPanel()
        {
            var panel = new Guna2GradientPanel
            {
                BackColor = Color.Transparent,
                BorderRadius = 4,
                FillColor = Color.FromArgb(180, 100, 200),
                FillColor2 = Color.FromArgb(255, 120, 180),
                BorderColor = Color.FromArgb(255, 140, 200),
                BorderThickness = 1,
                Size = new Size(23, 15)
            };
            panel.ShadowDecoration.Enabled = true;
            panel.ShadowDecoration.Color = Color.FromArgb(255, 120, 200);
            panel.ShadowDecoration.Shadow = new Padding(2);
            return panel;
        }

        private Guna2GradientPanel CreateFourHoursPlusPanel()
        {
            var panel = new Guna2GradientPanel
            {
                BackColor = Color.Transparent,
                BorderColor = Color.FromArgb(255, 200, 255),
                BorderRadius = 4,
                BorderThickness = 1,
                FillColor = Color.FromArgb(255, 100, 180),
                FillColor2 = Color.FromArgb(255, 200, 240),
                Size = new Size(23, 15)
            };
            panel.ShadowDecoration.Enabled = true;
            panel.ShadowDecoration.Color = Color.FromArgb(255, 180, 255);
            panel.ShadowDecoration.Shadow = new Padding(3);
            return panel;
        }

        public SessionDurationEnum ConvertDurationSecondsToSessionDurationEnum(int durationSeconds)
        {
            if (durationSeconds <= 0)
            {
                return SessionDurationEnum.Zero;
            }
            if (durationSeconds > 0 && durationSeconds < 3600)
            {
                return SessionDurationEnum.UnderOneHour;
            }
            if (durationSeconds >= 3600 && durationSeconds < 7200)
            {
                return SessionDurationEnum.OneToTwoHours;
            }
            if (durationSeconds >= 7200 && durationSeconds < 14400)
            {
                return SessionDurationEnum.TwoToFourHours;
            }
            else
            {
                return SessionDurationEnum.FourHoursPlus;
            }


        }
    }
}