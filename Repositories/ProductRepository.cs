
using Microsoft.Data.SqlClient;
using POS_Supermarket.Interfaces;
using POS_Supermarket.Models;
using System.Data;

namespace POS_Supermarket.Repositories
{
    public class ProductRepository(string ConnectionString) : IProductRepository
    {
        public async Task<bool> AddProduct(Product Product)
        {
            int RowsAffected = 0;

            string Query = @"
                INSERT INTO Products
                (
                    CreatedBy,
                    BarcodeNumber,
                    ProductName,
                    CategoryId,
                    Price
                )
                VALUES
                (
                    @CreatedBy,
                    @BarcodeNumber,
                    @ProductName,
                    @CategoryId,
                    @Price
                )";

            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(Query, connection))
                {
                    command.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value =
                        (object?)Product.CreatedBy ?? DBNull.Value;

                    command.Parameters.Add("@BarcodeNumber", SqlDbType.NVarChar).Value =
                        (object?)Product.BarcodeNumber ?? DBNull.Value;

                    command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value =
                        (object?)Product.ProductName ?? DBNull.Value;

                    command.Parameters.Add("@CategoryId", SqlDbType.Int).Value =
                        Product.CategoryId;

                    command.Parameters.Add("@Price", SqlDbType.Int).Value =
                        Product.Price;

                    try
                    {
                        await connection.OpenAsync();
                        RowsAffected = await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        // Error handling
                    }
                }
            }

            return RowsAffected > 0; 
        }

        public async Task<bool> UpdateProduct(Product Product)
        {
            int RowsAffected = 0;

            string Query = @"
                UPDATE Products
                SET
                    BarcodeNumber = @BarcodeNumber,
                    ProductName = @ProductName,
                    CategoryId = @CategoryId,
                    Price = @Price,
                     LastModifiedBy = @LastModifiedBy,
                     LastModifiedAtUtc = @LastModifiedAtUtc
                WHERE Id = @Id";

            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(Query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = Product.Id;

                    command.Parameters.Add("@BarcodeNumber", SqlDbType.NVarChar).Value =
                        (object?)Product.BarcodeNumber ?? DBNull.Value;

                    command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value =
                        (object?)Product.ProductName ?? DBNull.Value;

                    command.Parameters.Add("@CategoryId", SqlDbType.Int).Value =
                        Product.CategoryId;

                    command.Parameters.Add("@Price", SqlDbType.Int).Value =
                        Product.Price;
                    command.Parameters.Add("@LastModifiedBy", SqlDbType.NVarChar).Value =
                        (object?)Product.LastModifiedBy ?? DBNull.Value;

                    command.Parameters.Add("@LastModifiedAtUtc", SqlDbType.DateTime2).Value =
                        (object?)Product.LastModifiedAtUtc ?? DBNull.Value;

                    try
                    {
                        await connection.OpenAsync();
                        RowsAffected = await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        // Error handling
                    }
                }
            }

            return RowsAffected > 0;
        }

        public async Task<bool> DeleteProduct(int Id)
        {
            int RowsAffected = 0;

            string Query = @"
                UPDATE Products
                SET
                    IsDeleted = 1,
                    DeletedAt = GETDATE()
                WHERE Id = @Id AND IsDeleted = 0";

            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(Query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

                    try
                    {
                        await connection.OpenAsync();
                        RowsAffected = await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        // Error handling
                    }
                }
            }

            return RowsAffected > 0;
        }

        public async Task<bool> IsProductNameExsist(string ProductName)
        {
            bool IsExsist = false;

            string Query = @"
                SELECT 1
                FROM Products
                WHERE ProductName = @ProductName
                AND IsDeleted = 0";

            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(Query, connection))
                {
                    command.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value =
                        (object?)ProductName ?? DBNull.Value;

                    try
                    {
                        await connection.OpenAsync();

                        object? Result = await command.ExecuteScalarAsync();

                        if (Result != null && Result != DBNull.Value)
                        {
                            IsExsist = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Error handling
                    }
                }
            }

            return IsExsist;
        }

        public async Task<Product?> GetProductById(int Id)
        {
            Product? Product = null;

            string Query = @"
                SELECT
                    Id,
                    CreatedBy,
                    BarcodeNumber,
                    ProductName,
                    CategoryId,
                    Price
                FROM Products
                WHERE Id = @Id AND IsDeleted = 0";

            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(Query, connection))
                {
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

                    try
                    {
                        await connection.OpenAsync();

                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            Product = new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                CreatedBy = Convert.ToString(reader["CreatedBy"])!,
                                BarcodeNumber = Convert.ToString(reader["BarcodeNumber"])!,
                                ProductName = Convert.ToString(reader["ProductName"])!,
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                Price = Convert.ToInt32(reader["Price"])
                            };
                        }

                        await reader.DisposeAsync();
                    }
                    catch (Exception ex)
                    {
                        // Error handling
                    }
                }
            }

            return Product;
        }

        public async Task<IEnumerable<Product>> GetPage(int Page, int PageSize)
        {
            List<Product> Products = [];

            string Query = @"
                SELECT
                    Id,
                    CreatedBy,
                    BarcodeNumber,
                    ProductName,
                    CategoryId,
                    Price
                FROM Products
                WHERE IsDeleted = 0
                ORDER BY Id
                OFFSET (@Page - 1) * @PageSize ROWS
                FETCH NEXT @PageSize ROWS ONLY";

            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(Query, connection))
                {
                    command.Parameters.Add("@Page", SqlDbType.Int).Value = Page;
                    command.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;

                    try
                    {
                        await connection.OpenAsync();

                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            Products.Add(new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                CreatedBy = Convert.ToString(reader["CreatedBy"])!,
                                BarcodeNumber = Convert.ToString(reader["BarcodeNumber"])!,
                                ProductName = Convert.ToString(reader["ProductName"])!,
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                Price = Convert.ToInt32(reader["Price"])
                            });
                        }

                        await reader.DisposeAsync();
                    }
                    catch (Exception ex)
                    {
                        // Error handling
                    }
                }
            }

            return Products;
        }

        public async Task<IEnumerable<Product>> GetPageWithSearch(
            int Page,
            int PageSize,
            string search)
        {
            List<Product> Products = [];

            string Query = @"
                SELECT
                    Id,
                    CreatedBy,
                    BarcodeNumber,
                    ProductName,
                    CategoryId,
                    Price
                FROM Products
                WHERE
                    (
                        ProductName LIKE @Search
                        OR BarcodeNumber LIKE @Search
                    )
                    AND IsDeleted = 0
                ORDER BY Id
                OFFSET (@Page - 1) * @PageSize ROWS
                FETCH NEXT @PageSize ROWS ONLY";

            using (var connection = new SqlConnection(ConnectionString))
            {
                using (var command = new SqlCommand(Query, connection))
                {
                    command.Parameters.Add("@Page", SqlDbType.Int).Value = Page;
                    command.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;

                    command.Parameters.Add("@Search", SqlDbType.NVarChar).Value =
                        "%" + search + "%";

                    try
                    {
                        await connection.OpenAsync();

                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            Products.Add(new Product
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                CreatedBy = Convert.ToString(reader["CreatedBy"])!,
                                BarcodeNumber = Convert.ToString(reader["BarcodeNumber"])!,
                                ProductName = Convert.ToString(reader["ProductName"])!,
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                Price = Convert.ToInt32(reader["Price"])
                            });
                        }

                        await reader.DisposeAsync();
                    }
                    catch (Exception ex)
                    {
                        // Error handling
                    }
                }
            }

            return Products;
        }
    }
}
