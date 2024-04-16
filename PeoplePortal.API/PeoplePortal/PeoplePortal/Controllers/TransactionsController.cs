using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeoplePrtal.Models;
using PeoplePrtal.Models.ViewModel;
using PeoplePrtal.Services;
using System.Security.Principal;

namespace PeoplePrtal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "BasicAuthentication")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;
        private readonly IAccountsService _accountsService;
        public TransactionsController(ITransactionsService transactionsService, IAccountsService accountsService)
        {
            _transactionsService = transactionsService;
            _accountsService = accountsService;
        }

        [HttpGet("id/{code}")]
        public async Task<ActionResult<List<Transaction>>> GetAccountTransactions(int code)
        {
            return Ok(await _transactionsService.GetAccountTransactions(code));
        }

        [HttpGet("account/{accountCode}")]
        public async Task<ActionResult<Transaction>> GetTransactionByCode(int accountCode)
        {
            try
            {
                var transaction = await _transactionsService.FindTransactionByCode(accountCode);

                if (transaction == null)
                    return NotFound();
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Transaction>>> CreateTransaction([FromBody]TransactionVM transaction)
        {
            try
            {
                if (transaction.Amount <= 0)
                    return BadRequest("Amount for transaction cannot be zero.");

                var account = await _accountsService.FindByAccountCode(transaction.AccountCode);
                if (account == null)
                    return BadRequest("Transaction account does not exist.");

                var newTransaction = new Transaction
                {
                    AccountCode = transaction.AccountCode,
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    Type = transaction.Type,
                    TransactionDate = DateTime.Parse(transaction.TransactionDate),
                    CaptureDate = DateTime.UtcNow,
                };

                await _transactionsService.CreateTransaction(newTransaction);

                await _accountsService.TransactAccount(account.AccountNumber, transaction.Type, transaction.Amount);

                return Ok(await _transactionsService.GetAccountTransactions(account.Code));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Person>>> UpdateTransaction(TransactionVM transaction)
        {
            try
            {
                var dbTransaction = await _transactionsService.FindTransactionByCode(transaction.Code);
                if (dbTransaction == null)
                    return BadRequest("Transaction does not exist.");
                                
                dbTransaction.TransactionDate = DateTime.Now;
                dbTransaction.Amount = transaction.Amount;
                dbTransaction.Description = transaction.Description;

                await _transactionsService.UpdateTransactionDetails(dbTransaction);

                var account = await _accountsService.FindByAccountCode(transaction.AccountCode);

                _accountsService.UpdateAccountTransaction(account.AccountNumber, dbTransaction.Type, transaction.Type, dbTransaction.Amount, transaction.Amount);

                return Ok(await _transactionsService.GetAccountTransactions(dbTransaction.AccountCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
