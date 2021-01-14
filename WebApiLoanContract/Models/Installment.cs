using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApiLoanContract.Models
{
    public class Installment
    {
        [Key]
        public int InstallmentId { get; set; }
                
        [JsonIgnore]
        public Contract Contract { get; set; }
        
        [ForeignKey("ContractId")]        
        [Required(ErrorMessage = "Required field")]        
        public int ContractId { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime? PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }
    }
}