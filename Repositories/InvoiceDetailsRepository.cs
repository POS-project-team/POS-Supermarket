
using Microsoft.Data.SqlClient;
using POS_Supermarket.Interfaces;
using POS_Supermarket.Models;
using System.Data;
using System.Text;

namespace POS_Supermarket.Repositories
{
    public class InvoiceDetailsRepository(string ConnectionString) : IInvoiceDetailsRepository
    {
        public async Task<bool> AddListOfDetailsForOneInvoiceAsync(int InvoiceId, IEnumerable<InvoiceDetails> Recordes)
        {
  
            StringBuilder Sqlbuilder = new StringBuilder("INSERT INTO  InvoiceDetails ([InvoiceID] ,[ProductId] ,[Quantity] ,[UnitPrice]) VALUES ");

            List<SqlParameter> parameters = [];
            int parameterIndex = 0;

            foreach(InvoiceDetails details in Recordes)
            {
                Sqlbuilder.Append($"(@InvoiceID{parameterIndex}, @ProductId{parameterIndex}, @Quantity{parameterIndex}, @UnitPrice{parameterIndex}),");

                parameters.Add(new SqlParameter { ParameterName = $"@InvoiceID{parameterIndex}", DbType = DbType.Int32, SqlDbType = SqlDbType.Int , SqlValue = details.InvoiceId, IsNullable = false });
                parameters.Add(new SqlParameter { ParameterName = $"@ProductId{parameterIndex}", DbType = DbType.Int32, SqlDbType = SqlDbType.Int , SqlValue = details.ProductId, IsNullable = false });
                parameters.Add(new SqlParameter { ParameterName = $"@Quantity{parameterIndex}", DbType = DbType.Int32, SqlDbType = SqlDbType.Int , SqlValue = details.Quantity, IsNullable = false });
                parameters.Add(new SqlParameter { ParameterName = $"@UnitPrice{parameterIndex}", DbType = DbType.Decimal, SqlDbType = SqlDbType.Decimal , SqlValue = details.Unit_Price, IsNullable = false });

                parameterIndex++;
            }

            using (var connection = new SqlConnection(ConnectionString))
            {

                using (var command = new SqlCommand(Sqlbuilder.ToString().TrimEnd(','), connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                   
                    try
                    {
                        await connection.OpenAsync();
                        int RowEfficted = await command.ExecuteNonQueryAsync();

                        return RowEfficted > 0;
                    }
                    catch (Exception ex)
                    {
                        //هنا كود الفيور
                    }

                    return false;
                }
            }

        }
        public async Task<IEnumerable<InvoiceDetails>> GetDetailsForOneInvoceAsync(int InvoiceId)
        {
            List<InvoiceDetails> records = [];
            string Query = "Select [ID] ,[InvoiceId] ,[ProductId] ,[Quantity] ,[UnitPrice] FROM InvoiceDetails WHERE InvoiceId = @invoiceId";
            
            using (var connection = new SqlConnection(ConnectionString))
            {

                using (var command = new SqlCommand(Query, connection))
                {

                    command.Parameters.Add("@InvoiceId", SqlDbType.Int).Value = InvoiceId;
                    
                    try
                    {
                        await connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        
                            while (await reader.ReadAsync())
                            {
                                records.Add(new InvoiceDetails
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    InvoiceId = Convert.ToInt32(reader["InvoiceId"]),
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Unit_Price = Convert.ToDecimal(reader["UnitPrice"])

                                });
                        
                            await reader.DisposeAsync();

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return records;

        }

    }
}
