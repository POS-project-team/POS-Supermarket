using POS_Supermarket.Enum;
using POS_Supermarket.Models;
using System.Drawing.Printing;
using System.Security.Cryptography;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

public class UserService(IUserRepository repository) : IUserServices
{
    //authoriztion
    public async Task<bool> AddUser(User user)
    {
        
        if (await repository.IsUserExsistByUserNameAsync(user.UserName))
            return false;

        user.Password = ComputeSha256Hash(user.Password);
        
           int? UserId = await repository.AddUserAsync(user);
        return UserId != null;
    }

    //authoraization how will delete
    //what will delete
    public async Task<bool> DeleteUser(int Id)
    {
        return await repository.DeleteUserAsync(Id);    
    }

    public async Task<User?> GetUserById(int Id)
    {
        return await repository.GetUserById(Id);
    }

    public Task<IEnumerable<User>> GetPage(int Page = 1,int PageSize = 10)
    {
       return repository.GetUsersAsync(Page,PageSize,ColumnSearchTypes.Id);
    }
    public Task<IEnumerable<User>> GetUsers(int Page,int PageSize,string search)
    {
        return repository.GetUsersAsync(Page, PageSize, ColumnSearchTypes.UserName,search);
    }

    public async Task<bool> IsUserExsist(string UserName, string Password)
    {
      return await repository.IsUserExsistAsync(UserName,ComputeSha256Hash(Password));
    }

    public async Task<bool> IsUserNameExsist(string UserName)
    {
      return await repository.IsUserExsistByUserNameAsync(UserName);
    }

    public async Task<bool> UpdateUser(User user)
    {
        if (!await repository.IsUserExsistByIdAsync(user.Id)) return false;

        return await repository.UpdateUserAsync(user);
    }

    string ComputeSha256Hash(string Data)
    {
        using(SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(Data));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

}