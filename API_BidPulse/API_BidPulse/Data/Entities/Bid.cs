using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_BidPulse.Data.Entities
{
    public class Bid
    {
        [Key]
        public int BidId { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal BidAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // FK → Auction
        public int AuctionId { get; set; }
        public Auction Auction { get; set; } = null!;

        // FK → User
        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
