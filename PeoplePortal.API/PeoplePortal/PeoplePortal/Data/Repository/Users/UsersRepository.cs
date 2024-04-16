using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data.Repository.Accounts;
using SuperHeroAPI.Models;
using System;
using System.Collections;
using System.Text;

namespace SuperHeroAPI.Data.Repository.Users
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User> GetUser(string username);
    }
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(DataContext context) : base(context)
        {
        }

        public async Task<User> GetUser(string username)
        {
            var users = await GetAll();
            var user = users.FirstOrDefault(x => x.Username == username);
            if (user == null)
                return new User();
            else
                return user;
        }
    }
}
