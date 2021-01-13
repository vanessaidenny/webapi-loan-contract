using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiLoanContract.Models
{
    public class ContractPatchRequest
    {
        [Required(ErrorMessage = "Required field")]
        public int NumberInstallments { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Range(1, int.MaxValue, ErrorMessage = "The amount must be valid")]
        public decimal AmountFinanced { get; set; }
    
        public List<Installment> Installments { get; set; }
    }
}