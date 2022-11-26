using PaycheckBackend.Models;

namespace PaycheckBackend.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserById(int id);
        User? GetUserByIdWithJobs(int id);
        void CreateUser(User user);
    }
}