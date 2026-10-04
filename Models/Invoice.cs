
using POS_Supermarket.Models.Base;
using System.Windows.Forms;

namespace POS_Supermarket.Models
{
    public class Invoice :AuditableEntity
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
