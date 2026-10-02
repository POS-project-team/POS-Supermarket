using POS_Supermarket.Models;

public interface IUserServices
{
    public Task<bool> AddUser(User user);
    public Task<bool> UpdateUser(User user);
    public Task<bool> DeleteUser(string UserName);
    public Task<bool> IsUserNameExsist(string UserName);
    public Task<bool> IsPasswordExsist(string Password);
    public Task<bool> IsUserExsist(string UserName, string Password);
    public Task<User> GetUserById(int Id);
    public Task<IEnumerable<User>> GetUsers();

}