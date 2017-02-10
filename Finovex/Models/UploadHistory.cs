using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finovex.Models
{
    public class UploadHistory
    {
        public string filename { get; set; }
        public DateTime uploadTimestamp { get; set; }
        public int userid { get; set; }
    }
}