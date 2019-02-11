using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetFinalProject.Enums;

namespace DotNetFinalProject.Models
{
    [Table("paymentterms")]
    public class PaymentTerm
    {
        [Key]
        [Required]
        public int PaymentTermSk {get;set;}

        [Display(Name = "Name:")]
        [Required]
        [StringLength(80)]
        public String Name {get;set;}
        
        [Display(Name = "Discount Days:")]
        [Range(0,999)]
        [Required]
        public int DiscountDays {get;set;}
        
        [Display(Name = "Due Days:")]
        [Range(0,999)]
        [Required]
        public int DueDays {get;set;}
        
        [Display(Name = "Discount Rate:")]
        [Range(0,99.99)]
        [Required]
        public decimal DiscountRate {get;set;}
        
        [Display(Name = "Discount Date Option:")]
        [Range(1,2)]
        [Required]
        public DiscountDateOption DiscountDateOption {get;set;}
        
        public DateTime CreatedAt {get;set;}

        [Required]
        public DateTime UpdatedAt {get;set;}
    }

}