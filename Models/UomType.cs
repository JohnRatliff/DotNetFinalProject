using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetFinalProject.Enums;

namespace DotNetFinalProject.Models
{
    [Table("uomtypes")]
    public class UomType
    {
        [Key]
        [Required]
        public int UomTypeSk {get;set;}

        [Display(Name = "Uom Type:")]
        [Required]
        [StringLength(10)]
        public string UomTypeName {get;set;}
        
        public DateTime CreatedAt {get;set;}

        [Required]
        public DateTime UpdatedAt {get;set;}
    }

}