using BackendSBI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendSBI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IConfiguration Config;
        private readonly AccountsDbContext _context;
        public RegisterController(IConfiguration Config, AccountsDbContext _context)
        {
            this.Config = Config;
            this._context = _context;
        }
        [AllowAnonymous]
        [HttpPost("RegisterAccount")]
        public IActionResult CreateUser(Accounts account)
        {
            if (_context.Account.Where(u => u.Email == account.Email).FirstOrDefault() != null)
            {
                return Ok("Email already exists");
            }
            _context.Account.Add(account);
            _context.SaveChanges();
            return Ok("Success");
        }
        [AllowAnonymous]
        [HttpPost("RegisterInternetBanking")]
        public IActionResult CreateInternetBanking(InternetBanking user)
        {
            if (_context.Account.Where(u => u.Email == user.Email).FirstOrDefault() == null)
            {
                return Ok("Email not matched");
            }
            _context.InternetBankings.Add(user);
            _context.SaveChanges();
            return Ok("Success");
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult LoginUser(Login user)
        {
            var UserDetails = _context.InternetBankings.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();
            if (UserDetails != null)
            {
                return Ok(new jwtService(Config).GenerateToken(UserDetails.Email, UserDetails.AccountNumber));
            }
            return Ok("Failure");
        }
       
    }
}
