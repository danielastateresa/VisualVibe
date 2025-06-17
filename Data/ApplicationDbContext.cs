using Microsoft.EntityFrameworkCore;
using VisualVibe.Models;
using VisualVibe.Data;

namespace VisualVibe.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; } 
        public DbSet<User> Users { get; set; } 

    }
}
