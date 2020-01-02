﻿using System.Collections.Generic;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.NWScript.Enumerations;

namespace SWLOR.Game.Server.Perk
{
    public interface IPerk
    {
        PerkType PerkType { get; }
        string Name { get; }
        bool IsActive { get; }
        string Description { get; }
        PerkCategoryType Category { get; }
        PerkCooldownGroup CooldownGroup { get; }
        PerkExecutionType ExecutionType { get; }
        bool IsTargetSelfOnly { get; }
        int Enmity { get; }
        EnmityAdjustmentRuleType EnmityAdjustmentType { get; }
        ForceBalanceType ForceBalanceType { get; }
        Animation CastAnimation { get; }

        string CanCastSpell(NWCreature creature, NWObject oTarget, int spellTier);
        int FPCost(NWCreature creature, int baseFPCost, int spellTier);
        float CastingTime(NWCreature creature, int spellTier);
        float CooldownTime(NWCreature creature, float baseCooldownTime, int spellTier);
        void OnImpact(NWCreature creature, NWObject target, int perkLevel, int spellTier);
        void OnConcentrationTick(NWCreature creature, NWObject target, int spellTier, int tick);
        void OnPurchased(NWCreature creature, int newLevel);
        void OnRemoved(NWCreature creature);
        void OnItemEquipped(NWCreature creature, NWItem oItem);
        void OnItemUnequipped(NWCreature creature, NWItem oItem);
        void OnCustomEnmityRule(NWCreature creature, int amount);
        bool IsHostile();

        Dictionary<int, PerkLevel> PerkLevels { get; }
    }
}