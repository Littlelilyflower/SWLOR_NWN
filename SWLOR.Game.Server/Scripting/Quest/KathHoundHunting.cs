﻿using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Quest;

namespace SWLOR.Game.Server.Scripting.Quest
{
    public class KathHoundHunting: AbstractQuest
    {
        public KathHoundHunting()
        {
            CreateQuest(14, "Kath Hound Hunting", "k_hound_hunting")
                .AddObjectiveKillTarget(1, NPCGroup.ViscaraWildlandKathHounds, 7)
                .AddObjectiveTalkToNPC(2)

                .AddRewardGold(350)
                .AddRewardFame(FameRegion.VelesColony, 10);
        }
    }
}