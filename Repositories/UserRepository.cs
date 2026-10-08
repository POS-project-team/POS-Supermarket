using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using POS_Supermarket.Enum;
using POS_Supermarket.Models;
using System.Data;

public class UserRepository(string ConnectionString) : IUserRepository
{
    
    public async Task<int?> AddUserAsync(User user)
    {
        int? Id = null ;
        string Query = "INSERT INTO  Users ([UserName] ,[Password] ,[RoleValue],[Created_at],[RoleValue],[Salary]) VALUES @UserName ,@Password,@RoleValue,@Created_at,@RoleValue,@Salary) SELECT SCOPE_IDENTITY()  ";

        using (var connection = new SqlConnection(ConnectionString))
        {

            using (var command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = (object?)user.UserName?? DBNull.Value;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = (object?)user.Password?? DBNull.Value;
                command.Parameters.Add("@RoleValue", SqlDbType.Int).Value = (object?)user.Role?? DBNull.Value;
                command.Parameters.Add("@Created_at", SqlDbType.DateTime).Value = (object?)user.Created_at?? DBNull.Value;
                command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = (object?)user.Salary?? DBNull.Value;
                

                try
                {
                    await connection.OpenAsync();
                    object? Result = await command.ExecuteScalarAsync();
                    if (Result == null || Result == DBNull.Value)
                        throw new DataException("the data come from Database is null fail on insert user");
                    Id = Convert.ToInt32(Result);
                }
                catch (Exception ex)
                {
                    //Â‰« ﬂÊœ «·›ÌÊ—
                }

                return Id;
            }
        }

    }

    public async Task<bool> DeleteUserAsync(int Id)
    {
        int rowaffected = 0;

        string Query = "Update Users Set IsActive = 0 Where ID = @ID";

        using (var connection = new SqlConnection(ConnectionString))
        {

            using (var command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@ID", SqlDbType.Int).Value = (object?)Id ?? DBNull.Value ;

                try
                {
                    await connection.OpenAsync();
                    rowaffected = await command.ExecuteNonQueryAsync();

                }
                catch (Exception ex)
                {
                    //
                }
            }

        }
        return rowaffected > 0;
    }

    public async Task<User?> GetUserById(int Id)
    {
        User? user = null;
        string Query = "Select [UserName] ,[Password] ,[Role] ,[Created_at] ,[Salary]  FROM Users Where Id = @ID And IsActive = 1";

        using (var connection = new SqlConnection(ConnectionString))
        {

            using (var command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@ID",SqlDbType.Int).Value = (object?)Id ?? DBNull.Value;

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        user = ReadDataFromReader(reader);

                        await reader.DisposeAsync();
                        user.Id= Id;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        return user;
    }

  
    public async Task<bool> UpdateUserAsync(User user)
    {
        int rowaffected = 0;

        string Query = "UPDATE Users SET [UserName] = @UserName,[Password] = @Password,[Role] = @Role ,[Salary] = @Salary ,[LastModifiedBy] = @LastModifiedBy,[LastModifiedAtUtc] = @LastModifiedAtUtcWhere ID = @ID And IsActive = 1";

        
        using (var connection = new SqlConnection(ConnectionString))
        {


            using (var command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@ID",SqlDbType.Int).Value = (object?)user.Id??DBNull.Value;
                command.Parameters.Add("@UserName",SqlDbType.NVarChar).Value = (object?)user.UserName??DBNull.Value;
                command.Parameters.Add("@Salary",SqlDbType.Decimal).Value = (object?)user.Salary??DBNull.Value;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = (object?)user.Password?? DBNull.Value;
                command.Parameters.Add("@LastModifiedAtUtc", SqlDbType.Date).Value = (object?)user.LastModifiedAtUtc??DBNull.Value;
                command.Parameters.Add("@LastModifiedBy", SqlDbType.NVarChar).Value = (object?)user.LastModifiedBy??DBNull.Value;
                
                try
                {
                    await connection.OpenAsync();
                    rowaffected = await command.ExecuteNonQueryAsync();

                }
                catch (Exception ex)
                {
                    //
                }
            }
        }
        return rowaffected > 0;
    }

    public Task<IEnumerable<User>> GetUsers()
    {
        throw new NotImplementedException();
    }


    private User ReadDataFromReader(SqlDataReader reader)
    {
    User user = new User
         {
            UserName = Convert.ToString(reader["UserName"])!,
            Password = Convert.ToString(reader["password"])!,
            Role = (UserRole)Convert.ToInt32(reader["Role"])!,
            Salary = Convert.ToInt32(reader["Salary"]),
            Created_at = Convert.ToDateTime(reader["Created_at"]),
            CreatedBy = Convert.ToString(reader["Created_at"])!,

         };
        return user;
    }

    public Task<bool> IsUserExsistByUserName(string UserName)
    {
        int? Id = null;
        string Query = "select 1 from Users Where UserID = @UserId And IsActive = 1";

        using (var connection = new SqlConnection(ConnectionString))
        {

            using (var command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = (object?)user.UserName ?? DBNull.Value;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = (object?)user.Password ?? DBNull.Value;
                command.Parameters.Add("@RoleValue", SqlDbType.Int).Value = (object?)user.Role ?? DBNull.Value;
                command.Parameters.Add("@Created_at", SqlDbType.DateTime).Value = (object?)user.Created_at ?? DBNull.Value;
                command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = (object?)user.Salary ?? DBNull.Value;


                try
                {
                    await connection.OpenAsync();
                    object? Result = await command.ExecuteScalarAsync();
                    if (Result == null || Result == DBNull.Value)
                        throw new DataException("the data come from Database is null fail on insert user");
                    Id = Convert.ToInt32(Result);
                }
                catch (Exception ex)
                {
                    //Â‰« ﬂÊœ «·›ÌÊ—
                }

                return Id;

            }

    public Task<bool> IsUserExsistById(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserExsist(string UserName, string Password)
    {
        throw new NotImplementedException();
    }
}