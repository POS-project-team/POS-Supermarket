using POS_Supermarket.Enum;
using POS_Supermarket.Models;

public class UserController(UserService service)
{
    public void HandleLoginUser(string UserName,string Password)
    {
        service.IsUserExsist(UserName, Password);
    }
    public void HandleAddUser(string UserName,string Password,int RoleValue,string PhoneNumber,decimal salary)
    {

        //check data here

        UserRole role;
        if (Enum.IsDefined(typeof(UserRole), RoleValue)) {
            role = (UserRole)RoleValue;
        }
        else
            role = UserRole.User;

        var user = new User
        {
            UserName = UserName,
            Password = Password,
            Phone_number = PhoneNumber,
            Role = role,
            Created_at = DateTime.UtcNow,
            Salary = salary

        };
        service.AddUser(user);
    }
}