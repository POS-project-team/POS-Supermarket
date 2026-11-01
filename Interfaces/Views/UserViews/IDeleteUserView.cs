
namespace POS_Supermarket.Interfaces.Views
{
    public interface IDeleteUserView
    {
        public int UserId { get; }
        public void CleanForm();
        public void Print(string message, string? title = null, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None);
        public bool AskUser(string message, string? title = null, MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel, MessageBoxIcon icon = MessageBoxIcon.None);
    }
}
