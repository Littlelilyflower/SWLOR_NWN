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
    
    public partial class PCPerkRefund
    {
        public int PCPerkRefundID { get; set; }
        public string PlayerID { get; set; }
        public int PerkID { get; set; }
        public int Level { get; set; }
        public System.DateTime DateAcquired { get; set; }
        public System.DateTime DateRefunded { get; set; }
    
        public virtual Perk Perk { get; set; }
        public virtual PlayerCharacter PlayerCharacter { get; set; }
    }
}
