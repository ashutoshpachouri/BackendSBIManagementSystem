using BackendSBI.Models;
using BackendSBI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BackendSBI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly AccountsDbContext _context;
        private readonly IAccountRepository _accountRepository;
        public DashboardController(AccountsDbContext context, IAccountRepository accountRepository)
        {
            _context = context;
            _accountRepository = accountRepository;

        }
        [HttpGet("GetUserDetails")]
        public async Task<IActionResult> GetDashboardDetails()
        {
            try
            {
                // Retrieve the current user's email from the claims
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;

                // Query the repository to get Internet banking details for the user
                var internetBankingDetails = await _accountRepository.GetInternetBankingByUserEmailAsync(userEmail);
                var accountDetails = await _accountRepository.GetAccountByUserEmailAsync(userEmail);

                if (accountDetails != null && internetBankingDetails != null)
                {
                    var dashboardData = new
                    {
                        AccountDetails = accountDetails,
                        InternetBankingDetails = internetBankingDetails
                    };
                    return Ok(dashboardData);
                }

                if (internetBankingDetails == null)
                {
                    return NotFound("Internet banking details not found for the user.");
                }

                return Ok(internetBankingDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving dashboard data.");
            }
        }
        /**/
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                var user = await _accountRepository.GetInternetBankingByUserEmailAsync(userEmail);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                if (model.OldPassword != user.Password)
                {
                    return BadRequest("Old password is incorrect.");
                }

                await _accountRepository.UpdatePasswordAsync(user, model.NewPassword);

                return Ok("Password changed successfully.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during password change
                return StatusCode(500, "Error changing password.");
            }
        }
        [HttpPost("SaveBeneficiary")]
        public IActionResult AddBeneficiary(Beneficiary beneficiary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _accountRepository.AddBeneficiaryAsync(beneficiary);
                return Ok("Beneficiary saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving beneficiary.");
            }
        }

        /**/
        [HttpPost("RTGS")]
        public async Task<IActionResult> RTGSTransactionAsync(Transaction trans)
        {
            try
            {
                // Validate the model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if payer and payee accounts exist
                var payerAccount = await _accountRepository.GetInternetBankingByAccountNumberAsync(trans.PayerAccount);
                var payeeAccount = await _accountRepository.GetBeneficiaryByIdAsync(trans.PayeeAccount);

                if (payerAccount == null)
                {
                    return BadRequest("Payer account not found.");
                }

                if (payeeAccount == null)
                {
                    return BadRequest("Payee account not found.");
                }

                var transaction = new Transaction
                {
                    TransactionId = Guid.NewGuid(),
                    PayeeAccount = payeeAccount.AccountNumber,
                    PayerAccount = payerAccount.AccountNumber,
                    Amount = trans.Amount,
                    TDate = trans.TDate,
                    Remark = trans.Remark,
                    Mode = "RTGS"
                };

                await _accountRepository.AddTransactionAsync(transaction);

                // Implement your RTGS transaction logic here, including updating balances, recording the transaction, etc.

                return Ok("RTGS transaction successful.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the transaction
                return StatusCode(500, "Error performing RTGS transaction.");
            }
        }


        [HttpPost("IMPS")]
        public async Task<IActionResult> IMPSTransactionAsync(Transaction trans)
        {
            try
            {
                // Validate the model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if payer and payee accounts exist
                var payerAccount = await _accountRepository.GetInternetBankingByAccountNumberAsync(trans.PayerAccount);
                var payeeAccount = await _accountRepository.GetBeneficiaryByIdAsync(trans.PayeeAccount);

                if (payerAccount == null)
                {
                    return BadRequest("Payer account not found.");
                }

                if (payeeAccount == null)
                {
                    return BadRequest("Payee account not found.");
                }

                var transaction = new Transaction
                {
                    TransactionId = Guid.NewGuid(),
                    PayeeAccount = payeeAccount.AccountNumber,
                    PayerAccount = payerAccount.AccountNumber,
                    Amount = trans.Amount,
                    TDate = trans.TDate,
                    Remark = trans.Remark,
                    Mode = "IMPS"
                };

                await _accountRepository.AddTransactionAsync(transaction);

            

                return Ok("IMPS transaction successful.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the transaction
                return StatusCode(500, "Error performing IMPS transaction.");
            }
        }
        [HttpPost("NEFT")]
        public async Task<IActionResult> NEFTTransactionAsync(Transaction trans)
        {
            try
            {
                // Validate the model
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Check if payer and payee accounts exist
                var payerAccount = await _accountRepository.GetInternetBankingByAccountNumberAsync(trans.PayerAccount);
                var payeeAccount = await _accountRepository.GetBeneficiaryByIdAsync(trans.PayeeAccount);

                if (payerAccount == null)
                {
                    return BadRequest("Payer account not found.");
                }

                if (payeeAccount == null)
                {
                    return BadRequest("Payee account not found.");
                }

                var transaction = new Transaction
                {
                    TransactionId = Guid.NewGuid(),
                    PayeeAccount = payeeAccount.AccountNumber,
                    PayerAccount = payerAccount.AccountNumber,
                    Amount = trans.Amount,
                    TDate = trans.TDate,
                    Remark = trans.Remark,
                    Mode = "NEFT"
                };

                await _accountRepository.AddTransactionAsync(transaction);

               

                return Ok("NEFT transaction successful.");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the transaction
                return StatusCode(500, "Error performing NEFT transaction.");
            }
        }
        [HttpGet("GetMyTransactions")]
        public async Task<IActionResult> GetMyTransactions()
        {
            try
            {
                // Retrieve the current user's email and account number from the claims
                var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                var userAccountNumber = User.FindFirst("acnumber")?.Value; // Retrieve the acnumber claim

                if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userAccountNumber))
                {
                    return BadRequest("User email or account number not found in claims.");
                }

                // Query the repository to retrieve transactions for the user's account number
                var transactions = await _accountRepository.GetTransactionsForUserAsync(userAccountNumber);

                if (transactions.Count == 0)
                {
                    return NotFound("No transactions found for the user.");
                }

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the retrieval
                return StatusCode(500, "Error retrieving transactions.");
            }
        }
    }
}






























