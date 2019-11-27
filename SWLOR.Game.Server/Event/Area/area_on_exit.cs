﻿using SWLOR.Game.Server.Event.Area;
using SWLOR.Game.Server.Messaging;

// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    public class area_on_exit
#pragma warning restore IDE1006 // Naming Styles
    {
        public void Main()
        {
            MessageHub.Instance.Publish(new OnAreaExit());
        }
    }
}
