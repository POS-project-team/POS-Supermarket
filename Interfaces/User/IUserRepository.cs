using POS_Supermarket.Enum;
using POS_Supermarket.Models;

public interface IUserRepository
{
    public Task<int?> AddUserAsync(User user);
    public Task<bool> DeleteUserAsync(int Id);
    public Task<bool> UpdateUserAsync(User user);
    public Task<User?> GetUserById(int Id);
    public Task<IEnumerable<User>> GetUsersAsync(int Page, int pageSize, string SearchColumn, string search = null);
    public Task<bool> IsUserExsistByUserNameAsync(string UserName);
    public Task<bool> IsUserExsistByIdAsync(int Id);
    public Task<bool> IsUserExsistAsync(string UserName,string Password);

}