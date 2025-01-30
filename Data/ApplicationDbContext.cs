using JWTTokenAuthenticationPOC.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTTokenAuthenticationPOC.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet <Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
