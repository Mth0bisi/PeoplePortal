using Microsoft.EntityFrameworkCore;
using PeoplePrtal.Models;
using PeoplePrtal.Models.ViewModel;

namespace PeoplePrtal.Data.Repository.Accounts
{
    public interface IAccountsRepository : IRepository<Account>
    {
        Task<List<AccountVM>> FindPersonAccounts(int personCode);
        Task<AccountVM?> FindByAccountNumberAsync(string accountNumber);
        void UpdateAccountTransaction(string accountNumber, string oldTransactionType, string newTransactionType, decimal oldAmount, decimal newAmount);
        Task TransactAccount(string accountNumber, string transactionType, decimal amount);
        Task UpdateAccountStatus(int accountCode, string status);
        Task CreateAccount(Account account);
        Task UpdateAccount(AccountVM account);
        Task<string> GetAccountStatus(int accountCode);
    }
    public class AccountsRepository : Repository<Account>, IAccountsRepository
    {
        public AccountsRepository(DataContext context) : base(context)
        {
        }

        public async Task CreateAccount(Account account)
        {
            await Create(account);

            var dbAccount = await FindByAccountNumberAsync(account.AccountNumber);
            var accountStatus = new Status
            {
                AccountCode = dbAccount.Code,
                StatusType = "Open"
            };

             _context.Status.Add(accountStatus);
        }

        public async Task<AccountVM?> FindByAccountNumberAsync(string accountNumber)
        {
            var accounts = await GetAll();
            var account = accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (account != null) 
                return new AccountVM
                {
                    AccountNumber = account.AccountNumber,
                    PersonCode = account.PersonCode,
                    AccountStatus = await GetAccountStatus(account.Code),
                    Code = account.Code,
                    OutstandingBalance = account.OutstandingBalance

                };

            return null;
        }

        public async Task<List<AccountVM>> FindPersonAccounts(int personCode)
        {
            var accounts = await GetAll();
            var accountsVM = new List<AccountVM>();
            accounts = accounts.Where(a => a.PersonCode == personCode).ToList();

            foreach (var account in accounts) 
            {
                accountsVM.Add(new AccountVM
                {
                    AccountNumber = account.AccountNumber,
                    PersonCode = personCode,
                    AccountStatus = await GetAccountStatus(account.Code),
                    Code = account.Code,
                    OutstandingBalance = account.OutstandingBalance
                });
            }
            return accountsVM;
        }

        public async Task<string> GetAccountStatus(int accountCode) 
            => await _context.Status.Where(s => s.AccountCode == accountCode).Select(a => a.StatusType).FirstOrDefaultAsync();

        public async Task TransactAccount(string accountNumber, string transactionType, decimal amount)
        {
            var accountVM = await FindByAccountNumberAsync(accountNumber);
            var account = await GetByCode(accountVM.Code);
            if (account != null)
            {
                if (transactionType.ToLower().Equals("debit"))
                    account.OutstandingBalance = account.OutstandingBalance + amount;
                else
                    account.OutstandingBalance = account.OutstandingBalance - amount;

                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAccount(AccountVM account)
        {
            var dbAccount = await GetByCode(account.Code);

            dbAccount.AccountNumber = account.AccountNumber;
            Update(dbAccount);
        }

        public async Task UpdateAccountStatus(int accountCode, string status)
        {
            var sccountStatus = await _context.Status.FirstOrDefaultAsync(s => s.AccountCode == accountCode);
            if (sccountStatus != null)
            {
                sccountStatus.StatusType = status;
                _context.Status.Update(sccountStatus);
                await _context.SaveChangesAsync();
            }
        }

        public async void UpdateAccountTransaction(string accountNumber, string oldTransactionType, string newTransactionType, decimal oldAmount, decimal newAmount)
        {
            var account = await FindByAccountNumberAsync(accountNumber);
            if (account != null)
            {
                if (oldAmount != newAmount && oldTransactionType == newTransactionType)
                {
                    if (oldTransactionType.ToLower().Equals("debit"))
                    {
                        account.OutstandingBalance = account.OutstandingBalance - oldAmount;
                        account.OutstandingBalance = account.OutstandingBalance + newAmount;
                    }

                    else
                    {
                        account.OutstandingBalance = account.OutstandingBalance + oldAmount;
                        account.OutstandingBalance = account.OutstandingBalance - newAmount;
                    }
                }
                else if (oldAmount == newAmount && oldTransactionType != newTransactionType)
                {
                    if (oldTransactionType.ToLower().Equals("debit"))
                    {
                        account.OutstandingBalance = account.OutstandingBalance - oldAmount;
                        account.OutstandingBalance = account.OutstandingBalance - oldAmount;
                    }

                    else
                    {
                        account.OutstandingBalance = account.OutstandingBalance + oldAmount;
                        account.OutstandingBalance = account.OutstandingBalance + oldAmount;
                    }
                }
                else if (oldAmount != newAmount && oldTransactionType != newTransactionType)
                {
                    if (oldTransactionType.ToLower().Equals("debit"))
                    {
                        account.OutstandingBalance = account.OutstandingBalance - oldAmount;
                        account.OutstandingBalance = account.OutstandingBalance - newAmount;
                    }

                    else
                    {
                        account.OutstandingBalance = account.OutstandingBalance + oldAmount;
                        account.OutstandingBalance = account.OutstandingBalance + newAmount;
                    }
                }
            }


           
        }
    }
}
