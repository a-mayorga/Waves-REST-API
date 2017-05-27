using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace WebAPI.Controllers
{
    public class SongController : ApiController
    {
        // GET: api/song
        public List<Models.Song> GetAllSongs()
        {
            List<Models.Song> songs = new List<Models.Song>();
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM song;";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Models.Song song = new Models.Song()
                    {
                        id = int.Parse(reader["songID"].ToString()),
                        name = reader["songName"].ToString(),
                        artist = reader["songArtist"].ToString(),
                        album = reader["songAlbum"].ToString(),
                        genre = reader["songGenre"].ToString(),
                        songNumber = int.Parse(reader["songNumber"].ToString())
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

        // GET: api/song/{id}
        public Models.Song GetSong(int id)
        {
            Models.Song song = new Models.Song();

            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = String.Format("SELECT * FROM song WHERE songID = {0};", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    song.id = int.Parse(reader["songID"].ToString());
                    song.name = reader["songName"].ToString();
                    song.artist = reader["songArtist"].ToString();
                    song.album = reader["songAlbum"].ToString();
                    song.genre = reader["songGenre"].ToString();
                    song.songNumber = int.Parse(reader["songNumber"].ToString());
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

        // POST: api/song
        public string PostAlbum(Models.Song song)
        {
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                string query = String.Format("INSERT INTO song(songName, songArtist, songAlbum, songGenre, songNumber) VALUES('{0}', {1}, {2}, {3}, {4});", song.name, int.Parse(song.artist), int.Parse(song.album), int.Parse(song.genre), song.songNumber);

                MySqlCommand insert = new MySqlCommand(query, conn);
                insert.ExecuteNonQuery();

                return "Canción agregada exitosamente";
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

        // PUT: api/song
        public void PutSong(Models.Song song)
        {
        }

        // DELETE: api/song/{id}
        public void DeleteSong(int id)
        {
        }
    }
}
