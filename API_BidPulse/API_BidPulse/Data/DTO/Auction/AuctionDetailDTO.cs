namespace API_BidPulse.Data.DTO.Auction
{
    public class AuctionDetailDTO
    {
        public int AuctionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }

        public decimal StartPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

    }
}
