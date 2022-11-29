using PaycheckBackend.Models;

namespace PaycheckBackend.Repositories.Interfaces
{
    public interface IUserRepository 
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserById(int id);
        User? GetUserByIdWithJobs(int id);
        User? GetUserByIdWithPaychecks(int id);
        User? GetUserByIdWithWorkdays(int id);
        void CreateUser(User user);
        User PatchUser(User user);
    }
}