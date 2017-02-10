using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finovex.Models
{
    public class DeleteHistory
    {
        public string filename { get; set; }
        public DateTime deleteTimestamp { get; set; }
        public int userid { get; set; }
    }
}