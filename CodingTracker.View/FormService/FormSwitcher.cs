using CodingTracker.View.FormPageEnums;
using System.Windows.Forms;

namespace CodingTracker.View.FormService
{

    public interface IFormSwitcher
    {
        Form SwitchToForm(FormPageEnum formType);
        void CloseLoginPage();

    }


    public class FormSwitcher : IFormSwitcher
    {
        private readonly IFormController _formController;
        private readonly IFormFactory _formFactory;
        private readonly IFormStateManagement _formStateManagement;
        private Form _currentForm;

        public FormSwitcher(IFormController formController, IFormFactory formFactory, IFormStateManagement formStateManagement)
        {
            _formController = formController;
            _formFactory = formFactory;
            _formStateManagement = formStateManagement;
        }

        public Form SwitchToCreateAccountPage() // This is implemented to return an instance of CreateAccountPage so that the AccountCreatedCallback can be triggered. This allows for the Account Created message to be displayed on the LoginPage once a user account has been created. 
        {
            var createAccountPage = _formFactory.CreateForm(FormPageEnum.CreateAccountPage);
            _formController.HandleAndShowForm(() => createAccountPage, nameof(PassWordTextBox), true);
            return createAccountPage;
        }

        public Form SwitchToForm(FormPageEnum formType)
        {
            var form = _formFactory.GetOrCreateForm(formType);

            if (_currentForm != null)
            {
                _currentForm.Hide();
            }
            _currentForm = form;

            form.Show();

            return form;
        }

        public void CloseLoginPage()
        {
            var loginForm = Application.OpenForms.OfType<LoginPage>().FirstOrDefault();
            if (loginForm != null)
            {
                loginForm.Close();
            }
        }
    }
}
