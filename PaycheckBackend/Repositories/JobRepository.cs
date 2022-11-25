using PaycheckBackend.Models;
using PaycheckBackend.Repositories.Interfaces;
using PaycheckBackend.Db;

namespace PaycheckBackend.Repositories
{
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        public JobRepository(AppDbContext appDbContext) : base(appDbContext)
        { }
    }
}