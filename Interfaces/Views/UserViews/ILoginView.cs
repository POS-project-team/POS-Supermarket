namespace POS_Supermarket.Interfaces
{
    public interface ILoginView
    {
        public string UserName { get; }
        public string Password { get; }
        public void ClearForm();
        public void Print(string message,string? title = null, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None);
    }
}