//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Finovex
{
    using System;
    using System.Collections.Generic;
    
    public partial class UploadHistory
    {
        public int userid { get; set; }
        public System.DateTime uploadTimestamp { get; set; }
        public string filename { get; set; }
    
        public virtual user user { get; set; }
    }
}
