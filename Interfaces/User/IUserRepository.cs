using POS_Supermarket.Enum;
using POS_Supermarket.Models;

public interface IUserRepository
{
    public Task<int?> AddUserAsync(User user);
    public Task<bool> DeleteUserAsync(int Id);
    public Task<bool> UpdateUserAsync(User user);
    public Task<User?> GetUserById(int Id);
    public Task<IEnumerable<User>> GetUsers();
    public Task<bool> IsUserExsistByUserName(string UserName);
    public Task<bool> IsUserExsistById(int Id);
    public Task<bool> IsUserExsist(string UserName,string Password);

}