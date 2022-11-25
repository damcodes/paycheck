using PaycheckBackend.Models;
using PaycheckBackend.Repositories.Interfaces;
using PaycheckBackend.Db;

namespace PaycheckBackend.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        { }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll()
                .OrderBy(u => u.FirstName)
                .ToList();
        }
    }
}