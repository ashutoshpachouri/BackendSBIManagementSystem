using System.ComponentModel.DataAnnotations;

namespace BackendSBI.Models
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }
        [Required(ErrorMessage = "Please enter beneficiary Account Number")]
        public string PayeeAccount { get; set; }
        [Required(ErrorMessage = "Please enter your Account Number")]
        public string PayerAccount { get; set; }
        [Required(ErrorMessage = "Please enter Amount")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Please enter Date")]
        public DateTime TDate { get; set; }
        [Required(ErrorMessage = "Please enter Remark")]
        public string Remark { get; set; }
        public string Mode { get; set; }
    }
}
