using PaycheckBackend.Models;

namespace PaycheckBackend.Repositories.Interfaces
{
    public interface IWorkdayRepository
    {
        Workday? GetWorkdayById(int id);
        void CreateWorkday(Workday workday, Job job);
        void RecalculateWagesEarned(List<Workday> workdays, Job job);
        void RecalculateWagesEarned(Workday workday, Job job);
        void PatchWorkday(Workday workday, Job job);
    }
}