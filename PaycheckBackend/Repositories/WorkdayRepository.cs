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

        public void CreateWorkday(Workday workday, Job job)
        {
            if (workday.TimeOut > workday.TimeIn)
            {
                double hoursWorked = (workday.TimeOut - workday.TimeIn).TotalHours;
                double workdayWages = job.PayRate * hoursWorked;
                if (workday.Tips != null && workday.Tips > 0)
                {
                    workdayWages += (double)workday.Tips;
                }
                workday.WagesEarned = workdayWages;
                workday.TimeIn = DateTime.SpecifyKind(workday.TimeIn, DateTimeKind.Utc);
                workday.TimeOut = DateTime.SpecifyKind(workday.TimeOut, DateTimeKind.Utc);

                Create(workday);
            }
            else
            {
                throw new Exception("Time out must be after time in");
            }
            
        }

        public void PatchWorkday(Workday workday, Job job)
        {
            if (workday.TimeOut > workday.TimeIn)
            {
                workday.TimeIn = DateTime.SpecifyKind(workday.TimeIn, DateTimeKind.Utc);
                workday.TimeOut = DateTime.SpecifyKind(workday.TimeOut, DateTimeKind.Utc);
                RecalculateWagesEarned(workday, job);
                Update(workday);
            }
            else
            {
                throw new Exception("Time out must be after time in");
            }
        }

        public void RecalculateWagesEarned(List<Workday> workdays, Job job)
        {
            foreach (Workday w in workdays)
            {
                if (w.TimeOut > w.TimeIn)
                {
                    double hoursWorked = (w.TimeOut - w.TimeIn).TotalHours;
                    double workdayWages = job.PayRate * hoursWorked;
                    if (w.Tips != null && w.Tips > 0)
                    {
                        workdayWages += (double)w.Tips;
                    }

                    w.WagesEarned = workdayWages;
                    Update(w);
                }
                else
                {
                    throw new Exception("Time out must be after time in");
                }
                
            }
        }

        public void RecalculateWagesEarned(Workday workday, Job job)
        {
            if (workday.TimeOut > workday.TimeIn)
            {
                double hoursWorked = (workday.TimeOut - workday.TimeIn).TotalHours;
                double workdayWages = job.PayRate * hoursWorked;
                if (workday.Tips != null && workday.Tips > 0)
                {
                    workdayWages += (double)workday.Tips;
                }
                workday.WagesEarned = workdayWages;
            }
            else
            {
                throw new Exception("Time out must be after time in");
            }
        }
    }
}