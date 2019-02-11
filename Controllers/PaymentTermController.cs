using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DotNetFinalProject.Helpers;
using DotNetFinalProject.Models;
using DotNetFinalProject.Enums;

namespace DotNetFinalProject.Controllers
{
    public class PaymentTermController : BaseController
    {
        #region Fields
        private DotNetFinalProjectDbContext dbContext;
        private PaymentTermHelper paymentTermHelper;

        #endregion

        #region Constructors
        public PaymentTermController(DotNetFinalProjectDbContext context)
        {
            this.dbContext = context;
            this.paymentTermHelper = new PaymentTermHelper(context);
        }
        #endregion

        #region Actions
        //--------------------------------------------------------------
        // GET: Displays PaymentTerm New / List view view
        [HttpGet]
        [Route("PaymentTermNewListView")]
        public IActionResult PaymentTermNewListView()
        {
            System.Console.WriteLine("-----------------------------------PaymentTermNewListView(1)--------------------------------------");
            if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            {
                return  RedirectToAction("Login", "Login");
            }

            setLayoutViewBag();
            PaymentTerm paymentTerm = new PaymentTerm {DiscountDays = 0, DueDays = 0, DiscountRate = 0, DiscountDateOption = DiscountDateOption.OrderDate};
            ViewBag.AllPaymentTerms = paymentTermHelper.GetAllPaymentTermsByNameAsc();

            return View("PaymentTermNewListView", paymentTerm);
        }

        //--------------------------------------------------------------
        // POST: Add new PaymentTerm to DB
        [HttpPost]
        [Route("PaymentTerm/New/Save")]
        public IActionResult PaymentTermNewSave(PaymentTerm paymentTerm)
        {
            System.Console.WriteLine("-----------------------------------PaymentTermNewSave--------------------------------------");

            bool isValid = ModelState.IsValid;
            if( isValid)
            {
                var nameInDb = dbContext.PaymentTerm.FirstOrDefault(p => p.Name == paymentTerm.Name);
                if( nameInDb != null)
                {
                    ModelState.AddModelError("Name", "Name already exists.");
                    isValid = false;
                }
            }

            if( isValid)
            {
                // Add new PaymentTerm
                DateTime now = DateTime.Now;
                paymentTerm.CreatedAt = now;
                paymentTerm.UpdatedAt = now;
                dbContext.Add(paymentTerm);
                dbContext.SaveChanges();

                return RedirectToAction("PaymentTermNewListView");
            }
            else
            {
                // New was invalid, route back to PaymentTerm New / List view
                setLayoutViewBag();
                ViewBag.AllPaymentTerms = paymentTermHelper.GetAllPaymentTermsByNameAsc();
    
                return View("PaymentTermNewListView", paymentTerm);
            }
        }

        //--------------------------------------------------------------
        // GET: Edit PaymentTerm view
        [HttpGet]
        [Route("PaymentTerm/Edit/{paymentTermSk}")]
        public IActionResult PaymentTermEdit(int paymentTermSk)
        {
            System.Console.WriteLine("-----------------------------------PaymentTermEdit--------------------------------------");
            System.Console.WriteLine($"PaymentTermSk: {paymentTermSk}");

            if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            {
                return  RedirectToAction("Login", "Login");
            }

            setLayoutViewBag();
            PaymentTerm paymentTerm = paymentTermHelper.GetPaymentTermBySk(paymentTermSk);
            return View("PaymentTermEdit", paymentTerm);
        }


        //--------------------------------------------------------------
        // POST: Save Edit to PaymentTerm to DB
        [HttpPost]
        [Route("PaymentTerm/Edit/Save")]
        public IActionResult PaymentTermEditSave(PaymentTerm paymentTerm)
        {
            System.Console.WriteLine("-----------------------------------UomEditSave(PaymentTerm paymentTerm):1--------------------------------------");

            bool isValid = ModelState.IsValid;
            if( isValid)
            {
                var nameInDb = (from p in dbContext.PaymentTerm
                                  where p.PaymentTermSk != paymentTerm.PaymentTermSk
                                  where p.Name == paymentTerm.Name
                                  select p).FirstOrDefault();

                if( nameInDb != null)
                {
                    ModelState.AddModelError("Name", "Name already exists.");
                    isValid = false;
                }
            }

            if( isValid)
            {
                // Save PaymentTerm
                DateTime now = DateTime.Now;
                paymentTerm.UpdatedAt = now;
                dbContext.Update(paymentTerm);
                dbContext.SaveChanges();

                return RedirectToAction("PaymentTermNewListview");
            }

            // Edit was invalid, route back to PaymentTerm Edit view
            setLayoutViewBag();
            return View("PaymentTermEdit", paymentTerm);
        }

        //--------------------------------------------------------------
        // Get: Delete the passed PaymentTerm (ref. by paymentTermSk) from DB
        [HttpGet]
        [Route("PaymentTerm/Delete/{paymentTermSk}")]
        public IActionResult PaymentTermDelete(int paymentTermSk)
        {
            System.Console.WriteLine("-----------------------------------PaymentTermDelete--------------------------------------");
            if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            {
                return  RedirectToAction("Login", "Login");
            }

            bool isDeleteOk = true;
            PaymentTerm paymentTermInDb = paymentTermHelper.GetPaymentTermBySk(paymentTermSk);
            if( paymentTermInDb == null)
            {
                // PaymentTerm not found in Db.  This is an exception...
                isDeleteOk = false;
            }

            if( isDeleteOk)
            {
                System.Console.WriteLine("-----------------------------------PaymentTermDelete-IsDeleteOk=true-------------------------------------");

                // Delete PaymentTerm
                dbContext.PaymentTerm.Remove(paymentTermInDb);
                dbContext.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("-----------------------------------PaymentTermDelete-IsDeleteOk=false-------------------------------------");
            }
            return RedirectToAction("PaymentTermNewListView");
        }
        #endregion

        #region Methods
        #endregion
    }
}