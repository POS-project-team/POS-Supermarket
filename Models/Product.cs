
using POS_Supermarket.Models.Base;

namespace POS_Supermarket.Models
{
    public class Product: AuditableEntity
    {
       
        //13 charcters
        public string BarcodeNumbe { set; get; } = string.Empty;
        public string ProductName { set; get; } = string.Empty;
        public int CategoryId { set; get; }
        public int Price { set; get; }

    }
}


