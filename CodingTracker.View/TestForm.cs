namespace CodingTracker.View
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            TestBorderlessForm.DragForm = true;
            TestBorderlessForm.BorderRadius = 12;
        }
    }
}
