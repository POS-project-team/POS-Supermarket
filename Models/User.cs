using System;
using POS_Supermarket.Enum;

namespace POS_Supermarket.Models;
public class User
{
    public int ID{ get; set; }
    public string UserName{ get; set; }
    public string Password{ get; set; }
    public UserRole Role{ get; set; }
    public DateTime Created_at{ get; set; }
    public string Phone_number { get; set; } = null!;
    public decimal? Salary { get; set; } = null;

}




