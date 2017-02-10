using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finovex.Models
{
    public class UpdateHistory
    {
        public string filename { get; set; }
        public string prevfilename { get; set; }
        public DateTime updateTimestamp { get; set; }
        public int userid { get; set; }
    }
}