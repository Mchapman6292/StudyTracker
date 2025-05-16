using CodingTracker.Common.LoggingInterfaces;

namespace CodingTracker.View.FormManagement
{

    public interface IFormNavigator
    {
        Form SwitchToForm(FormPageEnum formType);
        void CloseLoginPage();
        Form SwitchToFormWithoutPreviousFormClosing(FormPageEnum formType);
        void SwitchToTimerAndWaveForm();

    }


    public class FormNavigator : IFormNavigator
    {
        private readonly IFormManager _formController;
        private readonly IFormFactory _formFactory;
        private readonly IFormStateManagement _formStateManagement;
        private readonly IApplicationLogger _appLogger;


        public FormNavigator(IFormManager formController, IFormFactory formFactory, IFormStateManagement formStateManagement, IApplicationLogger appLogger)
        {
            _formController = formController;
            _formFactory = formFactory;
            _formStateManagement = formStateManagement;
            _appLogger = appLogger;
        }

        public Form SwitchToCreateAccountPage() // This is implemented to return an instance of CreateAccountForm so that the AccountCreatedCallback can be triggered. This allows for the Account Created message to be displayed on the LoginPage once a user account has been created. 
        {
            var createAccountPage = _formFactory.CreateForm(FormPageEnum.CreateAccountForm);
            _formController.HandleAndShowForm(() => createAccountPage, nameof(CreateAccountPage), true);
            return createAccountPage;
        }

        public Form SwitchToForm(FormPageEnum formType)
        {
            var oldForm = _formStateManagement.GetCurrentForm();
            var newForm = _formFactory.GetOrCreateForm(formType);

            if (oldForm != null)
            {
                oldForm.Hide();
            }

            _formStateManagement.SetCurrentForm(newForm);

            newForm.Show();

            return newForm;
        }

        public Form SwitchToFormWithoutPreviousFormClosing(FormPageEnum formType)
        {
            var currentForm = _formStateManagement.GetCurrentForm();
            var newForm = _formFactory.GetOrCreateForm(formType);

            if (_formStateManagement.CheckIfFormEnumIsTimerForm(formType))
            {
                _formStateManagement.AddActiveTimerForm(formType, newForm);
            }
            else
            {
                _formStateManagement.SetCurrentForm(newForm);
            }

            if (currentForm != null)
            {
                currentForm.WindowState = FormWindowState.Normal;
                currentForm.Show();
            }


            newForm.Show();

            return newForm;
        }


        public void CloseLoginPage()
        {
            var loginForm = Application.OpenForms.OfType<LoginPage>().FirstOrDefault();
            if (loginForm != null)
            {
                loginForm.Close();
            }
        }

        public void SwitchToTimerAndWaveForm()
        {
            var oldForm = _formStateManagement.GetCurrentForm();
            var timerForm = _formFactory.GetOrCreateForm(FormPageEnum.CountdownTimerForm);
            var waveForm = _formFactory.GetOrCreateForm(FormPageEnum.WaveVisualizationForm);

            if (oldForm != null)
            {
                oldForm.Hide();
            }

            _formStateManagement.SetCurrentForm(timerForm);

            timerForm.Show();
            waveForm.Show();
        }

    }
}
