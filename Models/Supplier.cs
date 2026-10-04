using POS_Supermarket.Models.Base;

namespace POS_Supermarket.Models
{
    public class Supplier: AuditableEntity
    {
        public int SupplierName{ get; set; }
        public int CompanyName{ get; set; }
        public int PhoneNumber{ get; set; }
    }
}
