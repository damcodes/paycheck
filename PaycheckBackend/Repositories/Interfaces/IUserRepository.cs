using PaycheckBackend.Models;

namespace PaycheckBackend.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IEnumerable<User> GetAllUsers();
    }
}