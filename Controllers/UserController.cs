using POS_Supermarket.Enum;
using POS_Supermarket.Interface;
using POS_Supermarket.Interfaces;
using POS_Supermarket.Models;
using System.IO.Enumeration;

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
        if (System.Enum.IsDefined(typeof(UserRole), newUserView.RoleValue)) {
            role = (UserRole)newUserView.RoleValue;
        }
        else
        {
            newUserView.Print("«Œ «— —ﬁ„ »Ì‰ 0 Ê 1 ");
            return;
        }

        if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))//string.IsNullOrEmpty(CreatedBy)
        {
            newUserView.Print("›«÷Ì");
            return;
        }
        if ((UserName.Length < 3 || UserName.Length > 50) || (Password.Length < 5 || Password.Length > 50))//string.IsNullOrEmpty(CreatedBy)
        {
            newUserView.Print("⁄œ· «·ÿÊ·");
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
            newUserView.Print(" „ «÷«›… „” Œœ„ ÃœÌœ");

        }
       else
       {
            newUserView.Print("›‘·  «÷«›… „” Œœ„ ÃœÌœ");

        }
    }
}