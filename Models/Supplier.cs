using POS_Supermarket.Models.Base;

namespace POS_Supermarket.Models
{
    public class Supplier: AuditableEntity
    {
        public string SupplierName { get; set; } = string.Empty;
        public string CompanyName{ get; set; } = string.Empty;
        public int PhoneNumber{ get; set; }
    }
}
