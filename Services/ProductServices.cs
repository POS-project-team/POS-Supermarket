using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using POS_Supermarket.Models;
using POS_Supermarket.Interfaces;
public class ProductServices(IProductRepository repository) : IProductServices
{
    public async Task<bool> AddProduct(Product Product)
    {
        if (await repository.IsProductNameExsist(Product.ProductName))
            return false;
        return await repository.AddProduct(Product);
    }
    public async Task<bool> DeleteProduct(int Id)
    {
        return await repository.DeleteProduct(Id);
    }
    public async Task<Product?> GetProductById(int Id)
    {
        return await repository.GetProductById(Id);
    }
    public Task<IEnumerable<Product>> GetPage(int Page = 1, int PageSize = 10)
    {
        return repository.GetPage(Page, PageSize);
    }
    public Task<IEnumerable<Product>> GetPageWithSearch(int Page, int PageSize, string search)
    {
        return repository.GetPageWithSearch(Page, PageSize, search);
    }
    public async Task<bool> IsProductNameExsist(string ProductName)
    {
        return await repository.IsProductNameExsist(ProductName);
    }
    public async Task<bool> UpdateProduct(Product Product)
    {
        if (!await repository.IsProductNameExsist(Product.ProductName)) return false;
        return await repository.UpdateProduct(Product);
    }
}