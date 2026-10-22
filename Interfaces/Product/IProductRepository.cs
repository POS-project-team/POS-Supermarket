using POS_Supermarket.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace POS_Supermarket.Interfaces
{
    internal interface IProductRepository
    {
        public Task<bool> AddProduct (Product  Product );
        public Task<bool> UpdateProduct (Product  Product );
        public Task<bool> DeleteProduct (int Id);
        public Task<bool> IsProductNameExsist(string ProductName);
        public Task<Product?> GetProductById(int Id);
        public Task<IEnumerable<Product >> GetPage(int Page, int PageSize);
        public Task<IEnumerable<Product >> GetPageWithSearch(int Page, int PageSize, string search);
    }
}
