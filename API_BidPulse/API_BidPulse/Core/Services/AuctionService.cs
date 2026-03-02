using API_BidPulse.Core.Interfaces;
using API_BidPulse.Data.DTO.Auction;
using API_BidPulse.Data.Entities;
using API_BidPulse.Data.Interfaces;

namespace API_BidPulse.Core.Services
{
        public class AuctionService : IAuctionService
        {
            private readonly IAuctionRepo _auctionRepo;
            private readonly IUserRepo _userRepo;

            public AuctionService(IAuctionRepo auctionRepo, IUserRepo userRepo)
            {
                _auctionRepo = auctionRepo;
                _userRepo = userRepo;
            }





            public async Task<List<AuctionListDTO>> GetOpenAuctionsAsync(string? search)
            {
                var auctions = await _auctionRepo.GetOpenAuctionsAsync(search);

                return auctions.Select(a => new AuctionListDTO
                {
                    AuctionId = a.AuctionId,
                    Title = a.Title,
                    StartPrice = a.StartPrice,
                    EndDate = a.EndDate,
                    UserId = a.UserId,
                    ImageUrl = a.ImageUrl
                }).ToList();
            }

            public async Task<AuctionDetailDTO?> GetByIdAsync(int id)
            {
                var auction = await _auctionRepo.GetByIdAsync(id);
                if (auction == null) return null;
                var user = await _userRepo.GetByIdAsync(auction.UserId);

            return new AuctionDetailDTO
                {
                    AuctionId = auction.AuctionId,
                    Title = auction.Title,
                    Description = auction.Description,
                    StartPrice = auction.StartPrice,
                    StartDate = auction.StartDate,
                    EndDate = auction.EndDate,
                    UserId = auction.UserId,
                    UserName = user?.Name ?? "Unknown",
                    ImageUrl = auction.ImageUrl
                };
            }

        public async Task<bool> CreateAuctionAsync(CreateAuctionDTO dto)
        {
            if (dto == null ||
                string.IsNullOrWhiteSpace(dto.Title) ||
                dto.StartPrice < 0 ||
                dto.UserId <= 0)
                return false;
            var start = DateTime.UtcNow;

         
            if (dto.EndDate <= start) return false;

            var user = await _userRepo.GetByIdAsync(dto.UserId);
            if (user == null) return false;

            var auction = new Auction
            {
                Title = dto.Title.Trim(),
                Description = dto.Description?.Trim() ?? string.Empty,
                StartPrice = dto.StartPrice,
                StartDate = start,
                EndDate = dto.EndDate,
                UserId = dto.UserId,
                ImageUrl = dto.ImageUrl
            };
            await _auctionRepo.AddAuctionAsync(auction);
            return true;
        }


        public async Task<bool> UpdateAuctionAsync(int id, UpdateAuctionDTO dto)
        {
            var auction = await _auctionRepo.GetByIdAsync(id);
            if (auction == null) return false;

            // uppdatera bara det som skickas in
            if (!string.IsNullOrWhiteSpace(dto.Title))
               auction.Title = dto.Title.Trim();
            if (dto.Description != null)
                auction.Description = dto.Description.Trim();
            if (dto.StartPrice.HasValue)
            {
                if (dto.StartPrice < 0) return false;
                auction.StartPrice = dto.StartPrice.Value;
            }
            if (dto.EndDate.HasValue)
            {
                if (dto.EndDate <= DateTime.UtcNow) return false;
                auction.EndDate = dto.EndDate.Value;
            }
            await _auctionRepo.UpdateAuctionAsync(auction);
            return true;
        }

    }
}
