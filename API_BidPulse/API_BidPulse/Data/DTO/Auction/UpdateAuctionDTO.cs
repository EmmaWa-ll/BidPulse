namespace API_BidPulse.Data.DTO.Auction
{
    public class UpdateAuctionDTO
    {

        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? StartPrice { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
