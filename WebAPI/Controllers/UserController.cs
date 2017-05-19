using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/user
        public List<Models.User> GetAllUsers()
        {
            List<Models.User> users = new List<Models.User>();
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM user;";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new Models.User()
                    {
                        id = int.Parse(reader["userID"].ToString()),
                        name = reader["userName"].ToString(),
                        email = reader["userEmail"].ToString(),
                        password = reader["userPassword"].ToString(),
                        username = reader["userNickname"].ToString(),
                        status = int.Parse(reader["userStatus"].ToString())
                    });
                }

                return users;
            }
            catch(Exception e)
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

        // GET: api/user/{id}
        public Models.User GetUser(int id)
        {
            Models.User user = new Models.User();

            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = String.Format("SELECT * FROM user WHERE userID = {0};", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user.id = int.Parse(reader["userID"].ToString());
                    user.name = reader["userName"].ToString();
                    user.email = reader["userEmail"].ToString();
                    user.password = reader["userPassword"].ToString();
                    user.username = reader["userNickname"].ToString();
                    user.status = int.Parse(reader["userStatus"].ToString());                    
                }

                return user;
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

        // POST: api/user
        public string PostUser([FromBody] Models.User user)
        {
            return user.name;
        }

        // PUT: api/user/{id}
        public void PutUser(int id, [FromBody] Models.User user)
        {
        }

        // DELETE: api/user/{id}
        public void DeleteUser(int id)
        {
        }
    }
}
