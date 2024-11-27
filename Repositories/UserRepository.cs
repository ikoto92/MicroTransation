using MicroTransation.Data;
using MicroTransation.Models;

namespace MicroTransation.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public User GetUser(int id)
        {
            return _appDbContext.Users.FirstOrDefault(user => user.Id == id);
        }

        public List<User> GetAll()
        {
            return _appDbContext.Users.ToList();
        }
        public User Create(User user)
        {
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            return user;
        }

        public void Delete(int id)
        {
            User user = _appDbContext.Users.FirstOrDefault(user => user.Id == id);
            _appDbContext.Users.Remove(user);
        }

    }
}

