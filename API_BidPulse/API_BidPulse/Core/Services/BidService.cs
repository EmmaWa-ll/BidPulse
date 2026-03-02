using API_BidPulse.Core.Interfaces;
using API_BidPulse.Data.DTO.Bid;
using API_BidPulse.Data.Entities;
using API_BidPulse.Data.Interfaces;

namespace API_BidPulse.Core.Services
{
    public class BidService : IBidService
    {

        private readonly IBidRepo _bidRepo;
        private readonly IAuctionRepo _auctionRepo;
        private readonly IUserRepo _userRepo;

        public BidService(IBidRepo bidRepo, IAuctionRepo auctionRepo, IUserRepo userRepo)
        {
            _bidRepo = bidRepo;
            _auctionRepo = auctionRepo;
            _userRepo = userRepo;
        }



        public async Task<List<ShowBidDTO>> GetBidsForAuctionAsync(int auctionId)
        {
            var bids = await _bidRepo.GetBidsByAuctionIdAsync(auctionId);

            var result = new List<ShowBidDTO>();
            foreach (var b in bids)
            {
                var user = await _userRepo.GetByIdAsync(b.UserId);
                result.Add(new ShowBidDTO
                {
                    BidId = b.BidId,
                    BidAmount = b.BidAmount,
                    CreatedAt = b.CreatedAt,
                    UserId = b.UserId,
                    UserName = user?.Name ?? "Unknown"
                });
            }
            return result;
        }

        public async Task<(bool ok, string message)> PlaceBidAsync(CreateBidDTO dto)
        {
            if (dto == null || dto.AuctionId <= 0 || dto.UserId <= 0 || dto.BidAmount <= 0)
                return (false, "Invalid values.");

            var auction = await _auctionRepo.GetByIdAsync(dto.AuctionId);
            if (auction == null) return (false, "Auction not found.");
            if (auction.EndDate <= DateTime.UtcNow) return (false, "Auction is closed.");
            if (auction.UserId == dto.UserId) return (false, "You cannot bid on your own auction.");

            if (await _userRepo.GetByIdAsync(dto.UserId) == null)
                return (false, "User not found.");

            var minBid = (await _bidRepo.GetHighestBidAmountAsync(dto.AuctionId)) ?? auction.StartPrice;
            if (dto.BidAmount <= minBid)
                return (false, $"Bid must be higher than {minBid}.");

            await _bidRepo.AddBidAsync(new Bid
            {
                AuctionId = dto.AuctionId,
                UserId = dto.UserId,
                BidAmount = dto.BidAmount,
                CreatedAt = DateTime.UtcNow 
            });

            return (true, "Bid placed successfully.");
        }
    }
}
