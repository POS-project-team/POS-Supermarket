using POS_Supermarket.Enum;
using POS_Supermarket.Interface;
using POS_Supermarket.Interfaces;
using POS_Supermarket.Interfaces.Views;
using POS_Supermarket.Models;


namespace POS_Supermarket.Controller;
public class UserController(IUserServices service)
{
    public async Task HandleLoginUser(ILoginView loginView)
    {
        string UserName = loginView.UserName.Trim();
        string Password = loginView.Password.Trim();


        if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))//string.IsNullOrEmpty(CreatedBy)
        {
            loginView.Print("User Name Or Password Are Empty");
            return;
        }


        if (await service.IsUserExsist(UserName, Password))
        {
            loginView.Print("Login Sucsses");
            loginView.ClearForm();
        }
        else
            loginView.Print("Login Faile");

    }
    public async Task HandleAddUser(IAddNewUserView newUserView)
    {
        string UserName = newUserView.UserName.Trim();
        string Password = newUserView.Password.Trim();

        //check data here

        UserRole role;
        if (System.Enum.IsDefined(typeof(UserRole), newUserView.RoleValue))
        {
            role = (UserRole)newUserView.RoleValue;
        }
        else
        {
            newUserView.Print("choose Admin or user");
            return;
        }

        if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))//string.IsNullOrEmpty(CreatedBy)
        {
            newUserView.Print("User Name or password are empty");
            return;
        }
        if ((UserName.Length < 3 || UserName.Length > 50) || (Password.Length < 5 || Password.Length > 50))//string.IsNullOrEmpty(CreatedBy)
        {
            newUserView.Print("User name or password are short");
            return;
        }

        var user = new User
        {
            UserName = UserName,
            Password = Password,
            Role = role,
            Created_at = DateTime.UtcNow,
            CreatedBy = newUserView.CreatedBy
        };

        bool Sucsses = await service.AddUser(user);

        if (Sucsses)
        {
            newUserView.Print("Add User Sucsses");

        }
        else
        {
            newUserView.Print("Add User Fail");

        }
    }
    public async Task HandleDeleteUser(IDeleteUserView deleteUserView)
    {
        if (deleteUserView.AskUser($"you will delete user with {deleteUserView.UserId} ID ", "Are you sure"))
        {
            var sucsses = await service.DeleteUser(deleteUserView.UserId);

                if (sucsses)
                    deleteUserView.Print("Delete User Sucsses");
                else
                    deleteUserView.Print("Delete User Fail",null, MessageBoxButtons.OK, MessageBoxIcon.Error);
        
        }  
    }
}