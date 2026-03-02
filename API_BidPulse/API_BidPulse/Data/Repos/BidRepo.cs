using API_BidPulse.Data.Entities;
using API_BidPulse.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_BidPulse.Data.Repos
{
    public class BidRepo :IBidRepo
    {
        private readonly BidPulseDbContext _context;
        public BidRepo (BidPulseDbContext context)
        {
            _context = context;
        }



        public async Task AddBidAsync(Bid bid)
        {
            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Bid>> GetBidsByAuctionIdAsync(int auctionId)
        {
            return await _context.Bids.Where(b => b.AuctionId == auctionId)
                .OrderByDescending(b => b.BidAmount)
                .ToListAsync();
        }

        public async Task<decimal?> GetHighestBidAmountAsync(int auctionId)
        {
            return await _context.Bids.Where(b => b.AuctionId == auctionId)
                .MaxAsync(b => (decimal?)b.BidAmount);
        }
    }
}
