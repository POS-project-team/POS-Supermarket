using POS_Supermarket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS_Supermarket.Interfaces
{
    public interface IProductServices
    {
        Task<bool> AddProduct(Product Product);
        Task<bool> UpdateProduct(Product Product);
        Task<bool> DeleteProduct(int Id);
        Task<bool> IsProductNameExsist(string ProductName);
        Task<Product?> GetProductById(int Id);
        Task<IEnumerable<Product>> GetPage(int Page, int PageSize);
        Task<IEnumerable<Product>> GetPageWithSearch(int Page, int PageSize, string search);
    }
}