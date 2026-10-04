using POS_Supermarket.Enum;
using POS_Supermarket.Models.Base;

namespace POS_Supermarket.Models;
public class User:AuditableEntity
{
    public string UserName { get; set; } = string.Empty;
    public string Password{ get; set; } = string.Empty;
    public UserRole Role{ get; set; }
    public DateTime Created_at{ get; set; }
    public string Phone_number { get; set; } = null!;
    public decimal? Salary { get; set; } = null;

}




