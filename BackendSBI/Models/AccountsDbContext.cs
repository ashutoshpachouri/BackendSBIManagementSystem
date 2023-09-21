using Microsoft.EntityFrameworkCore;

namespace BackendSBI.Models
{
    public class AccountsDbContext: DbContext
    {
        public AccountsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Accounts> Account { get; set; }
        public DbSet<InternetBanking> InternetBankings { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
