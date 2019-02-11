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
    public class BaseController : Controller
    {
        #region Fields
        #endregion

        #region Constructors
        public BaseController() {}
        #endregion

        #region Actions
       #endregion


        #region Methods
        internal void setLayoutViewBag()
        {
            // LoggedIn
            int? loggieIn = HttpContext.Session.GetInt32("LoggedIn");
            HttpContext.Session.SetInt32("LoggedIn", loggieIn ?? 0);
            ViewBag.LoggedIn = HttpContext.Session.GetInt32("LoggedIn");

            // LoggedInUserId
            int? loggedInUserId = HttpContext.Session.GetInt32("LoggedInUserId");
            HttpContext.Session.SetInt32("LoggedInUserId", loggedInUserId ?? 0);
            ViewBag.LoggedInUserId = HttpContext.Session.GetInt32("LoggedInUserId");

            // LoggedInUserFirstName
            string loggedInUserFirstName = HttpContext.Session.GetString("LoggedInUserFirstName");
            HttpContext.Session.SetString("LoggedInUserFirstName", loggedInUserFirstName ?? "");
            ViewBag.LoggedInUserFirstName = HttpContext.Session.GetString("LoggedInUserFirstName");

            // LoggedInUserLastName
            string loggedInUserLastName = HttpContext.Session.GetString("LoggedInUserLastName");
            HttpContext.Session.SetString("LoggedInUserLastName", loggedInUserLastName ?? "");
            ViewBag.LoggedInUserLastName = HttpContext.Session.GetString("LoggedInUserLastName");

            // LoggedInUserLevel
            int? loggedInUserLevel = HttpContext.Session.GetInt32("LoggedInUserId");
            HttpContext.Session.SetInt32("LoggedInUserLevel", loggedInUserLevel ?? 0);
            ViewBag.LoggedInUserId = HttpContext.Session.GetInt32("LoggedInUserLevel");
        }        
        #endregion
    }
}