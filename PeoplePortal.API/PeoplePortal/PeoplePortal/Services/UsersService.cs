using PeoplePrtal.Data.Repository.Users;
using PeoplePrtal.Models;

namespace PeoplePrtal.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }
    public class UsersService: IUserService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user =  await _usersRepository.GetUser(username);
            if (user == null)
                return new User();
            return user;

        }
        
    }
}
