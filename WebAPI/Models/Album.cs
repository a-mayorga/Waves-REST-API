using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Album
    {
        public int id { get; set; }
        public string title { get; set; }
        public string year { get; set; }
        public string genre { get; set; }
        public int songs { get; set; }
    }
}