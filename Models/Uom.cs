using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetFinalProject.Models
{
    [Table("uoms")]
    public class Uom
    {
        #region Properties
        [Key]
        [Required]
        public int UomSk {get;set;}

        [Display(Name = "Uom Type:")]
        [ForeignKey("UomType")]
        [Required]
        public int UomTypeSk {get;set;}
        
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

        [Display(Name = "Uom (Type)")]
        [NotMapped]
        public string UomIdUomTypeName
        {
            get { return String.Format("{0} ({1})", UomId, (UomType == null ? "" : UomType.UomTypeName)); }
            set {}
        }
        
        #endregion


        #region ForeignKeys

        public UomType UomType { get; set; }
        
        #endregion

        #region InverseProperties
        
        [InverseProperty("ConvertFromUom")]
        public List<UomConversion> FromUomConversions {get;set;}
        
        [InverseProperty("ConvertToUom")]
        public List<UomConversion> ToUomConversions {get;set;}
        
        #endregion
    }

}