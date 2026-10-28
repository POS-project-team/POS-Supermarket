namespace POS_Supermarket.Interfaces
{
    public interface IAddNewUserView
    {
        public string UserName { get; }
        public string Password { get; }
        public int RoleValue{ get; }
        public string  CreatedBy{ get; }
        public void CleanForm();
        public void Print(string message, string? title = null, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None);
        public bool AskUser(string message, string? title = null, MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel, MessageBoxIcon icon = MessageBoxIcon.None);
    }
}
