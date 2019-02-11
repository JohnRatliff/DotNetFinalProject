using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using DotNetFinalProject.Models;

namespace DotNetFinalProject.Helpers
{
    public class UserHelper
    {
        private DotNetFinalProjectDbContext dbContext;
    
        public UserHelper( DotNetFinalProjectDbContext context)
        {
            dbContext = context;
        }

        #region Methods
        //--------------------------------------------------------------
        // Selects all Users ordered by LastName and FirstName
        public List<User> GetAllUsersByLastNameFirstName()
        {
           List<User> users = (from u in dbContext.User 
                                 orderby u.LastName, u.FirstName
                                 select u)
                                 .ToList();
            return users;
        }
        #endregion
    }
}