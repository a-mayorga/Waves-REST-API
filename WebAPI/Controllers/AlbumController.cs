using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace WebAPI.Controllers
{
    public class AlbumController : ApiController
    {
        // GET: api/album
        public List<Models.Album> GetAllAlbums()
        {
            List<Models.Album> albums = new List<Models.Album>();
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT al.albumID,al.albumTitle,al.albumDate,gen.genreName FROM album al, genre gen WHERE al.albumGenre = gen.genreID;";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {                    
                    Models.Album album = new Models.Album()
                    {
                        id = int.Parse(reader["albumID"].ToString()),
                        title = reader["albumTitle"].ToString(),
                        year = reader["albumDate"].ToString(),
                        genre = reader["genreName"].ToString(),
                    };                    

                    albums.Add(album);
                }

                return albums;
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

        // GET: api/album/{id}
        public Models.Album GetAlbum(int id)
        {
            Models.Album album = new Models.Album();

            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = String.Format("SELECT * FROM album WHERE albumID = {0};", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    album.id = int.Parse(reader["albumID"].ToString());
                    album.title = reader["albumTitle"].ToString();
                    album.year = reader["albumYear"].ToString();
                    album.genre = reader["albumGenre"].ToString();
                    album.songs = int.Parse(reader["albumSongs"].ToString());
                }

                return album;
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

        // POST: api/album
        public string PostAlbum(Models.Album album)
        {
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                string query = String.Format("INSERT INTO album(albumTitle, albumYear, albumGenre, albumSongs) VALUES('{0}', '{1}', {2}, {3});", album.title, album.year, int.Parse(album.genre), album.songs);

                MySqlCommand insert = new MySqlCommand(query, conn);
                insert.ExecuteNonQuery();

                return "Album agregado exitosamente";
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

        // PUT: api/album
        public void PutAlbum(Models.Album album)
        {
        }

        // DELETE: api/album/{id}
        public void DeleteAlbum(int id)
        {
        }
    }
}
