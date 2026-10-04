using Microsoft.VisualBasic.Devices;
using POS_Supermarket.Models.Base;

namespace POS_Supermarket.Models
{
    public class Inventory:AuditableEntity
    {
        public int ProductId{ set; get; }
        public int LowerstLimitOfProduct { set; get; }
        public int ProductQuantity { set; get; }

    }
}
