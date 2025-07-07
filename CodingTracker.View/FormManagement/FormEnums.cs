using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.FormManagement
{
    public enum FormPageEnum
    {
        LoginPage,
        MainPage,
        EditSessionForm,
        CreateAccountForm,
        SessionGoalForm,
        CountdownTimerForm,
        ElapsedTimerForm,
        ConfirmUsernameForm,
        ResetPasswordForm,
        OrbitalTimerForm,
        WaveVisualizationForm,
        TestForm,
        SessionNotesForm,
        SessionRatingForm
    }



    public enum ColumnAnimationSetting
    {
        IsMovingUp,
        IsMovingDown,
    }


    public enum TimerAnimationType
    {
        CircleAnimation,
        ColumnScroll
    }

}



