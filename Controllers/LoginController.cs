using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using DotNetFinalProject.Models;


namespace DotNetFinalProject.Controllers
{
    public class LoginController : BaseController
    {
        #region Fields
        private UserDbContext dbContext;


        // Controller to route to after a successful login
        private string _successController = "Home";
        public string SuccessController
        {
           get{ return _successController;}
        }
        // Controller-Action to route to after a successful login
        private string _successAction = "Home";
        public string SuccessAction
        {
           get{ return _successAction;}
        }
        #endregion

        #region Constructors
        public LoginController( UserDbContext context)
        {
            dbContext = context;
        }
        #endregion

        #region Actions
        //--------------------------------------------------------------
        // GET: Index, Displays registration
        [HttpGet]
        [Route("Registration")]
        public IActionResult Registration()
        {
            System.Console.WriteLine("--------------------------Registration--------------------------");
            setLayoutViewBag();
            return View();
        }

        //--------------------------------------------------------------
        // POST: RegistrationSave, Validates registration.  If valid, 
        //       adds user and routes to Login.  If NOT valid,
        //       routes back to Registration view
        [HttpPost]
        [Route("RegistrationSave")]
        public IActionResult RegistrationSave(User user)
        {
            System.Console.WriteLine("--------------------------RegistrationSave(1)--------------------------");
            if( ModelState.IsValid)
            {
                // Verify email address is not already registered in users 
                var userInDb = dbContext.User.FirstOrDefault(u => u.Email == user.Email);
                if( userInDb != null)
                {
                    ModelState.AddModelError("Email", "This Email Address is already registered.");
                    setLayoutViewBag();
                    return View("Registration");
                }

                // Verify password and confirm password match
                if( user.Password != user.ConfirmPassword)
                {
                    ModelState.AddModelError("Password", "Password does not match Confirm Password.");
                    setLayoutViewBag();
                    return View("Registration");
                }


                // Initialize PasswordHasher so we can saved the user's password as a hashed string
                PasswordHasher<User> hasher = new PasswordHasher<User>();
                user.Password = hasher.HashPassword(user, user.Password);

                int userCount = dbContext.User.ToList().Count;
                DateTime now = DateTime.Now;
                user.CreatedAt = now;
                user.UpdatedAt = now;
                user.UserLevel = (userCount == 0 ? 1 : 2);  // First logged in user is assigned Admin (1) UserLevel; otherwise Normal (2)
                dbContext.Add(user);
                dbContext.SaveChanges();

                // Log user in
                logUserIn( user);

                System.Console.WriteLine("--------------------------RegistrationSave(2)--------------------------");
                System.Console.WriteLine($"LoggedIn.............: {HttpContext.Session.GetInt32("LoggedIn")}");
                System.Console.WriteLine($"LoggedInUserLevel....: {HttpContext.Session.GetInt32("LoggedInUserLevel")}");
                System.Console.WriteLine($"LoggedInUserSk.......: {HttpContext.Session.GetInt32("LoggedInUserSk")}");
                System.Console.WriteLine($"LoggedInUserEmail....: {HttpContext.Session.GetString("LoggedInUserEmail")}");
                System.Console.WriteLine($"LoggedInUserFirstName: {HttpContext.Session.GetString("LoggedInUserFirstName")}");
                System.Console.WriteLine($"LoggedInUserLastName.: {HttpContext.Session.GetString("LoggedInUserLastName")}");

                System.Console.WriteLine("-----------------------------RegistrationSave(3)------------------------------");
                System.Console.WriteLine($"Action: {this.SuccessAction}, Controller: {this.SuccessController}, UserSk: {user.UserSk}");
                return RedirectToAction(this.SuccessAction, this.SuccessController, new {userSk = user.UserSk});
                // return View("Success");
            }
            else
            {
                System.Console.WriteLine("--------------------------RegistrationSave(4)--------------------------");
                setLayoutViewBag();
                return View("Registration");
            }
        }        

        //--------------------------------------------------------------
        // GET: Login, Displays login view
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            System.Console.WriteLine("--------------------------Login--------------------------");
            setLayoutViewBag();
            return View("Login");
        }

        //--------------------------------------------------------------
        // Post: LoginSave, Validates login.  If valid, routes to Success 
        //       view.  If NOT valid, routes to Login view
        [HttpPost]
        [Route("LoginSave")]
        public IActionResult LoginSave(Login login)
        {
            System.Console.WriteLine("--------------------------LoginSave(1)--------------------------");
            if( ModelState.IsValid)
            {
                 // Verify email address exists in the users table
                User userInDb = dbContext.User.FirstOrDefault(u => u.Email == login.LoginEmail);
                if( userInDb == null)
                {
                    ModelState.AddModelError("LoginEmail", "Email Address was not found.");
                    setLayoutViewBag();
                    return View("Login");
                }

                // Hash password and confirm hashed password matches hashed password in DB
                var hasher = new PasswordHasher<Login>();
                var result = hasher.VerifyHashedPassword(login, userInDb.Password, login.LoginPassword);
                if( result == 0)
                {
                    ModelState.AddModelError("LoginPassword", "Invalid Password.");
                    setLayoutViewBag();
                    return View("Login");
                }

                // Log user in
                logUserIn(userInDb);

                System.Console.WriteLine("--------------------------LoginSave(2)--------------------------");
                System.Console.WriteLine($"LoggedIn.............: {HttpContext.Session.GetInt32("LoggedIn")}");
                System.Console.WriteLine($"LoggedInUserLevel....: {HttpContext.Session.GetInt32("LoggedInUserLevel")}");
                System.Console.WriteLine($"LoggedInUserSk.......: {HttpContext.Session.GetInt32("LoggedInUserSk")}");
                System.Console.WriteLine($"LoggedInUserEmail....: {HttpContext.Session.GetString("LoggedInUserEmail")}");
                System.Console.WriteLine($"LoggedInUserFirstName: {HttpContext.Session.GetString("LoggedInUserFirstName")}");
                System.Console.WriteLine($"LoggedInUserLastName.: {HttpContext.Session.GetString("LoggedInUserLastName")}");



                System.Console.WriteLine("-----------------------------LoginSave(3)------------------------------");
                System.Console.WriteLine($"Action: {this.SuccessAction}, Controller: {this.SuccessController}, UserSk: {userInDb.UserSk}");

                return RedirectToAction(this.SuccessAction, this.SuccessController, new {userSk = userInDb.UserSk});
                // return View("Success");
            }
            setLayoutViewBag();
            return View("Login");
        }

        //--------------------------------------------------------------
        // Post: Logout, Logs user out and routes to the Register view
        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout()
        {
            System.Console.WriteLine("--------------------------Logout--------------------------");
            logUserOut();
            return  RedirectToAction("Home", "Home");
        }
        #endregion


        #region Methods
       //--------------------------------------------------------------
        private void logUserIn(User user)
        {
            HttpContext.Session.SetInt32("LoggedIn", 1);  // 1=Logged in;  null or 0 = NOT logged in
            HttpContext.Session.SetInt32("LoggedInUserSk", user.UserSk);
            HttpContext.Session.SetString("LoggedInUserEmail", user.Email);
            HttpContext.Session.SetString("LoggedInUserFirstName", user.FirstName);
            HttpContext.Session.SetString("LoggedInUserLastName", user.LastName);
            HttpContext.Session.SetInt32("LoggedInUserLevel", user.UserLevel);
        }

        //--------------------------------------------------------------
        private bool logUserOut()
        {
            bool loggedOut = false;
            if( HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                HttpContext.Session.Clear();
                loggedOut = true;
            }
            return loggedOut;
        }

        //--------------------------------------------------------------
        private bool isUserLoggedIn()
        {
            bool isLoggedIn = false;
            if (HttpContext.Session.GetInt32("LoggedIn") == 1)
            {
                isLoggedIn = true;
            }
            return isLoggedIn;
        }
        #endregion
    }
}