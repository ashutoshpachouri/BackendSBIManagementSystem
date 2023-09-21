using System.ComponentModel.DataAnnotations;

namespace BackendSBI.Models
{
    public class Beneficiary
    {
        [Required(ErrorMessage = "Please enter the Name")]
        public string Name { get; set; }
        [Key]
        [Required(ErrorMessage = "Please enter AccountNumber")]
        public string AccountNumber { get; set; }
    }
}
