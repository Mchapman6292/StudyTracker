using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.FormManagement;

public interface IFormManager
{
    void HandleAndShowForm<TForm>(Func<TForm> createForm, string methodName, bool closeCurrent = true) where TForm : Form;
    void HideCurrentForms();
    void DisplayForm<TForm>(TForm newForm) where TForm : Form;
}
public class FormManager : IFormManager
{
    private readonly IApplicationLogger _appLogger;
    private List<Form> currentForms = new List<Form>();
    private readonly IFormFactory _formFactory;

    public FormManager(IApplicationLogger appLogger, IFormFactory formFactory)
    {
        _appLogger = appLogger;
        _formFactory = formFactory;
    }

    public void HandleAndShowForm<TForm>(Func<TForm> createForm, string methodName, bool closeCurrent = true) where TForm : Form // Handles the logic for closing forms & implementing error handling logic via ExecutePageAction
    {
        var newForm = createForm();
        if (closeCurrent)
        {
            HideCurrentForms();
        }
        DisplayForm(newForm);
    }


    public void HideCurrentForms()
    {
        if (currentForms.Count > 0)
        {
            foreach (var form in currentForms)
            {
                form.Hide();
                currentForms.Remove(form);
            }
        }
    }

    public void DisplayForm<TForm>(TForm newForm) where TForm : Form
    {
        if (newForm == null)
        {
            _appLogger.Error($"Attempted to display a null form in {nameof(DisplayForm)}.");
            throw new ArgumentNullException(nameof(newForm), "New form is null.");
        }

        currentForms.Add(newForm);
        newForm.Show();
        _appLogger.Info($"Opened {newForm.Name}");
    }




}