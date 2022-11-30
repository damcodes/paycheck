using PaycheckBackend.Models;
using PaycheckBackend.Models.Dto;

namespace PaycheckBackend.Repositories.Interfaces
{
    public interface IUserRepository 
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserById(int id);
        User? GetUserByEmail(string email);
        User? GetUserByIdWithJobs(int id);
        User? GetUserByIdWithPaychecks(int id);
        User? GetUserByIdWithWorkdays(int id);
        void CreateUser(User user);
        User PatchUser(User user);
    }
}