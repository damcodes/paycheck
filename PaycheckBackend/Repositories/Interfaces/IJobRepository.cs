using PaycheckBackend.Models;

namespace PaycheckBackend.Repositories.Interfaces
{
    public interface IJobRepository : IBaseRepository<Job>
    {
        IEnumerable<Job> GetAllJobs();
        Job? GetJobById(int id);
        void CreateJob(Job job);
    }
}