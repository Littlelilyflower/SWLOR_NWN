﻿
using SWLOR.Game.Server.Event.Conversation.RimerCards;
using SWLOR.Game.Server.NWN;
using SWLOR.Game.Server.Service;
using SWLOR.Game.Server.ValueObject;

// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    internal static class rimer_cpu_4
#pragma warning restore IDE1006 // Naming Styles
    {
        public static int Main()
        {
            using (new Profiler(nameof(rimer_cpu_4)))
            {
                RimerDeckType deck = RandomService.Random(4) <= 3 ? RimerDeckType.FastCreatures : RimerDeckType.BigCreatures;
                RimerCPU.ConfigureGameSettings(_.OBJECT_SELF, deck, RimerAIDifficulty.Normal);
                return 0;
            }
        }
    }
}
