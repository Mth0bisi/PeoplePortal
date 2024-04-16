using SuperHeroAPI.Data.Repository.Transactions;
using SuperHeroAPI.Models;

namespace SuperHeroAPI.Services
{
    public interface ITransactionsService
    {
        Task<List<Transaction>> GetAccountTransactions(int accountCode);
        Task CreateTransaction(Transaction transaction);
        Task UpdateTransactionDetails(Transaction transaction);
        Task<Transaction> FindTransactionByCode(int transactionCode);
    }
    public class TransactionsService: ITransactionsService
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionsService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task CreateTransaction(Transaction transaction)
        {
            await _transactionRepository.Create(transaction);
            await _transactionRepository.Save();
        }

        public async Task<Transaction> FindTransactionByCode(int transactionCode)
        => await _transactionRepository.GetByCode(transactionCode);

        public async Task<List<Transaction>> GetAccountTransactions(int accountCode)
        => await _transactionRepository.GetAccountTransactions(accountCode);

        public async Task UpdateTransactionDetails(Transaction transaction)
        {
            _transactionRepository.Update(transaction);
            await _transactionRepository.Save();
        }
    }
}
