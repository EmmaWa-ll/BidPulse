using API_BidPulse.Data.Entities;

namespace API_BidPulse.Data.Interfaces
{
    public interface IAuctionRepo
    {
        
        Task AddAuctionAsync(Auction auction);

        
        Task<List<Auction>> GetOpenAuctionsAsync(string? search);
        Task<Auction?> GetByIdAsync(int id);

        Task UpdateAuctionAsync(Auction auction);
    }
}
