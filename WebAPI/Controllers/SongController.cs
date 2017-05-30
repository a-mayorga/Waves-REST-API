using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/song")]
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
                cmd.CommandText = "SELECT s.songID, s.songName, ar.artistName, s.songAlbum, al.albumTitle, gen.genreName FROM song s, artist ar, album al, genre gen WHERE s.songArtist = ar.artistID AND s.songAlbum = al.albumID AND s.songGenre = gen.genreID;";
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Models.Song song = new Models.Song()
                    {
                        id = int.Parse(reader["songID"].ToString()),
                        name = reader["songName"].ToString(),
                        artist = reader["artistName"].ToString(),
                        album = reader["albumTitle"].ToString(),
                        albumID = int.Parse(reader["songAlbum"].ToString()),
                        genre = reader["genreName"].ToString(),
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

        // GET: api/song/artist/{id}
        [HttpGet]        
        [Route("artist/{idArtist:int}")]
        public List<Models.Song> GetSongsByArtist(int idArtist)
        {
            List<Models.Song> songs = new List<Models.Song>();
            MySqlConnection conn = Connection.GetConnection();

            try
            {
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = String.Format("SELECT s.songID, s.songName, s.songAlbum, ar.artistName, al.albumTitle, al.albumYear, gen.genreName, s.songNumber FROM song s, artist ar, album al, genre gen WHERE s.songArtist = {0} AND s.songArtist = ar.artistID AND s.songAlbum = al.albumID AND s.songGenre = gen.genreID;", idArtist);
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
