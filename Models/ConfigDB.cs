using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Proyecto____Examen_Final.Models
{
    public class ConfigDB
    {
        private static string connectionString = "server=saacapps.com;UserID=saacapps_ucatolica;Database=saacapps_training;Password=Ucat0lica";

        /// <summary>
        /// Manage the DB connection
        /// </summary>
        /// <returns>A DB connection object like MySqlConnection</returns>

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}