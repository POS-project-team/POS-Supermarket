
using POS_Supermarket.Models.Base;
using System.Windows.Forms;

namespace POS_Supermarket.Models
{
    public class Invoice : Entity
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime SaleDate { get; set; }
        public List<InvoiceDetails> recordes { get; set; } = [];
    }
}
