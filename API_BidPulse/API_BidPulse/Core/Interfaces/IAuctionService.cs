using API_BidPulse.Data.DTO.Auction;

namespace API_BidPulse.Core.Interfaces
{
    public interface IAuctionService
    {
        Task<List<AuctionListDTO>> GetOpenAuctionsAsync(string? search);
        Task<AuctionDetailDTO?> GetByIdAsync(int id);
        Task<bool> CreateAuctionAsync(CreateAuctionDTO dto);
        Task<bool> UpdateAuctionAsync(int id, UpdateAuctionDTO dto);
    }
}
