using Flashcards_backend.Core.Models;
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
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<Attempt> Attempts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardEntity>()
                .HasOne(c => c.Deck)
                .WithMany(d => d.Cards);
        }
    }
}