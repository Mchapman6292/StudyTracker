using CodingTracker.Common.IApplicationLoggers;

namespace CodingTracker.View.TimerDisplayService.FormStatePropertyManagers;

public interface IFormStatePropertyManager
{
    void SetFormGoalTimeHHMM(int goalTimeHHMM);
    int ReturnFormGoalTimeHHMMAsInt();
    /*
    void SetIsFormGoalSet(bool goalSet);
    bool ReturnIsFormGoalSet();
    void SetFormGoalSeconds(int formGoalMins);
    int ReturnFormGoalSeconds();
    */

}

public class FormStatePropertyManger : IFormStatePropertyManager
{
    private readonly IApplicationLogger _appLogger;

    public FormStatePropertyManger(IApplicationLogger applicationLogger)
    {
        _appLogger = applicationLogger;
    }

    private int _formGoalTimeHHMM { get; set; }
    private bool _isFormGoalSet { get; set; }

    private int _formGoalSeconds { get; set; }


    public void SetFormGoalTimeHHMM(int goalTimeHHMM)
    {
        _formGoalTimeHHMM = goalTimeHHMM;
        _appLogger.Debug($"FormGoalTime set to {goalTimeHHMM}.");
    }

    public int ReturnFormGoalTimeHHMMAsInt()
    {
        _appLogger.Debug($"FormGoalTimeHHMM value: {_formGoalTimeHHMM}");
        return _formGoalTimeHHMM;
    }

    public void SetIsFormGoalSet(bool goalSet)
    {
        _isFormGoalSet = goalSet;
    }

    /*


    public bool ReturnIsFormGoalSet()
    {
        return _isFormGoalSet;
    }


    public void SetFormGoalSeconds(int formGoalSeconds)
    {
        _formGoalSeconds = formGoalSeconds;
    }

    public int ReturnFormGoalSeconds()
    {
        return _formGoalSeconds;
    }
    */
}
