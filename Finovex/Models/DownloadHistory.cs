using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finovex.Models
{
    public class DownloadHistory
    {
        public string filename { get; set; }
        public DateTime downloadTimestamp { get; set; }
        public int userid { get; set; }
    }
}