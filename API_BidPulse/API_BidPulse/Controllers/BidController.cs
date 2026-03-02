using API_BidPulse.Core.Interfaces;
using API_BidPulse.Data.DTO.Bid;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API_BidPulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {

        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }



        [HttpGet("auction/{auctionId}")]
        [SwaggerOperation(
        Summary = "Get all bids for an auction",
        Description = "Returns all bids placed on a specific auction, ordered by bid amount."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBidsForAuction(int auctionId)
        {
            var bids = await _bidService.GetBidsForAuctionAsync(auctionId);
            return Ok(bids);
        }

        // POST: api/bid
        [HttpPost]
        [SwaggerOperation(
        Summary = "Place a bid on an auction",
        Description = "Places a bid on an open auction. The bid must be higher than the current highest bid. " +
                  "The auction owner cannot place bids on their own auction."
        )]
        public async Task<IActionResult> PlaceBid(CreateBidDTO dto)
        {
            var (ok, message) = await _bidService.PlaceBidAsync(dto);

            if (!ok)
                return BadRequest(message);

            return Ok(new { message });
        }
    }
}
