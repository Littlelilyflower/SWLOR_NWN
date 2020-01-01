﻿using System.Linq;
using NWN;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.NWScript.Enumerations;
using SWLOR.Game.Server.Service;

using static NWN._;

namespace SWLOR.Game.Server.Perk.MartialArts
{
    public class Chi: IPerkHandler
    {
        public PerkType PerkType => PerkType.Chi;
        public string Name => "Chi";
        public bool IsActive => true;
        public string Description => "Restores the user's HP. At rank 7, this ability also heals all party members within area of effect.";
        public PerkCategoryType Category => PerkCategoryType.MartialArts;
        public PerkCooldownGroup CooldownGroup => PerkCooldownGroup.Chi;
        public PerkExecutionType ExecutionType => PerkExecutionType.CombatAbility;
        public bool IsTargetSelfOnly => true;
        public int Enmity => 0;
        public EnmityAdjustmentRuleType EnmityAdjustmentType => EnmityAdjustmentRuleType.None;
        public ForceBalanceType ForceBalanceType => ForceBalanceType.Universal;
        public Animation CastAnimation => Animation.Invalid;

        public string CanCastSpell(NWCreature oPC, NWObject oTarget, int spellTier)
        {
            return string.Empty;
        }
        
        public int FPCost(NWCreature oPC, int baseFPCost, int spellTier)
        {
            return baseFPCost;
        }

        public float CastingTime(NWCreature oPC, int spellTier)
        {
            return 0f;
        }

        public float CooldownTime(NWCreature oPC, float baseCooldownTime, int spellTier)
        {
            return baseCooldownTime;
        }

        public void OnImpact(NWCreature creature, NWObject target, int perkLevel, int spellTier)
        {
            int wisdom = creature.WisdomModifier;
            int constitution = creature.ConstitutionModifier;
            int min = 1 + wisdom / 2 + constitution / 3;

            // Rank 7 and up: AOE heal party members
            if (perkLevel >= 7)
            {
                var members = creature.PartyMembers.Where(x => Equals(x, creature) || 
                                                             _.GetDistanceBetween(creature, x) <= 10.0f && x.CurrentHP < x.MaxHP);
                foreach (var member in members)
                {
                    DoHeal(member, perkLevel, min);
                }
            }
            else
            {
                DoHeal(target, perkLevel, min);
            }
        }

        private void DoHeal(NWObject target, int perkLevel, int minimum)
        {
            float percentage = perkLevel * 0.10f;
            int heal = (int)(target.MaxHP * percentage);

            heal = RandomService.Random(minimum, heal);

            _.ApplyEffectToObject(DurationType.Instant, _.EffectHeal(heal), target);
            _.ApplyEffectToObject(DurationType.Instant, _.EffectVisualEffect(Vfx.Vfx_Imp_Healing_G), target);
        }

        public void OnPurchased(NWCreature creature, int newLevel)
        {
        }

        public void OnRemoved(NWCreature creature)
        {
        }

        public void OnItemEquipped(NWCreature creature, NWItem oItem)
        {
        }

        public void OnItemUnequipped(NWCreature creature, NWItem oItem)
        {
        }

        public void OnCustomEnmityRule(NWCreature creature, int amount)
        {
        }

        public bool IsHostile()
        {
            return false;
        }

        public void OnConcentrationTick(NWCreature creature, NWObject target, int perkLevel, int tick)
        {
            
        }
    }
}
