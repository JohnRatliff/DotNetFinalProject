using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using DotNetFinalProject.Models;

namespace DotNetFinalProject.Helpers
{
    public class UomConversionHelper
    {
        private DotNetFinalProjectDbContext dbContext;
    
        public UomConversionHelper( DotNetFinalProjectDbContext context)
        {
            dbContext = context;
        }

        #region Methods
        //--------------------------------------------------------------
        // Selects and return all UomConversion ordered by UomTypeName, FromUomId, ToUomId
        public List<UomConversion> GetUomConversionsByTypeFromToAsc()
        {
            System.Console.WriteLine("------------------------------------------GetUomConversionsByTypeFromToAsc(1)------------------------------------------");
            List<UomConversion> uomConversions = (from u in dbContext.UomConversion
                                                    orderby u.UomType.UomTypeName, u.ConvertFromUom.UomId, u.ConvertToUom.UomId ascending 
                                                    select u)
                                                    .Include("UomType")
                                                    .Include("ConvertFromUom")
                                                    .Include("ConvertToUom")
                                                    .ToList();
            System.Console.WriteLine("------------------------------------------GetUomConversionsByTypeFromToAsc(2)------------------------------------------");

            return uomConversions;
        }

         //--------------------------------------------------------------
        // Select and return the UomConversion related to the passed uomConversionSk
        public UomConversion GetUomConversionBySk(int uomConversionSk)
        {
            System.Console.WriteLine("------------------------------------------GetUomConversionBySk------------------------------------------");
            UomConversion uomConversion = (from u in dbContext.UomConversion
                                             where u.UomConversionSk == uomConversionSk
                                             select u)
                                             .Include("UomType")
                                             .Include("ConvertFromUom")
                                             .Include("ConvertToUom")
                                             .FirstOrDefault();

            return uomConversion;
        }       
        #endregion
    }
}