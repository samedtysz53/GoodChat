//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace özel_projem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mess
    {
        public int id { get; set; }
        public Nullable<int> Userid { get; set; }
        public Nullable<int> fid { get; set; }
        public string SetMessages { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Account Account1 { get; set; }
    }
}
