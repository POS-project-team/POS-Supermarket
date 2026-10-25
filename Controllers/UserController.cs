using POS_Supermarket.Enum;
using POS_Supermarket.Models;

public class UserController(UserService service)
{
    public void HandleLoginUser(string UserName,string Password)
    {
        service.IsUserExsist(UserName, Password);
    }
    public async Task HandleAddUser(string UserName,string Password,int RoleValue,decimal salary)
    {
        UserName = UserName.Trim();
        Password = Password.Trim();

        //check data here

        UserRole role;
        if (Enum.IsDefined(typeof(UserRole), RoleValue)) {
            role = (UserRole)RoleValue;
        }
        else
        {
            //print
            return;
        }

        if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))//string.IsNullOrEmpty(CreatedBy)
        {
            //print 
            return;
        }
        if ((UserName.Length < 3 || UserName.Length > 500) || (Password.Length < 3 || Password.Length > 500))//string.IsNullOrEmpty(CreatedBy)
        {
            //print 
            return;
        }

        var user = new User
        {
            UserName = UserName,
            Password = Password,
            Role = role,
            Created_at = DateTime.UtcNow,
            Salary = salary
        };

        bool Sucsses = await service.AddUser(user);

       if (Sucsses)
       {
            //print
            
       }
       else
       {
            //print

       }
    }
}