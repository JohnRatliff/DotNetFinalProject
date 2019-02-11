using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using DotNetFinalProject.Helpers;
using DotNetFinalProject.Models;

namespace DotNetFinalProject.Controllers
{
    public class UomConversionController : BaseController
    {
        #region Fields
        private DotNetFinalProjectDbContext dbContext;
        private UomConversionHelper uomConversionHelper;
        private UomTypeHelper uomTypeHelper;
        private UomHelper uomHelper;
        private IConfiguration configuration;
        #endregion

        #region Constructors
        public UomConversionController(DotNetFinalProjectDbContext context, IConfiguration configuration)
        {
            this.dbContext = context;
            this.uomConversionHelper = new UomConversionHelper(context);
            this.uomTypeHelper = new UomTypeHelper(context);
            this.uomHelper = new UomHelper(context);
            this.configuration = configuration;
        }
        #endregion

        #region Actions
        //--------------------------------------------------------------
        // GET: Displays UomConversion New / List view view
        [HttpGet]
        [Route("UomConversionNewListView")]
        public IActionResult UomConversionNewListView()
        {
            System.Console.WriteLine("-----------------------------------Get:UomConversionNewListView():1--------------------------------------");
            if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            {
                return  RedirectToAction("Login", "Login");
            }

            setLayoutViewBag();
            UomConversion uomConversion = new UomConversion {UomTypeSk = 2, ConversionFactor = 1};
            ViewBag.AllUomConversions = uomConversionHelper.GetUomConversionsByTypeFromToAsc();
            ViewBag.AllUomTypes = uomTypeHelper.GetAllUomTypes();
            UomType uomType = uomTypeHelper.GetUomTypeBySk(2);
            ViewBag.AllUoms = uomHelper.GetAllUomsByUomIdAsc(uomType.UomTypeName);

            return View("UomConversionNewListView", uomConversion);
        }

        //--------------------------------------------------------------
        // POST: Displays UomConversion New / List view view
        [HttpPost]
        [Route("UomConversionNewListView")]
        public IActionResult UomConversionNewListView(UomConversion uomConversion)
        {
            System.Console.WriteLine("-----------------------------------Post:UomConversionNewListView(UomConversion uomConversion):1--------------------------------------");
            if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            {
                return  RedirectToAction("Login", "Login");
            }

            System.Console.WriteLine("-----------------------------------Post:UomConversionNewListView(UomConversion uomConversion):2--------------------------------------");
            
            UomType uomType = uomTypeHelper.GetUomTypeBySk(uomConversion.UomTypeSk);
            string filterText = uomType.UomTypeName;

            setLayoutViewBag();
            ViewBag.AllUomConversions = uomConversionHelper.GetUomConversionsByTypeFromToAsc();
            ViewBag.AllUomTypes = uomTypeHelper.GetAllUomTypes();
            ViewBag.AllUoms = uomHelper.GetAllUomsByUomIdAsc(filterText);

            return View("UomConversionNewListView", uomConversion);
        }

        //--------------------------------------------------------------
        // POST: Save new UomConversion to DB
        [HttpPost]
        [Route("UomConversion/New/Save")]
        public IActionResult UomConversionNewSave(UomConversion uomConversion)
        {
            System.Console.WriteLine("-----------------------------------UomConversionNewSave(UomConversion uomConversion):1--------------------------------------");

            bool isValid = ModelState.IsValid;
            if( isValid)
            {
                var uomConversionInDb = (from u in dbContext.UomConversion
                                            where u.ConvertFromUomSk == uomConversion.ConvertFromUomSk
                                            where u.ConvertToUomSk == uomConversion.ConvertToUomSk
                                            select u).FirstOrDefault();
                if( uomConversionInDb != null)
                {
                    ModelState.AddModelError("ConvertFromUomSK", "From / To Uom conversion already exists.");
                    isValid = false;
                }
            }

            if( isValid)
            {
                // Add new UomConversion
                DateTime now = DateTime.Now;
                uomConversion.CreatedAt = now;
                uomConversion.UpdatedAt = now;
                dbContext.Add(uomConversion);
                dbContext.SaveChanges();
                dbContext.SaveChanges();

                return RedirectToAction("UomConversionNewListview");
            }
            else
            {
                System.Console.WriteLine("-----------------------------------UomConversionNewSave(UomConversion uomConversion):2--------------------------------------");
                System.Console.WriteLine($"{uomConversion.UomTypeSk}");

                // New was invalid, route back to UomConversion New / List view
                setLayoutViewBag();
                ViewBag.AllUomConversions = uomConversionHelper.GetUomConversionsByTypeFromToAsc();
                ViewBag.AllUomTypes = uomTypeHelper.GetAllUomTypes();
                UomType uomType = uomTypeHelper.GetUomTypeBySk(uomConversion.UomTypeSk);

                System.Console.WriteLine($"{uomType.UomTypeName}");

                ViewBag.AllUoms = uomHelper.GetAllUomsByUomIdAsc(uomType.UomTypeName);

                return View("UomConversionNewListView", uomConversion);
            }
        }

        //--------------------------------------------------------------
        // GET: Edit UomConversion view
        [HttpGet]
        [Route("UomConversion/Edit/{uomConversionSk}")]
        public IActionResult UomConversionEdit(int uomConversionSk)
        {
            System.Console.WriteLine("-----------------------------------UomConversionEdit(int uomConversionSk):1--------------------------------------");
            System.Console.WriteLine($"uomConversionSk: {uomConversionSk}");

            if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            {
                return  RedirectToAction("Login", "Login");
            }

            setLayoutViewBag();
            UomConversion uomConversion = uomConversionHelper.GetUomConversionBySk(uomConversionSk);
            ViewBag.AllUomConversions = uomConversionHelper.GetUomConversionsByTypeFromToAsc();
            ViewBag.AllUomTypes = uomTypeHelper.GetAllUomTypes();
            ViewBag.AllUoms = uomHelper.GetAllUomsByUomIdAsc(uomConversion.UomType.UomTypeName);

            return View("UomConversionEdit", uomConversion);
        }


        //--------------------------------------------------------------
        // POST: Save Edit to UomConversion to DB
        [HttpPost]
        [Route("UomConversion/Edit/Save")]
        public IActionResult UomConversionEditSave(UomConversion uomConversion)
        {
            System.Console.WriteLine("-----------------------------------UomConversionEditSave(UomConversion uomConversion):1--------------------------------------");
            bool isValid = ModelState.IsValid;
            if( isValid)
            {
                var uomConversionInDb = (from u in dbContext.UomConversion
                                           where u.UomConversionSk != uomConversion.UomConversionSk
                                           where u.ConvertFromUomSk == uomConversion.ConvertFromUomSk
                                           where u.ConvertToUomSk == uomConversion.ConvertToUomSk
                                           select u).FirstOrDefault();

                if( uomConversionInDb != null)
                {
                    System.Console.WriteLine("-----------------------------------UomConversionEditSave(UomConversion uomConversion):2--------------------------------------");
        
                    ModelState.AddModelError("ConvertFromUomSk", "From / To Conversion already exists.");
                    isValid = false;
                }
            }

            if( isValid)
            {
                System.Console.WriteLine("-----------------------------------UomConversionEditSave(UomConversion uomConversion):3--------------------------------------");

                // Save UomConversion
                DateTime now = DateTime.Now;
                uomConversion.UpdatedAt = now;
                dbContext.Update(uomConversion);
                dbContext.SaveChanges();

                return RedirectToAction("UomConversionNewListview");
            }

            // Edit was invalid, route back to UomConversion Edit view
            setLayoutViewBag();
            ViewBag.AllUomConversions = uomConversionHelper.GetUomConversionsByTypeFromToAsc();
            ViewBag.AllUomTypes = uomTypeHelper.GetAllUomTypes();
            UomType uomType = uomTypeHelper.GetUomTypeBySk(uomConversion.UomTypeSk);
            ViewBag.AllUoms = uomHelper.GetAllUomsByUomIdAsc(uomType.UomTypeName);

            return View("UomConversionEdit", uomConversion);
        }

        //--------------------------------------------------------------
        // GET: Delete the passed UomConversion (ref. by UomConversionSk) from DB
        [HttpGet]
        [Route("UomConversion/Delete/{uomConversionSk}")]
        public IActionResult UomConversionDelete(int uomConversionSk)
        {
            System.Console.WriteLine("-----------------------------------UomConversionDelete--------------------------------------");
            if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            {
                return  RedirectToAction("Login", "Login");
            }

            bool isDeleteOk = true;
            UomConversion uomConversionInDb = uomConversionHelper.GetUomConversionBySk(uomConversionSk);
            if( uomConversionInDb == null)
            {
                // Uom not found in Db.  This is an exception...
                isDeleteOk = false;
            }

            if( isDeleteOk)
            {
                System.Console.WriteLine("-----------------------------------UomConversionDelete-IsDeleteId=true-------------------------------------");
    
                // Delete Uom
                dbContext.UomConversion.Remove(uomConversionInDb);
                dbContext.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("-----------------------------------UomConversionDelete-IsDeleteId=false-------------------------------------");
            }
            return RedirectToAction("UomConversionNewListView");
        }
 

        //--------------------------------------------------------------
        // Get: Load Standard Units of Measure and Unit Conversions
        [HttpGet]
        [Route("UomConversion/LoadData")]
        public IActionResult UomLoadData()
        {
            System.Console.WriteLine("-----------------------------------UomLoadData:1--------------------------------------");
            if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            {
                return  RedirectToAction("Login", "Login");
            }

            // Load Data 
            uomHelper.UomLoadData(configuration);

            return RedirectToAction("UomConversionNewListView");
        }
       #endregion

        #region Methods
        #endregion
    }
}