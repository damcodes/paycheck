

namespace PaycheckBackend.Repositories.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IJobRepository Job { get; }
        IPaycheckRepository Paycheck { get; }
        IWorkdayRepository Workday { get; }
    }
}