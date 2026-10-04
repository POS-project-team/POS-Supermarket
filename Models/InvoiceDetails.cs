
using POS_Supermarket.Models.Base;

namespace POS_Supermarket.Models
{
    public class InvoiceDetails: AuditableEntity
    {
        public int InvoiceId { set; get; }
        public int ProductId { set; get; }
        public int Quantity { set; get; }
        public int Unit_Price { set; get; }
    }
}
