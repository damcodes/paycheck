using PaycheckBackend.Models;
using PaycheckBackend.Repositories.Interfaces;
using PaycheckBackend.Db;
using Microsoft.EntityFrameworkCore;
using PaycheckBackend.Logger;

namespace PaycheckBackend.Repositories
{
    public class PaycheckRepository : BaseRepository<Paycheck>, IPaycheckRepository
    {
        public PaycheckRepository(AppDbContext appDbContext) : base(appDbContext)
        { }

        public Paycheck? GetPaycheckById(int id)
        {
            return FindByCondition(p => p.PaycheckId.Equals(id))
                .Include(p => p.Job)
                .FirstOrDefault();
        }

        public void CreatePaycheck(Paycheck paycheck, Job job)
        {
            DateTime startDate = DateTime.SpecifyKind(paycheck.StartDate, DateTimeKind.Utc);
            DateTime endDate = paycheck.StartDate + job.PayPeriodLength - new TimeSpan(1, 0, 0, 0);
            endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

            paycheck.StartDate = startDate;
            paycheck.EndDate = endDate;

            Create(paycheck);
        }

        public void CalculateAndAdjustPaycheckAmount(Workday workday, Paycheck paycheck, Job job)
        {
            double hoursWorked = (workday.TimeOut - workday.TimeIn).TotalHours;
            double workdayWages = job.PayRate * hoursWorked;
            if (workday.Tips != null && workday.Tips > 0)
            {
                workdayWages += (double)workday.Tips;
            }
            paycheck.Amount += workdayWages;
            Update(paycheck);
        }
    }
}