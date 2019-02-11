using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DotNetFinalProject.Models;

namespace DotNetFinalProject.Helpers
{
    public class PaymentTermHelper
    {
        private DotNetFinalProjectDbContext dbContext;

        public PaymentTermHelper( DotNetFinalProjectDbContext context)
        {
            this.dbContext = context;
        }

        #region Methods
        //--------------------------------------------------------------
        // Selects and return all PaymentTerms ordered by Name
        public List<PaymentTerm> GetAllPaymentTermsByNameAsc()
        {
            System.Console.WriteLine("--------------------------------GetAllPaymentTermsByNameAsc(1)--------------------------------");

            List<PaymentTerm> allPaymentTerms = (from p in dbContext.PaymentTerm
                                                   orderby p.Name ascending 
                                                   select p)
                                                   .ToList();

            return allPaymentTerms;
        }

        //--------------------------------------------------------------
        // Select and return one PaymentTerm related to the passed paymentTermSk
        public PaymentTerm GetPaymentTermBySk(int paymentTermSk)
        {
            System.Console.WriteLine("--------------------------------GetPaymentTermBySk(1)--------------------------------");

            PaymentTerm paymentTerm = (from p in dbContext.PaymentTerm
                                         where p.PaymentTermSk == paymentTermSk
                                         select p)
                                         .FirstOrDefault();

            return paymentTerm;
        }

        #endregion
    }
}