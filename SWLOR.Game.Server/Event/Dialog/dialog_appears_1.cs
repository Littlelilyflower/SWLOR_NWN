﻿using SWLOR.Game.Server;

using SWLOR.Game.Server.Service;

// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    public class dialog_appears_1
#pragma warning restore IDE1006 // Naming Styles
    {
        // ReSharper disable once UnusedMember.Local
        public int Main()
        {
            return DialogService.OnAppearsWhen(2, 1) ? 1 : 0;
        }
    }
}