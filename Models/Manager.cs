using Microsoft.EntityFrameworkCore;

namespace CMCS.Models
{
    public class Manager
    {
        public int ManagerID { get; set; } // Primary Key
        public string Username { get; set; }
        public string Password { get; set; } // Ideally hashed in real applications
    }
}
