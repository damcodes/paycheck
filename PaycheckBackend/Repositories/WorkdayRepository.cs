using PaycheckBackend.Models;
using PaycheckBackend.Repositories.Interfaces;
using PaycheckBackend.Db;

namespace PaycheckBackend.Repositories
{
    public class WorkdayRepository : BaseRepository<Workday>, IWorkdayRepository
    {
        public WorkdayRepository(AppDbContext appDbContext) : base(appDbContext)
        { }
    }
}