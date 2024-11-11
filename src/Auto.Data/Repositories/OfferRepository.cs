using Auto.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auto.Data.Repositories
{
    public interface IOfferRepository : IGenericRepository<OfferEntity>
    {
        Task<OfferEntity> CreateOffer(OfferEntity offerEntity);
        Task<IList<OfferEntity>> GetOffersByAutoId(int id);
    }

    public class OfferRepository : GenericRepository<OfferEntity>, IOfferRepository
    {
        public OfferRepository(AutoDbContext autoDbContext) : base(autoDbContext)
        {

        }

        public async Task<IList<OfferEntity>> GetOffersByAutoId(int id)
        {
            return await _context.Offers
                .Where(q => q.AutoId == id)
                .ToListAsync();
        }

        public async Task<OfferEntity> CreateOffer(OfferEntity offerEntity)
        {
            _context.Offers.Add(offerEntity);
            await _context.SaveChangesAsync();
            return offerEntity;
        }
    }
}
