using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace WebAPI.Controllers
{
    public class LibraryController : ApiController
    {
        // GET: api/library
        public IEnumerable<string> GetLibrary()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/library/{id}
        public List<Models.Song> GetLibrary(int id)
        {
            List<Models.Song> songs = new List<Models.Song>();
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = String.Format("SELECT lib.libraryID, s.songID, s.songAlbum, s.songName, s.songNumber, s.songRoute, a.artistName, al.albumTitle, al.albumYear, gen.genreName FROM library_content libc, library lib, song s, album al, artist a, genre gen WHERE lib.userID = {0} AND lib.libraryID = libc.libraryID AND libc.songID = s.songID AND s.songArtist = a.artistID AND s.songAlbum = al.albumID AND s.songGenre = gen.genreID;", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Models.Song song = new Models.Song()
                    {
                        id = int.Parse(reader["songID"].ToString()),
                        name = reader["songName"].ToString(),
                        artist = reader["artistName"].ToString(),
                        album = reader["albumTitle"].ToString(),
                        albumYear = reader["albumYear"].ToString(),
                        albumID = int.Parse(reader["songAlbum"].ToString()),
                        genre = reader["genreName"].ToString(),
                        songNumber = int.Parse(reader["songNumber"].ToString()),
                        songRoute = reader["songRoute"].ToString()
                    };

                    songs.Add(song);
                }

                return songs;
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

        // POST: api/library
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

                string query = String.Format("INSERT INTO library_content(libraryID, songID) VALUES({0}, {1});", library.libraryID, library.songID);

                MySqlCommand insert = new MySqlCommand(query, conn);
                insert.ExecuteNonQuery();

                return "Canción agregada a tu biblioteca";
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
       
        // PUT: api/library
        public void Put(Models.Song song)
        {
        }

        // DELETE: api/library/{id}
        public void Delete(int id)
        {
        }
    }
}
