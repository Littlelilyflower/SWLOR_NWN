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
    
    public partial class DiscordChatQueue
    {
        public int DiscordChatQueueID { get; set; }
        public string SenderName { get; set; }
        public string Message { get; set; }
        public System.DateTime DateSent { get; set; }
        public Nullable<System.DateTime> DatePosted { get; set; }
        public Nullable<System.DateTime> DateForRetry { get; set; }
        public string ResponseContent { get; set; }
        public int RetryAttempts { get; set; }
        public string SenderAccountName { get; set; }
        public string SenderCDKey { get; set; }
    }
}
