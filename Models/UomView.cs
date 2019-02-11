using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetFinalProject.Enums;

namespace DotNetFinalProject.Models
{
    [Table("DVW_Uom")]
    public class UomView
    {
        [Key]
        [Required]
        public int UomSk {get;set;}
        
        [Display(Name = "Uom Type:")]
        [Required]
        [StringLength(10)]
        public string UomTypeName {get;set;}

        [Required]
        public int UomType {get;set;}
        
        [Display(Name = "Uom Id:")]
        [Required]
        [StringLength(10)]
        public string UomId {get;set;}
        
        [Display(Name = "Uom Name:")]
        [Required]
        [StringLength(20)]
        public string UomName {get;set;}
        
        public DateTime CreatedAt {get;set;}

        [Required]
        public DateTime UpdatedAt {get;set;}

        [Display(Name = "From Conversion Count:")]
        public int FromConversionCount {get;set;}

        [Display(Name = "To Conversion Count:")]
        public int ToConversionCount {get;set;}
    }

}