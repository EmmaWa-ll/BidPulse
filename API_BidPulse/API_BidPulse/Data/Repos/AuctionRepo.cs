using API_BidPulse.Data.Entities;
using API_BidPulse.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_BidPulse.Data.Repos
{
    public class AuctionRepo : IAuctionRepo
    {

        private readonly BidPulseDbContext _context;

        public AuctionRepo(BidPulseDbContext context)
        {
            _context = context;
        }


     
        public async Task AddAuctionAsync(Auction auction)
        {
            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Auction>> GetOpenAuctionsAsync(string? search)
        {
            var now = DateTime.UtcNow;

            var query = _context.Auctions
                .Where(a => a.EndDate > now); // hämta bara de där slutdatum är i framtiden

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(a => a.Title.Contains(term));
            }

            return await query.ToListAsync();
        }

    
        public async Task<Auction?> GetByIdAsync(int id)
        {
            return await _context.Auctions
                .SingleOrDefaultAsync(a => a.AuctionId == id);
        }

       
        public async Task UpdateAuctionAsync(Auction auction)
        {
            var original = await _context.Auctions
                .SingleOrDefaultAsync(a => a.AuctionId == auction.AuctionId);

            if (original == null)
            {
                return;
            }

            _context.Entry(original).CurrentValues.SetValues(auction);
            await _context.SaveChangesAsync();
        }

    }
}
