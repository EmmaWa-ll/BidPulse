namespace API_BidPulse.Data.DTO.Bid
{
    public class ShowBidDTO
    {

        public int BidId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
