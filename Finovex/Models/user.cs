using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finovex.Models
{
    public class user
    {
        public int userid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime create_time { get; set; }
        public bool active { get; set; }
    }
}