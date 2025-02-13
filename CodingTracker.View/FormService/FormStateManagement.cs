using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.FormService
{
    public interface IFormStateManagement
    {
        MainPage GetMainPageInstance();
        void SetMainPageInstance(MainPage form);
        CodingSessionPage GetCodingSessionPageInstance();
        void SetCodingSessionPageInstance(CodingSessionPage form);
        EditSessionPage GetEditSessionPageInstance();
        void SetEditSessionPageInstance(EditSessionPage form);
        CodingSessionTimerForm GetCodingSessionTimerInstance();
        void SetCodingSessionTimerInstance(CodingSessionTimerForm form);
        CreateAccountPage GetCreateAccountPageInstance();
        void SetCreateAccountPageInstance(CreateAccountPage form);
        void SaveFormState<TForm>(TForm form, object state) where TForm : Form;
        object GetFormState<TForm>() where TForm : Form;


        bool IsMainPageCreated();
        bool IsCodingSessionPageCreated();
        bool IsEditSessionPageCreated();
        bool IsCodingSessionTimerCreated();
        bool IsCreateAccountPageCreated();


    }

    public class FormStateManagement : IFormStateManagement
    {
        private Dictionary<Type, object> formStates = new Dictionary<Type, object>();
        private MainPage _mainPageInstance;
        private CodingSessionPage _codingSessionPageInstance;
        private EditSessionPage _editSessionPageInstance;
        private CodingSessionTimerForm _codingSessionTimerInstance;
        private CreateAccountPage _createAccountPageInstance;


        private bool _mainPageCreated = false;
        private bool _codingSessionPageCreated = false;
        private bool _editSessionPageCreated = false;
        private bool _codingSessionTimerCreated = false;
        private bool _createAccountPageCreated = false;



        public MainPage GetMainPageInstance()
        {
            return _mainPageInstance;
        }

        public void SetMainPageInstance(MainPage form)
        {
            _mainPageInstance = form;
        }

        public CodingSessionPage GetCodingSessionPageInstance()
        {
            return _codingSessionPageInstance;
        }

        public void SetCodingSessionPageInstance(CodingSessionPage form)
        {
            _codingSessionPageInstance = form;
        }

        public EditSessionPage GetEditSessionPageInstance()
        {
            return _editSessionPageInstance;
        }

        public void SetEditSessionPageInstance(EditSessionPage form)
        {
            _editSessionPageInstance = form;
        }

        public CodingSessionTimerForm GetCodingSessionTimerInstance()
        {
            return _codingSessionTimerInstance;
        }

        public void SetCodingSessionTimerInstance(CodingSessionTimerForm form)
        {
            _codingSessionTimerInstance = form;
        }

        public CreateAccountPage GetCreateAccountPageInstance()
        {
            return _createAccountPageInstance;
        }

        public void SetCreateAccountPageInstance(CreateAccountPage form)
        {
            _createAccountPageInstance = form;
        }

        public void SaveFormState<TForm>(TForm form, object state) where TForm : Form
        {
            formStates[typeof(TForm)] = state;
        }

        public object GetFormState<TForm>() where TForm : Form
        {
            formStates.TryGetValue(typeof(TForm), out var state);
            return state;
        }

        public bool IsMainPageCreated()
        {
            return _mainPageCreated;
        }

        public bool IsCodingSessionPageCreated()
        {
            return _codingSessionPageCreated;
        }

        public bool IsEditSessionPageCreated()
        {
            return _editSessionPageCreated;
        }

        public bool IsCodingSessionTimerCreated()
        {
            return _codingSessionTimerCreated;
        }

        public bool IsCreateAccountPageCreated()
        {
            return _createAccountPageCreated;
        }

    }
}