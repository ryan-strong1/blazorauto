using Auto.Data.Repositories;
using System.Linq;
using Auto.BizLogic.Mapping;
using Auto.BizLogic.Models.Dto;
using Auto.Data.Entities;
using FluentValidation;

namespace Auto.BizLogic.Services
{
    public interface IAutoService
    {
        Task<AutoDto> CreateAuto(CreateAutoDto createAutoDto);

        Task<IList<string>> GetAllMakes();

        Task<IList<string>> GetAllMakes(string make);

        Task<IList<AutoDto>> GetAllAutos();

        Task<IList<AutoDto>> SearchAutos(AutoSearchDto autoSearchDto);

        Task<AutoDto?> GetAutoById(int id);

        Task<IList<AutoDto>> GetAutosByUserId(int id);

        Task<bool> DeleteAutoByIdAsync(int id);
    }

    public class AutoService : IAutoService
    {
        private readonly IAutoRepository _autoRepository;
        private readonly IValidator<CreateAutoDto> _autoValidator;

        public AutoService(IAutoRepository autoRepository, IValidator<CreateAutoDto> autoValidator)
        {
            _autoRepository = autoRepository;
            _autoValidator = autoValidator;
        }

        public async Task<AutoDto> CreateAuto(CreateAutoDto createAutoDto)
        {
            var validationResult = await _autoValidator.ValidateAsync(createAutoDto);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var autoEntity = new AutoEntity()
            {
                Make = createAutoDto.Make,
                Model = createAutoDto.Model,
                Condition = createAutoDto.Condition,
                ModelYear = createAutoDto.ModelYear,
                Mileage = createAutoDto.Mileage,
                Description = createAutoDto.Description,
                Color = createAutoDto.Color,
                Price = createAutoDto.Price,
                UserId = createAutoDto.UserId,
                //TODO: Need to collect location data for time being setting it to 0,0
                Location = new NetTopologySuite.Geometries.Point(0, 0) { SRID = 4326 }
            };

            await _autoRepository.AddAsync(autoEntity);

            return autoEntity.ToDto();
        }

        public async Task<IList<AutoDto>> SearchAutos(AutoSearchDto autoSearchDto)
        {
            var autos = await _autoRepository.SearchAutos(autoSearchDto.ToAutoFilter());
            return autos.Select(s => s.ToDto()).ToList();
        }

        public async Task<IList<AutoDto>> GetAllAutos()
        {
            var autos = await _autoRepository.GetAllAsync();
            return autos.Select(s => s.ToDto()).ToList();
        }

        public async Task<IList<string>> GetAllMakes()
        {
            return await _autoRepository.GetAllMakesAsync();
        }

        public async Task<IList<string>> GetAllMakes(string make)
        {
            return await _autoRepository.GetModelsAsync(make);
        }

        public async Task<IList<AutoDto>> GetAutosByUserId(int id)
        {
            var autos = await _autoRepository.GetAllAutosByUserId(id);
            return autos.Select(s => s.ToDto()).ToList();
        }

        public async Task<AutoDto?> GetAutoById(int id)
        {
            var auto = await _autoRepository.GetByIdAsync(id);
            return auto?.ToDto();
        }

        public async Task<bool> DeleteAutoByIdAsync(int id)
        {
            var auto = await _autoRepository.GetByIdAsync(id);

            if (auto == null)
                return false;

            await _autoRepository.DeleteAsync(id);

            return true;
        }
    }
}