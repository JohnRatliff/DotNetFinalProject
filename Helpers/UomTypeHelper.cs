using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using DotNetFinalProject.Models;

namespace DotNetFinalProject.Helpers
{
    public class UomTypeHelper
    {
        private DotNetFinalProjectDbContext dbContext;
    
        public UomTypeHelper( DotNetFinalProjectDbContext context)
        {
            dbContext = context;
        }

        #region Methods
        //--------------------------------------------------------------
        // Selects and return all UomType ordered by UomTypeName
        public List<UomType> GetAllUomTypes()
        {
            List<UomType> allUomTypes = (from u in dbContext.UomType
                                           orderby u.UomTypeName ascending 
                                           select u)
                                           .ToList();

            return allUomTypes;
        }

        //--------------------------------------------------------------
        // Select and return one UomType related to the passed uomTypeSk
        public UomType GetUomTypeBySk(int uomTypeSk)
        {
            System.Console.WriteLine("--------------------------------GetUomTypeBySk(1)--------------------------------");

            UomType uomType = (from u in dbContext.UomType
                                 where u.UomTypeSk == uomTypeSk
                                 select u)
                                 .FirstOrDefault();

            return uomType;
        }       

        #endregion
    }
}