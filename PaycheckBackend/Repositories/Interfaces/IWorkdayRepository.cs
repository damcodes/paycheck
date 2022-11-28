using PaycheckBackend.Models;

namespace PaycheckBackend.Repositories.Interfaces
{
    public interface IWorkdayRepository
    {
        Workday? GetWorkdayById(int id);
        void CreateWorkday(Workday workday);
    }
}