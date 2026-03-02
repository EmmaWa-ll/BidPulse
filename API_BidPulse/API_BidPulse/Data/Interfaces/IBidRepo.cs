using API_BidPulse.Data.Entities;

namespace API_BidPulse.Data.Interfaces
{
    public interface IBidRepo
    {

        Task AddBidAsync(Bid bid);
        Task<List<Bid>> GetBidsByAuctionIdAsync(int auctionId);
        Task<decimal?> GetHighestBidAmountAsync(int auctionId);
    }
}
