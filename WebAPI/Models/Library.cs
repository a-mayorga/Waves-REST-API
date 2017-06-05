using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Library
    {
        public int libraryID { get; set; }
        public int userID { get; set; }
        public int songID { get; set; }
        public int libraryContentID { get; set; }
    }
}