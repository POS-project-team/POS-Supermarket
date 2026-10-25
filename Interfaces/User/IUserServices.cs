using POS_Supermarket.Models;

public interface IUserServices
{
    public Task<bool> AddUser(User user);
    public Task<bool> UpdateUser(User user);
    public Task<bool> DeleteUser(int Id);
    public Task<bool> IsUserNameExsist(string UserName); 
    public Task<bool> IsUserExsist(string UserName, string Password);
    public Task<User?> GetUserById(int Id);
    public Task<IEnumerable<User>> GetPage(int Page , int PageSize );
    public Task<IEnumerable<User>> GetPageWithSearch(int Page, int PageSize, string search);
}