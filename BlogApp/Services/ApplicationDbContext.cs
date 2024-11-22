using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RegisterDto> Register { get; set; }

        public DbSet<LoginAttempt> LoginAttempts { get; set; }
    }
}
