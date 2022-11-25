using PaycheckBackend.Models;
using PaycheckBackend.Repositories.Interfaces;
using PaycheckBackend.Db;

namespace PaycheckBackend.Repositories
{
    public class PaycheckRepository : BaseRepository<Paycheck>, IPaycheckRepository
    {
        public PaycheckRepository(AppDbContext appDbContext) : base(appDbContext)
        { }
    }
}