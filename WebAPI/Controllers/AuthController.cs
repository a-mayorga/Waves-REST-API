using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {    
        [HttpPost]
        [Route("login")]
        public Models.User Login(Models.User user)
        {
            Models.User userData = new Models.User();
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = String.Format("SELECT * FROM user WHERE userNickname = '{0}' AND userPassword = '{1}';", user.username, user.password);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    userData.id = int.Parse(reader["userID"].ToString());
                    userData.name = reader["userName"].ToString();
                    userData.email = reader["userEmail"].ToString();
                    userData.password = reader["userPassword"].ToString();
                    userData.username = reader["userNickname"].ToString();
                    userData.status = int.Parse(reader["userStatus"].ToString());
                }

                return userData;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
