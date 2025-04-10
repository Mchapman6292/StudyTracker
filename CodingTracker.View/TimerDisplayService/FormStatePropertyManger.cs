using CodingTracker.Common.IApplicationLoggers;

namespace CodingTracker.View.TimerDisplayService.FormStatePropertyManagers;

public interface IFormStatePropertyManager
{
    void SetIsFormGoalSet(bool goalSet);
    bool ReturnIsFormGoalSet();
    void SetFormGoalTimeHHMM(int goalTimeHHMM);
    int ReturnFormGoalTimeHHMMAsInt();
    void SetFormGoalMins(int formGoalMins);
    int ReturnFormGoalMins();

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

    private int _formGoalMinutes { get; set; }  


    public void SetIsFormGoalSet(bool goalSet)
    {
        _isFormGoalSet = goalSet;
    }

    public bool ReturnIsFormGoalSet()
    {
        return _isFormGoalSet;
    }

    public void SetFormGoalTimeHHMM(int goalTimeHHMM)
    {
        _formGoalTimeHHMM = goalTimeHHMM;
    }

    public int ReturnFormGoalTimeHHMMAsInt()
    {
        _appLogger.Debug($"FormGoalTimeHHMM value: {_formGoalTimeHHMM}"); 
        return _formGoalTimeHHMM;
    }

    public void SetFormGoalMins(int formGoalMins)
    {
        _formGoalMinutes = formGoalMins;
    }

    public int ReturnFormGoalMins()
    {
        return _formGoalMinutes;
    }

}
