using PaycheckBackend.Models;
using PaycheckBackend.Repositories.Interfaces;
using PaycheckBackend.Db;
using PaycheckBackend.Repositories;

namespace PaycheckBackend.Repositories
{
    public class WorkdayRepository : BaseRepository<Workday>, IWorkdayRepository
    {
        public WorkdayRepository(AppDbContext appDbContext) : base(appDbContext)
        { }

        public Workday? GetWorkdayById(int id)
        {
            return FindByCondition(w => w.WorkdayId.Equals(id)).FirstOrDefault();
        }

        public void CreateWorkday(Workday workday)
        {
            workday.TimeIn = DateTime.SpecifyKind(workday.TimeIn, DateTimeKind.Utc);
            workday.TimeOut = DateTime.SpecifyKind(workday.TimeOut, DateTimeKind.Utc);

            Create(workday);
        }
    }
}