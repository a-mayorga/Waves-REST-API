using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace WebAPI
{
    public class Connection
    {
        public static MySqlConnection GetConnection()
        {
            try
            {
                string connData = "server=localhost; uid=root; pwd=; database=waves;";
                MySqlConnection conn = new MySqlConnection(connData);
                conn.Open();
                return conn;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}