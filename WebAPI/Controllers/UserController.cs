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
        public string PostUser(Models.User user)
        {
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                string query = String.Format("INSERT INTO user(userName, userEmail, userPassword, userNickname, userStatus) VALUES('{0}', '{1}', '{2}', '{3}', {4});", user.name, user.email, user.password, user.username, user.status);

                MySqlCommand insert = new MySqlCommand(query, conn);
                insert.ExecuteNonQuery();
                conn.Close();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = String.Format("SELECT userID FROM user WHERE userNickname = '{0}';", user.username);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    user.id = int.Parse(reader["userID"].ToString());
                }
                conn.Close();
                conn.Open();

                string queryLibrary = String.Format("INSERT INTO library(userID) VALUES({0});", user.id);

                MySqlCommand insertLibrary = new MySqlCommand(queryLibrary, conn);
                insertLibrary.ExecuteNonQuery();

                conn.Close();

                return "Bienvenido a Waves";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return e.Message;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        // PUT: api/user
        public void PutUser(Models.User user)
        {
        }

        // DELETE: api/user/{id}
        public void DeleteUser(int id)
        {
        }
    }
}
