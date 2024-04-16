using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeoplePrtal.Models;
using PeoplePrtal.Models.ViewModel;
using PeoplePrtal.Services;

namespace PeoplePrtal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpGet("id/{code}")]
        public async Task<ActionResult<List<AccountVM>>> GetPersonAccounts(int code)
        {
            return Ok(await _accountsService.GetPersonAccounts(code));
        }

        [HttpGet("account/{accountNumber}")]
        public async Task<ActionResult<AccountVM?>> GetByAccountNumber(string accountNumber)
        {
            try 
            {
                var account = await _accountsService.FindByAccountNumber(accountNumber);

                if(account == null) 
                    return NotFound();
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<AccountVM>>> CreateAccount(Account account)
        {
            try
            {
                var dbAccount = await _accountsService.FindByAccountNumber(account.AccountNumber);
                if (dbAccount != null)
                    return BadRequest("Account already exists.");

                await _accountsService.CreateAccount(account);

                return Ok(await _accountsService.GetPersonAccounts(account.PersonCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<AccountVM>>> UpdateAccount(AccountVM account)
        {
            try
            {
                var dbAccount = await _accountsService.FindByAccountNumber(account.AccountNumber);
                if (dbAccount == null)
                    return BadRequest("Account does not exist.");

                if (account.AccountStatus.ToLower().Equals("closed"))
                {
                    if(dbAccount.OutstandingBalance != 0)
                        return BadRequest("Error closing account, outstanding amount must be 0.");
                }

                await _accountsService.UpdateAccountDetails(account);

                return Ok(await _accountsService.GetPersonAccounts(account.PersonCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
