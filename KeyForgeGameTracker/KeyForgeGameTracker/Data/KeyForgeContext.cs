using KeyForgeGameTracker.Models;
using KeyForgeGameTracker.Areas.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace KeyForgeGameTracker.Data
{
    public class KeyForgeContext : IdentityDbContext<AppUser, AppRole, int>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public KeyForgeContext(DbContextOptions<KeyForgeContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Game> Game { get; set; }

        public DbSet<Card> Card { get; set; }

        public DbSet<Deck> Deck { get; set; }

        public DbSet<House> House { get; set; }

        public DbSet<DeckCard> DeckCard { get; set; }
        
        public DbSet<DeckHouse> DeckHouse { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Deck>()
                .HasIndex(u => u.KeyForgeId)
                .IsUnique();

            modelBuilder.Entity<Card>()
                .HasIndex(u => u.KeyForgeId)
                .IsUnique();

            modelBuilder.Entity<House>()
                .HasIndex(u => u.KeyForgeId)
                .IsUnique();


            modelBuilder.Entity<DeckCard>()
                .HasKey(xy => new { xy.DeckId, xy.CardId });

            modelBuilder.Entity<DeckCard>()
                .HasOne(xy => xy.Deck)
                .WithMany(x => x.DeckCards)
                .HasForeignKey(xy => xy.DeckId);

            modelBuilder.Entity<DeckCard>()
                .HasOne(xy => xy.Card)
                .WithMany(y => y.DeckCards)
                .HasForeignKey(xy => xy.CardId);


            modelBuilder.Entity<DeckHouse>()
                .HasKey(xy => new { xy.DeckId, xy.HouseId });

            modelBuilder.Entity<DeckHouse>()
                .HasOne(xy => xy.Deck)
                .WithMany(x => x.DeckHouses)
                .HasForeignKey(xy => xy.DeckId);

            modelBuilder.Entity<DeckHouse>()
                .HasOne(xy => xy.House)
                .WithMany(y => y.DeckHouses)
                .HasForeignKey(xy => xy.HouseId);
        }

    }
}
