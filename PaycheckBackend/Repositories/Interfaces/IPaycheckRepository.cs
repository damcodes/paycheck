using PaycheckBackend.Models;

namespace PaycheckBackend.Repositories.Interfaces
{
    public interface IPaycheckRepository
    {
        Paycheck? GetPaycheckById(int id);
        void CreatePaycheck(Paycheck paycheck, Job job);
        void CalculateAndAdjustPaycheckAmount(Workday workday, Paycheck paycheck, Job job);
    }
}