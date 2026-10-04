
using POS_Supermarket.Models.Base;
using System.Windows.Forms;

namespace POS_Supermarket.Models
{
    public class SupplierInvoice: AuditableEntity
    {
        public int SupplierId { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime purchaseDate { get; set; }
    }
}
