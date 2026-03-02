using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace API_BidPulse.Data.Entities
{
    public class Auction
    {

        [Key]
        public int AuctionId { get; set; }

        [Required, MaxLength(120)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(3000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal StartPrice { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string? ImageUrl { get; set; }


        // FK → User
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Navigation
        public List<Bid> Bids { get; set; } = new();
    }
}
