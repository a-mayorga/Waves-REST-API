using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;


namespace WebAPI.Controllers
{
    public class DeleteController : ApiController
    {
        // GET: api/Delete
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Delete/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Delete
        public string Post(Models.Library library)
        {
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = String.Format("SELECT libraryID FROM library WHERE userID = {0};", library.userID);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    library.libraryID = int.Parse(reader["libraryID"].ToString());
                }

                conn.Close();
                conn.Open();

                string query = String.Format("DELETE FROM library_content WHERE libraryID = {0} AND songID={1};", library.libraryID, library.songID);

                MySqlCommand insert = new MySqlCommand(query, conn);
                insert.ExecuteNonQuery();

                return "Canción Eliminada de tu biblioteca";
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


        // PUT: api/Delete/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Delete/5
        public void Delete(int id)
        {
        }
    }
}
