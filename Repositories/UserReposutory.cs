using POS_Supermarket.Models;

public class UserReposutory : IUserRepository
{
    public Task<int> AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteUserAsync(int Id)
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

    public Task<bool> UpdateUserAsync(int Id)
    {
        throw new NotImplementedException();
    }
}