using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Player
    {
        public int albumID { get; set; }
        public string songName { get; set; }
        public string artistName { get; set; }
        public string albumTitle { get; set; }
    }
}