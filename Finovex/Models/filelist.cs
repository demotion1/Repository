using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Finovex.Models
{
    public class filelist
    {
        public int fileid { get; set; }
        public string filename { get; set; }
        public DateTime creationdDate { get; set; }
        public DateTime modificationDate { get; set; }
        public string filesize { get; set; }
        public string mimeType { get; set; }
        public int fileUser { get; set; }
    }
}