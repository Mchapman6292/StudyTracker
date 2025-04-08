namespace CodingTracker.View.TimerDisplayService.FormStatePropertyManagers;

public interface IFormStatePropertyManager
{
    void SetIsFormGoalSet(bool goalSet);
    bool ReturnIsFormGoalSet();
    void SetFormGoalTimeHHMM(int goalTimeHHMM);
    int ReturnFormGoalTimeHHMMAsInt();
}

public class FormStatePropertyManger : IFormStatePropertyManager
{
    private int _formGoalTimeHHMM { get; set; }
    private bool _isFormGoalSet { get; set; }


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
        return _formGoalTimeHHMM;
    }

}
