using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetFinalProject.Models
{
    public class Login
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address:")]
        [NotMapped]
        [Required]
        [StringLength(200)]
        public string LoginEmail {get;set;}
        
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        [MinLength(8, ErrorMessage="Password must be at least 8 characters long")]
        [NotMapped]
        [Required]
        [StringLength(400)]
        public string LoginPassword {get;set;}
    }
}