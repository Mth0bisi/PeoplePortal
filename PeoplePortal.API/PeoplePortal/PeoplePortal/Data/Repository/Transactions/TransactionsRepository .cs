using Microsoft.EntityFrameworkCore;

using PeoplePrtal.Models;

namespace PeoplePrtal.Data.Repository.Transactions
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<List<Transaction>> GetAccountTransactions(int accountCode);
    }
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Transaction>> GetAccountTransactions(int accountCode)
        {
            var transactions = await GetAll();
            return transactions.Where(t => t.AccountCode == accountCode).ToList();
        }
    }
}
