using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using POS_Supermarket.Enum;
using POS_Supermarket.Interfaces;
using POS_Supermarket.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace POS_Supermarket.Repositories
{
    public class InvoiceDetailsRepository(string ConnectionString) : IinvoiceDetailsRepository
    {
        public async Task<IEnumerable<InvoiceDetails>> GetAllRecordesForOneInvoiceAsync(int InvoiceId)
        {
            List<InvoiceDetails> recordes = [];
            string Query = "SELECT [Id],[Invoiceid],[Productid],[Quantity],[UnitPrice] FROM InvoiceDetails where InvoiceDetails.InvoiceId = @ID";

            await using (var connection = new SqlConnection(ConnectionString))
            {

                await using (var command = new SqlCommand(Query, connection))
                {

                    command.Parameters.Add("@ID", SqlDbType.Int).Value = InvoiceId;
                     
                   
                    try
                    {
                        await connection.OpenAsync();
                        await using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                recordes.Add(new InvoiceDetails
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    InvoiceId = Convert.ToInt32(reader["Invoiceid"]),
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Unit_Price = Convert.ToDecimal(reader["UnitPrice"])
                                });
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return recordes;






















        }
        public async Task<bool> InsertAllRecordesForOneInvoiceAsync(int InvoiceId, IEnumerable<InvoiceDetails> recordes)
        {
            bool Sucsses = false; 

            StringBuilder Querybuilder = new("INSERT INTO  InvoiceDetails([Invoiceid],[Productid],[Quantity],[UnitPrice]) VALUES ");

            var sqlParametes = new List<SqlParameter>();
            int RecordeIndex = 1;
            foreach (var record in recordes)
            {
                Querybuilder.Append($"(@InvoiceId{RecordeIndex},@ProductId{RecordeIndex},@Quantity{RecordeIndex},@UnitPrice{RecordeIndex}),");
                
                sqlParametes.Add(new SqlParameter { ParameterName = $"@InvoiceId{RecordeIndex}", SqlDbType = SqlDbType.Int, Value = record.InvoiceId });
                sqlParametes.Add(new SqlParameter { ParameterName = $"@ProductId{RecordeIndex}", SqlDbType = SqlDbType.Int, Value = record.ProductId });
                sqlParametes.Add(new SqlParameter { ParameterName = $"@Quantity{RecordeIndex}", SqlDbType = SqlDbType.Int, Value = record.Quantity });
                sqlParametes.Add(new SqlParameter { ParameterName = $"@UnitPrice{RecordeIndex}", SqlDbType = SqlDbType.Decimal, Value = record.Unit_Price });
               
                RecordeIndex++;
            }
            
           await using (var connection = new SqlConnection(ConnectionString))
            {

                await using (var command = new SqlCommand(Querybuilder.ToString().TrimEnd(','), connection))
                {
                    
                    command.Parameters.AddRange(sqlParametes.ToArray());

                    try
                    {
                        await connection.OpenAsync();
                        Sucsses = await command.ExecuteNonQueryAsync() > 0;
               
                    }
                    catch (Exception ex)
                    {
                       
                    }

                    return Sucsses;
                }

            }


    }
    
    }
}
