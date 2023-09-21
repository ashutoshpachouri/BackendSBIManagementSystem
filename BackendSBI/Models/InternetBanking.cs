using System.ComponentModel.DataAnnotations;

namespace BackendSBI.Models
{
    public class InternetBanking
    {
        [Required]

        public string AccountNumber { get; set; }
        [Key]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6), MaxLength(10)]
        public string Password { get; set; }
    }
}
