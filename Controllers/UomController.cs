//extern alias MySqlConnectorAlias;

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

namespace DotNetFinalProject.Controllers
{
    public class UomController : BaseController
    {
        #region Fields
        private DotNetFinalProjectDbContext dbContext;
        private UomHelper uomHelper;
        private UomTypeHelper uomTypeHelper;
        private IConfiguration configuration;
        #endregion

        #region Constructors
        public UomController(DotNetFinalProjectDbContext context, IConfiguration configuration)
        {
            this.dbContext = context;
            this.uomHelper = new UomHelper(context);
            this.uomTypeHelper = new UomTypeHelper(context);
            this.configuration = configuration;
        }
        #endregion

        #region Actions
        //--------------------------------------------------------------
        // GET: Displays Uom New / List view view
        [HttpGet]
        [Route("UomNewListView")]
        public IActionResult UomNewListView()
        {
            System.Console.WriteLine("-----------------------------------UomNewListView(1)--------------------------------------");
            if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            {
                return  RedirectToAction("Login", "Login");
            }

            setLayoutViewBag();
            Uom uom = new Uom {UomTypeSk = 2};
            ViewBag.AllUoms = uomHelper.GetAllUomsByUomIdAsc();
            ViewBag.AllUomTypes = uomTypeHelper.GetAllUomTypes();

            AllUoms();

            return View("UomNewListView", uom);
        }

        //--------------------------------------------------------------
        // GET: Returns a JSON object containing all uoms
        [HttpGet]
        [Route("AllUoms")]
        public IActionResult AllUoms()
        {
            System.Console.WriteLine("-----------------------------------AllUoms(1)--------------------------------------");
            List<Uom> allUoms = uomHelper.GetAllUomsByUomIdAsc();

            System.Console.WriteLine("-----------------------------------AllUoms(2)--------------------------------------");
            IActionResult ajax_allUoms = Json(allUoms);
            System.Console.WriteLine(ajax_allUoms);
            System.Console.WriteLine("-----------------------------------AllUoms(3)--------------------------------------");

            return ajax_allUoms;
        }

        //--------------------------------------------------------------
        // POST: Add new Uom to DB
        [HttpPost]
        [Route("Uom/New/Save")]
        public IActionResult UomNewSave(Uom uom)
        {
            System.Console.WriteLine("-----------------------------------UomNewSave--------------------------------------");

            bool isValid = ModelState.IsValid;
            if( isValid)
            {
                var uomIdInDb = dbContext.Uom.FirstOrDefault(u => u.UomId == uom.UomId);
                if( uomIdInDb != null)
                {
                    ModelState.AddModelError("UomId", "Uom Id already exists.");
                    isValid = false;
                }
                else {
                    var uomNameInDb = dbContext.Uom.FirstOrDefault(u => u.UomName == uom.UomName);
                    if( uomNameInDb != null)
                    {
                        ModelState.AddModelError("UomName", "Uom Name already exists.");
                        isValid = false;
                    }
                }
            }

            if( isValid)
            {
                // Add new Uom
                DateTime now = DateTime.Now;
                uom.CreatedAt = now;
                uom.UpdatedAt = now;
                dbContext.Add(uom);
                dbContext.SaveChanges();

                // Add the 1-1 Uom Conversion row for the new Uom
                var uomInDb = dbContext.Uom.FirstOrDefault(u => u.UomId == uom.UomId);
                UomConversion uomConversion = new UomConversion { UomTypeSk = uomInDb.UomTypeSk, ConvertFromUomSk = uomInDb.UomSk, ConvertToUomSk = uomInDb.UomSk, ConversionFactor = 1, CreatedAt = now, UpdatedAt = now};
                dbContext.Add(uomConversion);
                dbContext.SaveChanges();

                return RedirectToAction("UomNewListView");
            }
            else
            {
                // New was invalid, route back to Uom New / List view
                setLayoutViewBag();
                ViewBag.AllUoms = uomHelper.GetAllUomsByUomIdAsc();
                ViewBag.AllUomTypes = uomTypeHelper.GetAllUomTypes();
    
                return View("UomNewListView", uom);
            }
        }

        //--------------------------------------------------------------
        // GET: Edit Uom view
        [HttpGet]
        [Route("Uom/Edit/{uomSk}")]
        public IActionResult UomEdit(int uomSk)
        {
            System.Console.WriteLine("-----------------------------------UomEdit--------------------------------------");
            if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            {
                return  RedirectToAction("Login", "Login");
            }

            setLayoutViewBag();
            Uom uom = uomHelper.GetUomBySk(uomSk);
            ViewBag.AllUomTypes = uomTypeHelper.GetAllUomTypes();
            return View("UomEdit", uom);
        }


        //--------------------------------------------------------------
        // POST: Save Edit to Uom to DB
        [HttpPost]
        [Route("Uom/Edit/Save")]
        public IActionResult UomEditSave(Uom uom)
        {
            System.Console.WriteLine("-----------------------------------UomEditSave(Uom uom):1--------------------------------------");

            bool isValid = ModelState.IsValid;
            if( isValid)
            {
                var uomIdInDb = (from u in dbContext.Uom
                                  where u.UomSk != uom.UomSk
                                  where u.UomId == uom.UomId
                                  select u).FirstOrDefault();

                if( uomIdInDb != null)
                {
                    ModelState.AddModelError("UomId", "Uom Id already exists.");
                    isValid = false;
                }
                else
                {
                    var uomNameInDb = (from u in dbContext.Uom
                                        where u.UomSk != uom.UomSk
                                        where u.UomName == uom.UomName
                                        select u).FirstOrDefault();

                    if( uomNameInDb != null)
                    {
                        ModelState.AddModelError("UomName", "Uom Name already exists.");
                        isValid = false;
                    }
                }
            }

            if( isValid)
            {
                // Save uom
                DateTime now = DateTime.Now;
                uom.UpdatedAt = now;
                dbContext.Update(uom);
                dbContext.SaveChanges();

                return RedirectToAction("UomNewListview");
            }

            // Edit was invalid, route back to Uom Edit view
            setLayoutViewBag();
            ViewBag.AllUomTypes = uomTypeHelper.GetAllUomTypes();
            return View("UomEdit", uom);
        }

        //--------------------------------------------------------------
        // Get: Delete the passed Uom (ref. by uomSk) from DB
        [HttpGet]
        [Route("Uom/Delete/{uomSk}")]
        public IActionResult UomDelete(int uomSk)
        {
            System.Console.WriteLine("-----------------------------------UomDelete--------------------------------------");
            if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            {
                return  RedirectToAction("Login", "Login");
            }

            bool isDeleteOk = true;
            Uom uomInDb = uomHelper.GetUomBySk(uomSk);
            if( uomInDb == null)
            {
                // Uom not found in Db.  This is an exception...
                isDeleteOk = false;
            }

            if( isDeleteOk)
            {
                System.Console.WriteLine("-----------------------------------UomDelete-IsDeleteId=true-------------------------------------");
    
                // Delete Uom
                dbContext.Uom.Remove(uomInDb);
                dbContext.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("-----------------------------------UomDelete-IsDeleteId=false-------------------------------------");
            }
            // return Json(true, JsonRequestBehavior.AllowGet);
            return RedirectToAction("UomNewListView");
        }


        //--------------------------------------------------------------
        // Get: Load Standard Units of Measure and Unit Conversions
        [HttpGet]
        [Route("Uom/LoadData")]
        public IActionResult UomLoadData()
        {
            System.Console.WriteLine("-----------------------------------UomLoadData:1--------------------------------------");
            if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            {
                return  RedirectToAction("Login", "Login");
            }

            // Load Data 
            uomHelper.UomLoadData(configuration);

            return RedirectToAction("UomNewListView");
        }
        #endregion

        #region Methods
        #endregion
    }
}