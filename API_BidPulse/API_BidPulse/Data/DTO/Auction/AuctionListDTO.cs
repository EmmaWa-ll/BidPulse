namespace API_BidPulse.Data.DTO.Auction
{
    public class AuctionListDTO
    {
        public int AuctionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal StartPrice { get; set; }
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
