using API_BidPulse.Data.DTO.Bid;

namespace API_BidPulse.Core.Interfaces
{
    public interface IBidService
    {
        Task<List<ShowBidDTO>> GetBidsForAuctionAsync(int auctionId);
        Task<(bool ok, string message)> PlaceBidAsync(CreateBidDTO dto);
    }
}
