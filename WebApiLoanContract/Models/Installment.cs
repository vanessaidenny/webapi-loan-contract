using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApiLoanContract.Models
{
    public class Installment
    {
        [Key]
        public int InstallmentId { get; private set; }
                
        [JsonIgnore]
        public Contract Contract { get; set; }
        
        [ForeignKey("ContractId")]        
        [Required(ErrorMessage = "Required field")]        
        public int ContractId { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Date, ErrorMessage = "Insert a date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Date, ErrorMessage = "Insert a date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Range(1, int.MaxValue, ErrorMessage = "The amount must be valid")]
        public decimal Amount { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Required field")]
        public string Status { get; set; }
    }
}