using POS_Supermarket.Enum;
using POS_Supermarket.Models;

public interface IUserRepository
{
    public Task<int> AddUserAsync(User user);
    public Task<int> DeleteUserAsync(int Id);
    public Task<bool> UpdateUserAsync(int Id);
    public Task<User> GetUserById(int Id);
    public Task<IEnumerable<User>> GetUsers();
}