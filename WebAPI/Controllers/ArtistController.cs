using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace WebAPI.Controllers
{
    public class ArtistController : ApiController
    {
        // GET: api/artist
        public List<Models.Artist> GetAllArtists()
        {
            List<Models.Artist> artists = new List<Models.Artist>();
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM artist;";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    artists.Add(new Models.Artist()
                    {
                        id = int.Parse(reader["artistID"].ToString()),
                        name = reader["artistName"].ToString()                       
                    });
                }

                return artists;
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

        // GET: api/artist/{id}
        public Models.Artist GetArtist(int id)
        {
            Models.Artist artist = new Models.Artist();

            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = String.Format("SELECT * FROM artist WHERE artistID = {0};", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    artist.id = int.Parse(reader["artistID"].ToString());
                    artist.name = reader["artistName"].ToString();                    
                }

                return artist;
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

        // POST: api/artist
        public string PostArtist(Models.Artist artist)
        {
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                string query = String.Format("INSERT INTO artist(artistName) VALUES('{0}');", artist.name);

                MySqlCommand insert = new MySqlCommand(query, conn);
                insert.ExecuteNonQuery();

                return "Artista agregado exitosamente";
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

        // PUT: api/artist
        public void PutArtist(Models.Artist artist)
        {
        }

        // DELETE: api/artist/{id}
        public void DeleteArtist(int id)
        {
        }
    }
}
