using Microsoft.EntityFrameworkCore;
using PeoplePrtal.Data.Repository.Accounts;
using PeoplePrtal.Models;
using System;
using System.Collections;
using System.Text;

namespace PeoplePrtal.Data.Repository.Persons
{
    public interface IPersonsRepository : IRepository<Person>
    {
        Task<Person?> FindByIdNumberAsync(string IdNumber);
    }
    public class PersonsRepository : Repository<Person>, IPersonsRepository
    {
        private readonly IAccountsRepository _accountsRepository;
        public PersonsRepository(DataContext context, IAccountsRepository accountsRepository) : base(context)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<Person?> FindByIdNumberAsync(string IdNumber)
        {
            if (!String.IsNullOrEmpty(IdNumber))
            {
                return await _entities.Where(p => p.IdNumber == IdNumber).FirstOrDefaultAsync();
            }

            return null;
        }
    }
}
