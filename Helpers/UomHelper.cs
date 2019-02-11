extern alias MySqlConnectorAlias;

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
    public class UomHelper
    {
        private DotNetFinalProjectDbContext dbContext;

        public UomHelper( DotNetFinalProjectDbContext context)
        {
            this.dbContext = context;
        }

        #region Methods
        //--------------------------------------------------------------
        // Selects and return all Uoms ordered by UomId from UomView
        public List<UomView> GetAllUomViewsByUomIdAsc()
        {
            System.Console.WriteLine("--------------------------------GetAllUomViewsByUomIdAsc(1)--------------------------------");

            List<UomView> allUomViews = (from u in dbContext.UomView
                                   orderby u.UomId ascending 
                                   select u)
                                   .ToList();

            return allUomViews;
        }

        //--------------------------------------------------------------
        // Selects and return all Uoms ordered by UomId
        public List<Uom> GetAllUomsByUomIdAsc()
        {
            return GetAllUomsByUomIdAsc("");
        }
        public List<Uom> GetAllUomsByUomIdAsc(string filterText)
        {
            System.Console.WriteLine("--------------------------------GetAllUomsByUomIdAsc(1)--------------------------------");
            System.Console.WriteLine($"filterText: {filterText}");

            List<Uom> allUoms = (from u in dbContext.Uom 
                                   where u.UomType.UomTypeName.Contains(filterText ?? "")
                                   orderby u.UomId ascending 
                                   select u)
                                   .Include("UomType")
                                   .Include("FromUomConversions")
                                   .Include("ToUomConversions")
                                   .ToList();

            return allUoms;
        }
 
        //--------------------------------------------------------------
        // Select and return one Uom related to the passed uomSk
        public Uom GetUomBySk(int uomSk)
        {
            System.Console.WriteLine("--------------------------------GetUomBySk(1)--------------------------------");

            Uom uom = (from u in dbContext.Uom
                         where u.UomSk == uomSk
                         select u)
                         .Include("UomType")
                         .Include("FromUomConversions")
                         .Include("ToUomConversions")
                         .FirstOrDefault();

            return uom;
        }       
 
 
        //--------------------------------------------------------------
        // Load Data by calling related Stored Procedure
        public void UomLoadData( IConfiguration configuration)
        {
            System.Console.WriteLine("--------------------------------UomLoadData:1--------------------------------");
            
            // Link to resolve the error: 'The  type 'MySqlConnection ' exists in both 'MySql.Data' ... and 'MySqlConnector'... 
            // - https://stackoverflow.com/questions/48683241/the-type-mysqlconnection-exists-in-both-mysql-data-issue

            // Link used to calll a MySql Stored Procedure:
            // - https://dev.mysql.com/doc/connector-net/en/connector-net-programming-stored-using.html

            // Call Stored Procedure

            const string storedProcedureName = "DSP_INS_Data";

            MySqlConnectorAlias::MySql.Data.MySqlClient.MySqlConnection conn = new MySqlConnectorAlias::MySql.Data.MySqlClient.MySqlConnection();
            
            // Get connection string from appsettings.json
            conn.ConnectionString = configuration.GetSection("DBInfo").GetSection("ConnectionString").Value;

            MySqlConnectorAlias::MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlConnectorAlias::MySql.Data.MySqlClient.MySqlCommand();

            try{
                conn.Open();
                cmd.Connection = conn;

                cmd.CommandText = storedProcedureName;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pTruncateData", 1);
                cmd.Parameters["@pTruncateData"].Direction = ParameterDirection.Input;

                cmd.ExecuteNonQuery();
            }
            catch(MySqlConnectorAlias::MySql.Data.MySqlClient.MySqlException ex)
            {
                System.Console.WriteLine($"Error: {ex.Number}, Message: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }       
 
        #endregion
    }
}