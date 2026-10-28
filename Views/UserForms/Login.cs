using POS_Supermarket.Controller;
using POS_Supermarket.Interfaces;


namespace POS_Supermarket.Views.UserForms
{
    public partial class Login : Form, ILoginView
    {
        UserController Controller;

        public string UserName => textBox1.Text;
        public string Password => textBox2.Text;

        public Login(UserController controller)
        {
            InitializeComponent();
            Controller = controller;
        }
        
        private async void button1_Click(object sender, EventArgs e)
        {
           await Controller.HandleLoginUser(this);
        }

        public void Print(string message,string? title, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None)
        {
            MessageBox.Show(message, title, buttons, icon);
        }

        public void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

       
    }
}
