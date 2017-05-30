using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Song
    {
        public int id { get; set; }
        public string name { get; set; }
        public string artist { get; set; }
        public string album { get; set; }
        public string albumYear { get; set; }
        public int albumID { get; set; }
        public string genre { get; set; }
        public int songNumber { get; set; }
    }
}