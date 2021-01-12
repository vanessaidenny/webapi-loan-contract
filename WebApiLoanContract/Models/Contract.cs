using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiLoanContract.Models
{
    public class Contract
    {
        [Key]
        public int ContractId { get; private set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Date, ErrorMessage = "Insert a date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ContractDate { get; set; }

        [Required(ErrorMessage = "Required field")]
        public int NumberInstallments { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Range(1, int.MaxValue, ErrorMessage = "The amount must be valid")]
        public decimal AmountFinanced { get; set; }
    
        public List<Installment> Installments { get; set; }
    }
}