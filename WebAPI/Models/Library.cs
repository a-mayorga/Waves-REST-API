using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Library
    {
        public int libraryID { get; set; }
        public int libraryContentID { get; set; }
        public int songID { get; set; }
        public int albumID { get; set; }
        public string songName { get; set; }
        public string artistName { get; set; }
        public string albumTitle { get; set; }
        public string genreName { get; set; }
    }
}