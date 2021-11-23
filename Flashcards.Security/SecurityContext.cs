using Flashcards.Security.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.Security
{
    public class SecurityContext: DbContext
    {
        public SecurityContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }

        public DbSet<LoginUser> LoginUsers { get; set; }
    }
}