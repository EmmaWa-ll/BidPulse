using API_BidPulse.Core.Interfaces;
using API_BidPulse.Data.DTO.Auction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API_BidPulse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {

        private readonly IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }



        // GET: api/auction?search=iphone
        [HttpGet]
        [SwaggerOperation(
        Summary = "Find/Get open auctions",
        Description = "Returns all open auctions. Optional search filters by auction title."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOpenAuctions([FromQuery] string? search)
        {
            var result = await _auctionService.GetOpenAuctionsAsync(search);
            return Ok(result);
        }

        // GET: api/auction/5
        [HttpGet("{id}")]
        [SwaggerOperation(
        Summary = "Get auction by id",
        Description = "Returns detailed information about a specific auction."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var auction = await _auctionService.GetByIdAsync(id);
            if (auction == null) return NotFound();

            return Ok(auction);
        }

        // POST: api/auction
        [HttpPost]
        [SwaggerOperation(
        Summary = "Create auction",
        Description = "Creates a new auction for a specific user. The auction starts immediately and remains open until the end date."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateAuctionDTO dto)
        {
            var ok = await _auctionService.CreateAuctionAsync(dto);
            if (!ok) return BadRequest("Invalid auction data.");

            return Ok(new { message = "Auction created successfully." });
        }

        // PUT: api/auction/5
        [HttpPut("{id}")]
        [SwaggerOperation(
        Summary = "Update auction",
        Description = "Updates an existing auction. Only provided fields will be updated."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, UpdateAuctionDTO dto)
        {
            var ok = await _auctionService.UpdateAuctionAsync(id, dto);
            if (!ok) return BadRequest("Could not update auction.");

            return Ok(new { message = "Auction updated successfully." });
        }

    }
}
