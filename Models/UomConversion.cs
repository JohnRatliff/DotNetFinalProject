using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetFinalProject.Enums;

namespace DotNetFinalProject.Models
{
    [Table("uomconversions")]
    public class UomConversion
    {
        #region Properties

        [Key]
        [Required]
        public int UomConversionSk {get;set;}

        [Display(Name = "Uom Type:")]
        [ForeignKey("UomType")]
        [Required]
        public int UomTypeSk {get;set;}

        [Display(Name = "Convert From Uom:")]
        [ForeignKey("ConvertFromUom")]
        [Required]
        public int ConvertFromUomSk {get;set;}

        [Display(Name = "Convert To Uom:")]
        [ForeignKey("ConvertToUom")]
        [Required]
        public int ConvertToUomSk {get;set;}

        [Display(Name = "Conversion Factor:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString="{0:#########.#########}")]
        [Range(0.000000001,99999999.999999999)]
        [Required]
        public decimal ConversionFactor {get;set;}

        public DateTime CreatedAt {get;set;}

        [Required]
        public DateTime UpdatedAt {get;set;}
        
        #endregion

        #region ForeignKeys

        public virtual UomType UomType { get; set; }

        public virtual Uom ConvertFromUom { get; set; }
    
        public virtual Uom ConvertToUom { get; set; }
        
        #endregion

        #region InverseProperties
        #endregion
    }

}