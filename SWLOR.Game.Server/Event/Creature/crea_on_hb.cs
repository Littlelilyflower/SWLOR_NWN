﻿using SWLOR.Game.Server;
using SWLOR.Game.Server.Messaging;
using SWLOR.Game.Server.NWN.Events.Creature;


// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    public class crea_on_hb
#pragma warning restore IDE1006 // Naming Styles
    {
        public void Main()
        {
            MessageHub.Instance.Publish(new OnCreatureHeartbeat());
        }
    }
}
