using Microsoft.EntityFrameworkCore;
using JWTTokenAuthenticationPOC.Models;  // Adjust namespace if needed

namespace JWTTokenAuthenticationPOC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public ApplicationDbContext() { }
        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }  
    }
}
