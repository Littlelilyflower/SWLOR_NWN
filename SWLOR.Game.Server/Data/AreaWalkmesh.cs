//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SWLOR.Game.Server.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class AreaWalkmesh
    {
        public int AreaWalkmeshID { get; set; }
        public string AreaID { get; set; }
        public Nullable<double> LocationX { get; set; }
        public Nullable<double> LocationY { get; set; }
        public double LocationZ { get; set; }
    
        public virtual Area Area { get; set; }
    }
}
