using PeoplePrtal.Data.Repository.Accounts;
using PeoplePrtal.Data.Repository.Persons;
using PeoplePrtal.Models;
using PeoplePrtal.Models.ViewModel;
using System.Security.Principal;

namespace PeoplePrtal.Services
{
    public interface IAccountsService
    {
        Task<List<AccountVM>> GetPersonAccounts(int personCode);
        Task<AccountVM?> FindByAccountNumber(string accountNumber);
        Task<Account> FindByAccountCode(int accountCode);
        Task CreateAccount(Account account);
        bool CheckPersonAccountsClosedStatus(List<AccountVM> accounts);
        Task UpdateAccountDetails(AccountVM account);
        void UpdateAccountTransaction(string accountNumber, string oldTransactionType, string newTransactionType, decimal oldAmount, decimal newAmount);
        Task TransactAccount(string accountNumber, string transactionType, decimal amount);
    }
    public class AccountsService: IAccountsService
    {
        private readonly IAccountsRepository _accountsRepository;
        public AccountsService(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task CreateAccount(Account account)
        {
            await _accountsRepository.CreateAccount(account);
            await _accountsRepository.Save();
        }

        public async Task<Account> FindByAccountCode(int accountCode)
        => await _accountsRepository.GetByCode(accountCode);

        public async Task<AccountVM?> FindByAccountNumber(string accountNumber)
        => await _accountsRepository.FindByAccountNumberAsync(accountNumber);


        public async Task<List<AccountVM>> GetPersonAccounts(int personCode)
        => await _accountsRepository.FindPersonAccounts(personCode);

        public async Task TransactAccount(string accountNumber, string transactionType, decimal amount)
        {
            await _accountsRepository.TransactAccount(accountNumber, transactionType, amount);
        }

        public async Task UpdateAccountDetails(AccountVM account)
        {
            await _accountsRepository.UpdateAccount(account);
            await _accountsRepository.Save();

            var accountStatus = await _accountsRepository.GetAccountStatus(account.Code);

            if (account.AccountStatus != accountStatus)
                await UpdateAccountStatus(account.Code, account.AccountStatus);
        }

        private async Task UpdateAccountStatus(int accountCode, string status)
        {
            await _accountsRepository.UpdateAccountStatus(accountCode, status);
        }

        public async void UpdateAccountTransaction(string accountNumber, string oldTransactionType, string newTransactionType, decimal oldAmount, decimal newAmount)
        {
            _accountsRepository.UpdateAccountTransaction(accountNumber, oldTransactionType, newTransactionType, oldAmount, newAmount);
            await _accountsRepository.Save();
        }

        public bool CheckPersonAccountsClosedStatus(List<AccountVM> accounts)
        {
            foreach (var account in accounts)
            {
                if (account.OutstandingBalance != 0)
                    return false;
            }

            return true;
        }
    }
}
