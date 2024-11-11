using Auto.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Auto.Data.Repositories
{
    public interface IAutoRepository:IGenericRepository<AutoEntity>
    {
        Task<IList<string>> GetAllMakesAsync();
        Task<IList<string>> GetModelsAsync(string make);
        Task<IList<AutoEntity>> GetAllAutosByUserId(int id);
        Task<IList<AutoEntity>> SearchAutos(Filters.AutoFilter autoFilter);
    }

    public class AutoRepository : GenericRepository<AutoEntity>, IAutoRepository
    {
        private readonly AutoDbContext _autoDbContext;
        public AutoRepository(AutoDbContext autoDbContext) : base(autoDbContext)
        {
            _autoDbContext = autoDbContext;
        }

        public async Task<IList<AutoEntity>> SearchAutos(Filters.AutoFilter autoFilter)
        {
            var query = _autoDbContext.Autos.AsQueryable();

            if (!string.IsNullOrEmpty(autoFilter.Make))
                query = query.Where(q => q.Make == autoFilter.Make);

            if (!string.IsNullOrEmpty(autoFilter.Model))
                query = query.Where(q => q.Model == autoFilter.Model);

            if(!string.IsNullOrEmpty(autoFilter.Condition))
                query = query.Where(q => q.Condition == autoFilter.Condition);

            if (autoFilter.ModelYear.HasValue)
                query = query.Where(q => q.ModelYear == autoFilter.ModelYear);

            if (autoFilter.MinMileage.HasValue && autoFilter.MaxMileage.HasValue)
                query = query.Where(q => q.Mileage >= autoFilter.MinMileage && q.Mileage <= autoFilter.MaxMileage);

            if (autoFilter.MinPrice.HasValue && autoFilter.MaxPrice.HasValue)
                query = query.Where(q => q.Price >= autoFilter.MinPrice && q.Price <= autoFilter.MaxPrice);

            if (!string.IsNullOrEmpty(autoFilter.Color))
                query = query.Where(q => q.Color == autoFilter.Color);

            return await query
                .ToListAsync();
        }

        public async Task<IList<AutoEntity>> GetAllAutosByUserId(int id)
        {
            return await _autoDbContext.Autos
                .Where(q=>q.UserId == id)
                .ToListAsync();
        }

        public async Task<IList<string>> GetAllMakesAsync()
        {
            return await _autoDbContext.Autos
                .Select(a => a.Make)
                .Distinct()
                .OrderBy(o => o)
                .ToListAsync();
        }

        public async Task<IList<string>> GetModelsAsync(string make)
        {
            //if make is not provided return empty list
            if (string.IsNullOrEmpty(make))
                return new List<string>();
            
            return await _autoDbContext.Autos
                .Where(a => a.Make == make)
                .Select(a => a.Model)
                .Distinct()
                .OrderBy(o => o)
                .ToListAsync();
        }
    }
}
