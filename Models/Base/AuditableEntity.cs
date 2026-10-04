using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Supermarket.Models.Base
{
    public abstract class AuditableEntity:Entity
    {
        public string CreatedBy { set; get; } = string.Empty;
        public string LastModifiedBy { set; get; } = string.Empty;
        public DateTime LastModifiedAtUtc { set; get; }
    }
}
