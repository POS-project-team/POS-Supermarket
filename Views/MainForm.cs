using POS_Supermarket.Views.UserForms;

namespace POS_Supermarket;

public partial class MainForm : Form
{
    Login Form;
    public MainForm(Login f)
    {
         Form = f;
        InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
        Form.ShowDialog();
    }
}
