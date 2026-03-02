using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace API_BidPulse.Data.Entities
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(80)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(150)]
        public string Email { get; set; } = null!;

        [Required, MaxLength(255)]
        public string Password { get; set; } = null!;

        // Navigation
        public List<Auction> Auctions { get; set; } = new();
        public List<Bid> Bids { get; set; } = new();
    }
}
