using PaycheckBackend.Models;

namespace PaycheckBackend.Repositories.Interfaces
{
    public interface IPaycheckRepository
    {
        Paycheck? GetPaycheckById(int id);
        Paycheck? GetPaycheckByIdWithWorkdays(int id);
        void CreatePaycheck(Paycheck paycheck, Job job);
        void CalculateAndAdjustPaycheckAmount(Paycheck paycheck, Workday workday);
        Paycheck RecalculatePaycheck(Paycheck paycheck);
        Paycheck RecalculatePaycheck(Paycheck paycheck, List<Workday> workdays);
    }
}