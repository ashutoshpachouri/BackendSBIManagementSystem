using BackendSBI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace BackendSBI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountsDbContext _context;

        public AccountRepository(AccountsDbContext context)
        {
            _context = context;
        }
        public async Task<InternetBanking> GetInternetBankingByUserEmailAsync(string email)
        {
            return await _context.InternetBankings.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<InternetBanking> GetInternetBankingByAccountNumberAsync(string accountNumber)
        {
            return await _context.InternetBankings.FirstOrDefaultAsync(u => u.AccountNumber == accountNumber);
        }

        public async Task<Accounts> GetAccountByUserEmailAsync(string email)
        {
            return await _context.Account.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Beneficiary> GetBeneficiaryByIdAsync(string accountNumber)
        {
            return await _context.Beneficiaries.FirstOrDefaultAsync(b => b.AccountNumber == accountNumber);
        }

        public async Task<Transaction> GetTransactionByIdAsync(Guid id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(t => t.TransactionId == id);
        }
        public async Task<List<Transaction>> GetTransactionsForUserAsync(string userAccountNumber)
        {
            return await _context.Transactions
                .Where(t => t.PayerAccount == userAccountNumber || t.PayeeAccount == userAccountNumber)
                .ToListAsync();
        }


        public async Task AddBeneficiaryAsync(Beneficiary beneficiary)
        {
            _context.Beneficiaries.Add(beneficiary);
            await _context.SaveChangesAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePasswordAsync(InternetBanking user, string newPassword)
        {
            // Update user's password logic here
            user.Password = newPassword;
            await _context.SaveChangesAsync();
        }

        // Implement other repository methods as needed
        public async Task<object> GetUserDataAsync(string email)
        {
            var internetBankingDetails = await _context.InternetBankings.FirstOrDefaultAsync(u => u.Email == email);
            var accountDetails = await _context.Account.FirstOrDefaultAsync(u => u.Email == email);

            if (internetBankingDetails != null || accountDetails != null)
            {
                return new
                {
                    AccountDetails = accountDetails,
                    InternetBankingDetails = internetBankingDetails
                };
            }
            else
            {
                return null;
            }
        }
    }
}
