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
        // GET: api/Library
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Library/5
        public List<Models.Library> GetLibrary(int id)
        {
            List<Models.Library> librarySongs = new List<Models.Library>();

            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = String.Format("SELECT lib.libraryID,lib.libraryContentID,s.songID,s.songAlbum,s.songName,a.artistName,al.albumTitle,gen.genreName FROM library_content libc,library lib,song s,album al,artist a,genre gen WHERE lib.userID = 3 AND lib.libraryID = libc.libraryID AND libc.songID = s.songID AND s.songArtist = a.artistID AND s.songAlbum = al.albumID AND s.songGenre = gen.genreID;", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Models.Library library = new Models.Library();
                    {
                        library.songID = int.Parse(reader["songID"].ToString());
                        library.albumID = int.Parse(reader["songAlbum"].ToString());
                        library.libraryID = int.Parse(reader["libraryID"].ToString());
                        library.libraryContentID = int.Parse(reader["libraryContentID"].ToString());
                        library.songName = reader["songName"].ToString();
                        library.artistName = reader["artistName"].ToString();
                        library.albumTitle = reader["albumTitle"].ToString();
                        library.genreName = reader["genreName"].ToString();
                    };

                    librarySongs.Add(library);
                }
             
                return librarySongs;
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

        // POST: api/Library
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Library/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Library/5
        public void Delete(int id)
        {
        }
    }
}
