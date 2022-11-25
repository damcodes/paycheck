using PaycheckBackend.Repositories.Interfaces;
using PaycheckBackend.Db;

namespace PaycheckBackend.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDbContext _repoContext;
        private IUserRepository _user;
        private IJobRepository _job;
        private IPaycheckRepository _paycheck;
        private IWorkdayRepository _workday;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }

        public IJobRepository Job
        {
            get
            {
                if (_job == null)
                {
                    _job = new JobRepository(_repoContext);
                }
                return _job;
            }
        }

        public IPaycheckRepository Paycheck
        {
            get
            {
                if (_paycheck == null)
                {
                    _paycheck = new PaycheckRepository(_repoContext);
                }
                return _paycheck;
            }
        }

        public IWorkdayRepository Workday
        {
            get
            {
                if (_workday == null)
                {
                    _workday = new WorkdayRepository(_repoContext);
                }
                return _workday;
            }
        }

        public RepositoryWrapper(AppDbContext appDbContext)
        {
            _repoContext = appDbContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}