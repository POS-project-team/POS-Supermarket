
using POS_Supermarket.Models.Base;
using System.Windows.Forms;

namespace POS_Supermarket.Models
{
    public class SaleInvoiceDetails: AuditableEntity
    {
        public int SupplierId { get; set; }
        public int PruductId { get; set; }
        public int Quantity { get; set; }
        public int Unit_Cost{ get; set; }
    }
}
