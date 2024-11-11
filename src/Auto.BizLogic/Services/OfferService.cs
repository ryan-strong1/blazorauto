using Auto.BizLogic.Mapping;
using Auto.BizLogic.Models.Dto;
using Auto.Data.Entities;
using Auto.Data.Repositories;
using FluentValidation;

namespace Auto.BizLogic.Services
{
    public interface IOfferService
    {
        Task<IList<OfferDto>> GetOffers();

        Task<OfferDto?> GetOfferById(int id);

        Task<OfferDto> CreateOffer(CreateOfferDto createOfferDto);

        Task<bool> DeleteOfferByIdAsync(int id);
    }

    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IValidator<CreateOfferDto> _offerValidator;

        public OfferService(IOfferRepository offerRepository, IValidator<CreateOfferDto> offerValidator)
        {
            _offerRepository = offerRepository;
            _offerValidator = offerValidator;
        }

        public async Task<OfferDto> CreateOffer(CreateOfferDto createOfferDto)
        {
            var validationResult = await _offerValidator.ValidateAsync(createOfferDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var offer = new OfferEntity()
            {
                AutoId = createOfferDto.AutoId,
                UserId = createOfferDto.UserId,
                Amount = createOfferDto.Amount,
                Status = "Pending",
                Created = DateTime.Now,
                Expires = DateTime.Now.AddDays(7)
            };

            await _offerRepository.AddAsync(offer);

            return offer.ToDto();
        }

        public async Task<IList<OfferDto>> GetOffers()
        {
            var offers = await _offerRepository.GetAllAsync();
            return offers.Select(q => q.ToDto()).ToList();
        }

        public async Task<OfferDto?> GetOfferById(int id)
        {
            var offer = await _offerRepository.GetByIdAsync(id);
            return offer?.ToDto();
        }

        public async Task<IList<OfferDto>> GetOffersByAutoId(int id)
        {
            var offers = await _offerRepository.GetOffersByAutoId(id);
            return offers.Select(q => q.ToDto()).ToList();
        }

        public async Task<bool> DeleteOfferByIdAsync(int id)
        {
            var user = await _offerRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            await _offerRepository.DeleteAsync(id);

            return true;
        }
    }
}