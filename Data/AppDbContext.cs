using Microsoft.EntityFrameworkCore;
using Student_portal.Web.Models.Entities;

namespace Student_portal.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }


    }
}
