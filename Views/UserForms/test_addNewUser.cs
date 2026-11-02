using POS_Supermarket.Controller;
using POS_Supermarket.Interfaces;


namespace POS_Supermarket.Views.UserForms
{
    public partial class test_addNewUser : Form,IAddNewUserView
    {
        UserController Controller;
        public test_addNewUser(UserController controller)
        {
            InitializeComponent();
            Controller = controller;
        }

        public string UserName => textBox1.Text;

        public string Password => textBox2.Text;

        public int RoleValue => Convert.ToInt32(textBox3.Text);

        public string CreatedBy => textBox4.Text;

        public bool AskUser(string message, string? title = null, MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            throw new NotImplementedException();
        }

        public void CleanForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        public void Print(string message, string? title = null, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            MessageBox.Show(message, title, buttons, icon);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
          await Controller.HandleAddUser(this);
        }
    }
}
