using Microsoft.Data.SqlClient;
using POS_Supermarket.Enum;
using POS_Supermarket.Models;
using System.Data;

public class UserRepository(string ConnectionString) : IUserRepository
{

    public async Task<int?> AddUserAsync(User user)
    {
        int? Id = null;
        string Query = "INSERT INTO  Users ([UserName] ,[Password] ,[RoleValue],[RoleValue],[Salary],[CreatedBy]) VALUES @UserName ,@Password,@RoleValue,@RoleValue,@Salary,@CreatedBy) SELECT SCOPE_IDENTITY()  ";

        using (var connection = new SqlConnection(ConnectionString))
        {

            using (var command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = (object?)user.UserName ?? DBNull.Value;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = (object?)user.Password ?? DBNull.Value;
                command.Parameters.Add("@RoleValue", SqlDbType.Int).Value = (object?)user.Role ?? DBNull.Value;
                command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = (object?)user.Salary ?? DBNull.Value;
                command.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = (object?)user.CreatedBy ?? DBNull.Value;


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

        string Query = "Update Users Set UserName = UserName+'_Deleted_'+Cast(ID as nvarchar) , IsDeleted = 1, DeletedAt = GETDATE() Where ID = @ID";

        using (var connection = new SqlConnection(ConnectionString))
        {

            using (var command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@ID", SqlDbType.Int).Value = (object?)Id ?? DBNull.Value;

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
        string Query = "Select [UserName] ,[Password] ,[Role] ,[Created_at] ,[Salary] ,[IsActive] FROM Users Where Id = @ID And IsDeleted = 0";

        using (var connection = new SqlConnection(ConnectionString))
        {

            using (var command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@ID", SqlDbType.Int).Value = (object?)Id ?? DBNull.Value;

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        user = ReadDataFromReader(reader);

                        await reader.DisposeAsync();
                        user.Id = Id;
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

        string Query = "UPDATE Users SET [UserName] = @UserName,[Password] = @Password,[Role] = @Role ,[Salary] = @Salary ,[LastModifiedBy] = @LastModifiedBy,[LastModifiedAtUtc] = @LastModifiedAtUtcWhere,[IsActive] = @IsActive ID = @ID And IsActive = 1";


        using (var connection = new SqlConnection(ConnectionString))
        {


            using (var command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@ID", SqlDbType.Int).Value = (object?)user.Id ?? DBNull.Value;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = (object?)user.UserName ?? DBNull.Value;
                command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = (object?)user.Salary ?? DBNull.Value;
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = (object?)user.Password ?? DBNull.Value;
                command.Parameters.Add("@LastModifiedAtUtc", SqlDbType.Date).Value = (object?)user.LastModifiedAtUtc ?? DBNull.Value;
                command.Parameters.Add("@LastModifiedBy", SqlDbType.NVarChar).Value = (object?)user.LastModifiedBy ?? DBNull.Value;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = (object?)user.IsActive?? DBNull.Value;

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
    public async Task<IEnumerable<User>> GetUsersAsync(int Page,int pageSize,string SearchColumn,string search = null!)
    {
        List<User> users = [];
        string Query = string.Empty;

        if(SearchColumn == ColumnSearchTypes.Id)
            Query = "Select [ID] ,[UserName] ,[Role] ,[Created_at] ,[Salary]  FROM Users  IsDeleted = 0 ORDER BY @SearchColumn OFFSET (@Page - 1)* @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";
        else if (SearchColumn == ColumnSearchTypes.UserName)
            Query = "Select [ID] ,[UserName] ,[Role] ,[Created_at] ,[Salary]  FROM Users Where UserName Like \'@search%\' And IsDeleted = 0 ORDER BY @SearchColumn OFFSET (@Page - 1)* @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY";

        using (var connection = new SqlConnection(ConnectionString))
        {

            using (var command = new SqlCommand(Query, connection))
            {

                command.Parameters.Add("@Page", SqlDbType.NVarChar).Value = (object?)search ?? DBNull.Value;
                command.Parameters.Add("@PageSize", SqlDbType.NVarChar).Value = (object?)search ?? DBNull.Value;

                if (SearchColumn == ColumnSearchTypes.UserName)
                    command.Parameters.Add("@search", SqlDbType.NVarChar).Value = (object?)search ?? DBNull.Value;

                try
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            users.Add(new User
                            {
                                Id = Convert.ToInt32(reader["ID"]),
                                UserName = Convert.ToString(reader["UserName"])!,
                                Role = (UserRole)Convert.ToInt32(reader["Role"])!,
                                Salary = Convert.ToInt32(reader["Salary"]),

                            });
                        }
                        await reader.DisposeAsync();
                       
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        return users;
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
    public async Task<bool> IsUserExsistByUserNameAsync(string UserName)
    {
        bool IsExsist = false;
        string Query = "select 1 from Users Where UserName = @UserName And IsDeleted = 0";

        using (var connection = new SqlConnection(ConnectionString))
        {

            using (var command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = (object?)UserName ?? DBNull.Value;


                try
                {
                    await connection.OpenAsync();
                    object? Result = await command.ExecuteScalarAsync();
                    if (Result == null || Result == DBNull.Value)
                        IsExsist = false;

                    if (Convert.ToInt32(Result) == 1)
                    {
                        IsExsist = true;
                    }
                }
                catch (Exception ex)
                {

                }

                return IsExsist;

            }
        }
    }
    public async Task<bool> IsUserExsistByIdAsync(int Id)
    {
        bool IsExsist = false;
        string Query = "select 1 from Users Where UserId = @UserId And IsDeleted = 0";

        using (var connection = new SqlConnection(ConnectionString))
        {

            using (var command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = (object?)Id ?? DBNull.Value;


                try
                {
                    await connection.OpenAsync();
                    object? Result = await command.ExecuteScalarAsync();
                    if (Result == null || Result == DBNull.Value)
                        IsExsist = false;

                    if (Convert.ToInt32(Result) == 1)
                    {
                        IsExsist = true;
                    }
                }
                catch (Exception ex)
                {

                }

                return IsExsist;

            }
        }
    }
    public async Task<bool> IsUserExsistAsync(string UserName, string Password)
    {
        bool IsExsist = false;
        string Query = "select 1 from Users Where UserName = @UserName And Password = @Password And IsDeleted = 0";

        using (var connection = new SqlConnection(ConnectionString))
        {

            using (var command = new SqlCommand(Query, connection))
            {
                command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = (object?)Password ?? DBNull.Value;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = (object?)UserName ?? DBNull.Value;


                try
                {
                    await connection.OpenAsync();
                    object? Result = await command.ExecuteScalarAsync();
                    if (Result == null || Result == DBNull.Value)
                        IsExsist = false;

                    if (Convert.ToInt32(Result) == 1)
                    {
                        IsExsist = true;
                    }
                }
                catch (Exception ex)
                {

                }

                return IsExsist;

            }
        }
    }
}