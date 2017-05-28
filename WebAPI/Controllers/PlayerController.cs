using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class PlayerController : ApiController
    {

        // GET: api/player/{id}
        public List<Models.Player> GetSongData(int id)
        {
            List<Models.Player> song = new List<Models.Player>();
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = String.Format("SELECT al.albumID,s.songName,art.artistName,al.albumTitle FROM song s,artist art,album al WHERE s.songArtist = art.artistID AND s.songAlbum = al.albumID AND s.songID = '{0}';", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    song.Add(new Models.Player()
                    {
                        albumID = int.Parse(reader["albumID"].ToString()),
                        songName = reader["songName"].ToString(),
                        artistName = reader["artistName"].ToString(),
                        albumTitle = reader["albumTitle"].ToString(),
                    });
                }
                
                return song;
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


        // POST: api/Player
        public void Post([FromBody]string value)
        {
            
        }

        // PUT: api/Player/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Player/5
        public void Delete(int id)
        {
        }
    }
}
