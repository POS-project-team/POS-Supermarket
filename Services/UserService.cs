using POS_Supermarket.Models;

public class UserService : IUserServices
{
    public Task<bool> AddUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUser(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUsers()
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsPasswordExsist(string Password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserExsist(string UserName, string Password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserNameExsist(string UserName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}