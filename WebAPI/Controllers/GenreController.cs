using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace WebAPI.Controllers
{
    public class GenreController : ApiController
    {
        // GET: api/genre
        public List<Models.Genre> GetAllGenres()
        {
            List<Models.Genre> genres = new List<Models.Genre>();
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM genre;";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    genres.Add(new Models.Genre()
                    {
                        id = int.Parse(reader["genreID"].ToString()),
                        name = reader["genreName"].ToString()
                    });
                }

                return genres;
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

        // GET: api/genre/{id}
        public Models.Genre GetGenre(int id)
        {
            Models.Genre genre = new Models.Genre();

            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = String.Format("SELECT * FROM genre WHERE genreID = {0};", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    genre.id = int.Parse(reader["genreID"].ToString());
                    genre.name = reader["genreName"].ToString();
                }

                return genre;
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

        // POST: api/genre
        public string PostGenre(Models.Genre genre)
        {
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                string query = String.Format("INSERT INTO genre(genreName) VALUES('{0}');", genre.name);

                MySqlCommand insert = new MySqlCommand(query, conn);
                insert.ExecuteNonQuery();

                return "Género agregado exitosamente";
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

        // PUT: api/genre
        public void PutGenre(Models.Genre genre)
        {
        }

        // DELETE: api/genre/{id}
        public void DeleteGenre(int id)
        {
        }
    }
}
