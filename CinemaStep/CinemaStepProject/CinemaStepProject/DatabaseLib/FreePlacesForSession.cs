//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class FreePlacesForSession
    {
        public int id { get; set; }
        public Nullable<int> id_hall { get; set; }
        public Nullable<int> place_number { get; set; }
        public Nullable<bool> is_empty { get; set; }
    
        public virtual Hall Hall { get; set; }

        public override string ToString()
        {
            return place_number.ToString();
        }
    }
}
