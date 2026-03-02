namespace API_BidPulse.Data.DTO.Bid
{
    public class CreateBidDTO
    {
        public int AuctionId { get; set; }
        public int UserId { get; set; }
        public decimal BidAmount { get; set; }
    }
}
