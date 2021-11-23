using Flashcards.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.DataAccess
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            
        }
        
        public virtual DbSet<DeckEntity> Decks { get; set; }
        public virtual DbSet<CardEntity> Cards { get; set; }
    }
}