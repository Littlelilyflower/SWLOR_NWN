﻿using SWLOR.Game.Server.Core.NWScript;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Legacy.Enumeration;
using SWLOR.Game.Server.Legacy.GameObject;
using SWLOR.Game.Server.Legacy.Service;
using SWLOR.Game.Server.Service;
using PerkType = SWLOR.Game.Server.Legacy.Enumeration.PerkType;
using SkillType = SWLOR.Game.Server.Legacy.Enumeration.SkillType;

namespace SWLOR.Game.Server.Legacy.Scripts.Placeable.ScavengePoint
{
    public class OnOpened: IScript
    {
        public void SubscribeEvents()
        {
        }

        public void UnsubscribeEvents()
        {
        }

        public void Main()
        {
            NWPlaceable point = (NWScript.OBJECT_SELF);
            NWPlayer oPC = (NWScript.GetLastOpenedBy());
            if (!oPC.IsPlayer) return;

            var effectiveStats = PlayerStatService.GetPlayerItemEffectiveStats(oPC);
            const int baseChanceToFullyHarvest = 50;
            
            var hasBeenSearched = point.GetLocalInt("SCAVENGE_POINT_FULLY_HARVESTED") == 1;
            if (hasBeenSearched)
            {
                oPC.SendMessage("There's nothing left to harvest here...");
                return;
            }


            if (!oPC.IsPlayer && !oPC.IsDM) return;
            var rank = SkillService.GetPCSkillRank(oPC, SkillType.Scavenging);
            var lootTableID = point.GetLocalInt("SCAVENGE_POINT_LOOT_TABLE_ID");
            var level = point.GetLocalInt("SCAVENGE_POINT_LEVEL");
            var delta = level - rank;

            if (delta > 8)
            {
                oPC.SendMessage("You aren't skilled enough to scavenge through this. (Required Level: " + (level - 8) + ")");
                oPC.AssignCommand(() => NWScript.ActionInteractObject(point.Object));
                return;
            }

            var dc = 6 + delta;
            if (dc <= 4) dc = 4;
            var searchAttempts = 1 + CalculateSearchAttempts(oPC);

            var luck = PerkService.GetCreaturePerkLevel(oPC, PerkType.Lucky) + effectiveStats.Luck;
            if (SWLOR.Game.Server.Service.Random.Next(100) + 1 <= luck / 2)
            {
                dc--;
            }

            oPC.AssignCommand(() => NWScript.ActionPlayAnimation(Animation.LoopingGetLow, 1.0f, 2.0f));

            for (var attempt = 1; attempt <= searchAttempts; attempt++)
            {
                var roll = SWLOR.Game.Server.Service.Random.Next(20) + 1;
                if (roll >= dc)
                {
                    oPC.FloatingText(ColorToken.SkillCheck("Search: *success*: (" + roll + " vs. DC: " + dc + ")"));
                    var spawnItem = LootService.PickRandomItemFromLootTable(lootTableID);

                    if (spawnItem == null)
                    {
                        return;
                    }

                    if (!string.IsNullOrWhiteSpace(spawnItem.Resref) && spawnItem.Quantity > 0)
                    {
                        NWScript.CreateItemOnObject(spawnItem.Resref, point.Object, spawnItem.Quantity);
                    }

                    var xp = SkillService.CalculateRegisteredSkillLevelAdjustedXP(200, level, rank);
                    SkillService.GiveSkillXP(oPC, SkillType.Scavenging, (int)xp);
                }
                else
                {
                    oPC.FloatingText(ColorToken.SkillCheck("Search: *failure*: (" + roll + " vs. DC: " + dc + ")"));

                    var xp = SkillService.CalculateRegisteredSkillLevelAdjustedXP(50, level, rank);
                    SkillService.GiveSkillXP(oPC, SkillType.Scavenging, (int)xp);
                }
                dc += SWLOR.Game.Server.Service.Random.Next(3) + 1;
            }
            
            // Chance to destroy the scavenge point.
            var chanceToFullyHarvest = baseChanceToFullyHarvest - (PerkService.GetCreaturePerkLevel(oPC, PerkType.CarefulScavenger) * 5);
            
            if (chanceToFullyHarvest <= 5) chanceToFullyHarvest = 5;

            point.SetLocalInt("SCAVENGE_POINT_FULLY_HARVESTED", 1);
            oPC.SendMessage("This resource has been fully harvested...");

            point.SetLocalInt("SCAVENGE_POINT_DESPAWN_TICKS", 30);
        }


        private int CalculateSearchAttempts(NWPlayer oPC)
        {
            var perkLevel = PerkService.GetCreaturePerkLevel(oPC, PerkType.ScavengingExpert);

            var numberOfSearches = 0;
            var attempt1Chance = 0;
            var attempt2Chance = 0;

            switch (perkLevel)
            {
                case 1: attempt1Chance = 10; break;
                case 2: attempt1Chance = 20; break;
                case 3: attempt1Chance = 30; break;
                case 4: attempt1Chance = 40; break;
                case 5: attempt1Chance = 50; break;

                case 6:
                    attempt1Chance = 50;
                    attempt2Chance = 10;
                    break;
                case 7:
                    attempt1Chance = 50;
                    attempt2Chance = 20;
                    break;
                case 8:
                    attempt1Chance = 50;
                    attempt2Chance = 30;
                    break;
                case 9:
                    attempt1Chance = 50;
                    attempt2Chance = 40;
                    break;
                case 10:
                    attempt1Chance = 50;
                    attempt2Chance = 50;
                    break;
            }

            if (SWLOR.Game.Server.Service.Random.Next(100) + 1 <= attempt1Chance)
            {
                numberOfSearches++;
            }
            if (SWLOR.Game.Server.Service.Random.Next(100) + 1 <= attempt2Chance)
            {
                numberOfSearches++;
            }

            var background = (int)oPC.Class1;
            if (background == (int)BackgroundType.Scavenger)
                numberOfSearches++;

            return numberOfSearches;
        }
    }
}
