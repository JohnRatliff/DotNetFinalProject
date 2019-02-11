using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetFinalProject.Enums;

namespace DotNetFinalProject.Models
{
    [Table("contacts")]
    public class Contact
    {
        [Key]
        [Required]
        public int ContactSk {get;set;}

        [Display(Name = "Name:")]
        [Required]
        [StringLength(80)]
        public String Name {get;set;}
        
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address:")]
        [Required]
        [StringLength(200)]
        public string Email {get;set;}
        
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:###.###.####}")]
        [Display(Name = "Phone:")]
        [StringLength(20)]
        public string Phone {get;set;}
        
        [Display(Name = "Address Line 1:")]
        [Required]
        [StringLength(100)]
        public string AddressLine1 {get;set;}
        
        [Display(Name = "Address Line 2:")]
        [StringLength(100)]
        public string AddressLine2 {get;set;}
        
        [Display(Name = "City:")]
        [Required]
        [StringLength(40)]
        public string City {get;set;}
        
        [Display(Name = "State:")]
        [Required]
        [StringLength(40)]
        public string State {get;set;}
        
        [Display(Name = "Zip:")]
        [Required]
        [StringLength(20)]
        public string Zip {get;set;}
        
        public DateTime CreatedAt {get;set;}

        [Required]
        public DateTime UpdatedAt {get;set;}

        List<UomConversion> UomConversions {get;set;}
    }

}