﻿using SWLOR.Game.Server;
using SWLOR.Game.Server.Event.Module;
using SWLOR.Game.Server.Messaging;


// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    public static class mod_on_unacquire
#pragma warning restore IDE1006 // Naming Styles
    {
        // ReSharper disable once UnusedMember.Local
        public static void Main()
        {
            // Bioware default
            _.ExecuteScript("x2_mod_def_unaqu", NWGameObject.OBJECT_SELF);
            MessageHub.Instance.Publish(new OnModuleUnacquireItem());
        }
    }
}
