using Microsoft.EntityFrameworkCore;
using PeoplePrtal.Data.Repository.Accounts;
using PeoplePrtal.Models;
using System;
using System.Collections;
using System.Text;

namespace PeoplePrtal.Data.Repository.Users
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
