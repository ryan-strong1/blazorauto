using Auto.Data.Repositories;
using System.Linq;
using Auto.BizLogic.Mapping;
using Auto.BizLogic.Models.Dto;

namespace Auto.BizLogic.Services
{
    public interface IAutoService
    {
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

        public AutoService(IAutoRepository autoRepository)
        {
            _autoRepository = autoRepository;
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