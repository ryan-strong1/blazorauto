using Auto.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auto.Data.Repositories
{
    public interface IUserRepository:IGenericRepository<UserEntity>
    {
        Task<UserEntity?> GetUserbyEmail(string email);
    }
    
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(AutoDbContext autoDbContext) : base(autoDbContext)
        {
      
        }

        public async Task<UserEntity?> GetUserbyEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

    }
}
