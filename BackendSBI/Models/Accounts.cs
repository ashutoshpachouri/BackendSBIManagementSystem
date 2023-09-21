using System.ComponentModel.DataAnnotations;

namespace BackendSBI.Models
{
    public class Accounts
    {
        [Key]
        [Required]
        public string FullName { get; set; }
        [Required]
        public string FathersName { get; set; }
        [Required]
        [MaxLength(10), MinLength(10)]
        public string MobileNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(12), MinLength(12)]
        public string AadharNumber { get; set; }
        [Required]
        public string DOB { get; set; }
        [Required]
        public string RAddress { get; set; }
        [Required]
        public string PAddress { get; set; }
        [Required]
        public string RState { get; set; }
        [Required]
        public string PState { get; set; }
        [Required]
        public string RCity { get; set; }
        [Required]
        public string PCity { get; set; }
        [Required]
        public string RPincode { get; set; }
        [Required]
        public string PPincode { get; set; }
        [Required]
        public string OccupationType { get; set; }
        [Required]
        public string Income { get; set; }
        [Required]
        public string AnnualIncome { get; set; }
    }
}
