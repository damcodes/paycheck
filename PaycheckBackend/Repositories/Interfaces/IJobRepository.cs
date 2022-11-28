using PaycheckBackend.Models;

namespace PaycheckBackend.Repositories.Interfaces
{
    public interface IJobRepository
    {
        IEnumerable<Job> GetAllJobs();
        Job? GetJobById(int id);
        void CreateJob(Job job);
    }
}