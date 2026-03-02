using API_BidPulse.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_BidPulse.Data

{
    public class BidPulseDbContext : DbContext
    {
        public BidPulseDbContext(DbContextOptions<BidPulseDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Auction>()
                .Property(a => a.StartPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Bid>()
                .Property(b => b.BidAmount)
                .HasPrecision(18, 2);

            
            modelBuilder.Entity<Bid>()
                .HasOne(b => b.Auction)
                .WithMany(a => a.Bids)
                .HasForeignKey(b => b.AuctionId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
