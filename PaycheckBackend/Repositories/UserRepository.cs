using PaycheckBackend.Models;
using PaycheckBackend.Models.Dto;
using PaycheckBackend.Repositories.Interfaces;
using PaycheckBackend.Db;
using Microsoft.EntityFrameworkCore;

namespace PaycheckBackend.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        { }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll()
                .OrderBy(u => u.FirstName)
                .ToList();
        }

        public User? GetUserById(int id)
        {
            return FindByCondition(u => u.UserId.Equals(id)).FirstOrDefault();
        }

        public User? GetUserByEmail(string email)
        {
            return FindByCondition(u => u.Email.Equals(email)).FirstOrDefault();
        }

        public User? GetUserByIdWithJobs(int id)
        {
            return FindByCondition(u => u.UserId.Equals(id))
                .Include(u => u.Jobs)
                .FirstOrDefault();
        }

        public User? GetUserByIdWithPaychecks(int id)
        {
            return FindByCondition(u => u.UserId.Equals(id))
                .Include(u => u.Paychecks)
                .FirstOrDefault();
        }

        public User? GetUserByIdWithWorkdays(int id)
        {
            return FindByCondition(u => u.UserId.Equals(id))
                .Include(u => u.Workdays)
                .FirstOrDefault();
        }

        public User? GetUserByIdAllDetails(int id)
        {
            return FindByCondition(u => u.UserId.Equals(id))
                .Include(u => u.Workdays)
                .Include(u => u.Jobs)
                .Include(u => u.Paychecks)
                .FirstOrDefault();
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public User PatchUser(User user)
        {
            Update(user);
            return user;
        }
    }
}