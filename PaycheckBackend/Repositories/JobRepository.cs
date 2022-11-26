using PaycheckBackend.Models;
using PaycheckBackend.Repositories.Interfaces;
using PaycheckBackend.Db;

namespace PaycheckBackend.Repositories
{
    public class JobRepository : BaseRepository<Job>, IJobRepository
    {
        public JobRepository(AppDbContext appDbContext) : base(appDbContext)
        { }

        public IEnumerable<Job> GetAllJobs()
        {
            return FindAll()
                .OrderBy(j => j.Company)
                .ToList();
        }

        public Job? GetJobById(int id)
        {
            return FindByCondition(j => j.JobId.Equals(id)).FirstOrDefault();
        }

        public void CreateJob(Job job)
        {
            Create(job);
        }
    }
}