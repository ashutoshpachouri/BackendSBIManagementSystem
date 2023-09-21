using System.ComponentModel.DataAnnotations;

namespace BackendSBI.Models
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(10), MinLength(6)]
        public string Password { get; set; }
    }
}
