using Auto.API.Models;
using Auto.BizLogic.Models.Dto;
using Auto.BizLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auto.API.Controllers
{
    [Route("api/offers")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly ILogger<OfferController> _logger;
        private readonly IOfferService _offerService;

        public OfferController(ILogger<OfferController> logger, IOfferService offerService)
        {
            _logger = logger;
            _offerService = offerService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<OfferDto>), 201)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        public async Task<ActionResult<ApiResponse<OfferDto>>> CreateOffer(CreateOfferDto createOfferDto)
        {
            var offer = await _offerService.CreateOffer(createOfferDto);

            var url = Url.Action(nameof(GetOfferById), new { id = offer.OfferId });

            return Created(url, new ApiResponse<OfferDto>(offer, message: "Offer created successfully"));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<OfferDto>>), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse<IEnumerable<OfferDto>>>> GetOffers()
        {
            var offers = await _offerService.GetOffers();

            return Ok(new ApiResponse<IEnumerable<OfferDto>>(offers, message: "Offers retrieved successfully"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OfferDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ApiResponse<OfferDto>>> GetOfferById(int id)
        {
            var offer = await _offerService.GetOfferById(id);

            if (offer == null)
            {
                return NotFound(new ApiResponse<OfferDto>("Offer not found", success: false));
            }

            return Ok(new ApiResponse<OfferDto>(offer, message: "Offer retrieved successfully"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        [ProducesResponseType(204)]
        public async Task<ActionResult<ApiResponse<string>>> DeleteOfferById(int id)
        {
            var result = await _offerService.DeleteOfferByIdAsync(id);

            if (result == false)
            {
                return NotFound(new ApiResponse<string>("Offer not found", success: false));
            }

            return NoContent();
        }
    }
}