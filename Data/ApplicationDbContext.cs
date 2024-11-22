using Microsoft.EntityFrameworkCore;
using CMCS.Models;
using System.Security.Claims;

namespace CMCS.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<LectureClaim> Claims { get; set; }

        public DbSet<Manager> Managers { get; set; }

        
        

    }


}

