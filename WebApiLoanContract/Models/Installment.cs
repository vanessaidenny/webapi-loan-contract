using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiLoanContract.Models
{
    public class Installment
    {        
        [ForeignKey("ContractId")]
        public int Contract { get; set; }

        [Display(Name = "Insert the expiration date")]
        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Date, ErrorMessage = "Insert a date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Insert the payment date")]
        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Date, ErrorMessage = "Insert a date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Range(1, int.MaxValue, ErrorMessage = "The amount must be valid")]
        public decimal Amount { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Required field")]
        public virtual StatusList Status { get; set; }
    }
}