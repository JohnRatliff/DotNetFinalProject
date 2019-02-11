using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using DotNetFinalProject.Models;
using DotNetFinalProject.Helpers;


namespace DotNetFinalProject.Controllers
{
    public class HomeController : BaseController
    {
        #region Fields
        private DotNetFinalProjectDbContext dbContext;
        private UserHelper userHelper;
        #endregion

        #region Constructors
        public HomeController( DotNetFinalProjectDbContext context)
        {
            dbContext = context;
            userHelper = new UserHelper(context);
        }
        #endregion

        #region Actions
        //--------------------------------------------------------------
        // GET: Display Home view
        [HttpGet]
        [Route("")]
        public IActionResult Home()
        {
            System.Console.WriteLine("--------------------------Home--------------------------");
            // if (HttpContext.Session.GetInt32("LoggedIn") != 1)
            // {
            //     return  RedirectToAction("Login", "User");
            // }

            setLayoutViewBag();
            // int? i = HttpContext.Session.GetInt32("LoggedIn");
            // HttpContext.Session.SetInt32("LoggedIn", i ?? 0);
            // ViewBag.LoggedIn = HttpContext.Session.GetInt32("LoggedIn");

            // string loggedInUserFirstName = HttpContext.Session.GetString("LoggedInUserFirstName");
            // HttpContext.Session.SetString("LoggedInUserFirstName", loggedInUserFirstName ?? "");
            // ViewBag.LoggedInUserFirstName = HttpContext.Session.GetString("LoggedInUserFirstName");

            return View();
        }
       #endregion


        #region Methods
        #endregion
    }
}