﻿using NWN;
using SWLOR.Game.Server.Data.Entity;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.Perk;
using SWLOR.Game.Server.Service;
using System;
using SWLOR.Game.Server.ValueObject;
using PerkExecutionType = SWLOR.Game.Server.Enumeration.PerkExecutionType;

namespace SWLOR.Game.Server.Event.Delayed
{
    public class FinishAbilityUse : IRegisteredEvent
    {
        public bool Run(params object[] args)
        {
            using (new Profiler(nameof(FinishAbilityUse)))
            {
                Console.WriteLine("Firing FinishAbilityUse");

                // These arguments are sent from the AbilityService's ActivateAbility method.
                NWCreature activator = (NWCreature) args[0];
                string spellUUID = Convert.ToString(args[1]);
                int perkID = (int) args[2];
                NWObject target = (NWObject) args[3];
                int pcPerkLevel = (int) args[4];
                int spellTier = (int) args[5];
                float armorPenalty = (float) args[6];

                Console.WriteLine("FinishAbilityUse perkID = " + perkID);

                // Get the relevant perk information from the database.
                Data.Entity.Perk dbPerk = DataService.Single<Data.Entity.Perk>(x => x.ID == perkID);

                // The execution type determines how the perk behaves and the rules surrounding it.
                PerkExecutionType executionType = dbPerk.ExecutionTypeID;

                // Get the class which handles this perk's behaviour.
                IPerkHandler perk = PerkService.GetPerkHandler(perkID);

                // Pull back cooldown information.
                int? cooldownID = perk.CooldownCategoryID(activator, dbPerk.CooldownCategoryID, spellTier);
                CooldownCategory cooldown = cooldownID == null ? null : DataService.SingleOrDefault<CooldownCategory>(x => x.ID == cooldownID);

                // If the activator interrupted the spell or died, we can bail out early.
                if (activator.GetLocalInt(spellUUID) == (int) SpellStatusType.Interrupted || // Moved during casting
                    activator.CurrentHP < 0 || activator.IsDead) // Or is dead/dying
                {
                    activator.DeleteLocalInt(spellUUID);
                    return false;
                }

                // Remove the temporary UUID which is tracking this spell cast.
                activator.DeleteLocalInt(spellUUID);

                // Force Abilities, Combat Abilities, Stances, and Concentration Abilities
                if (executionType == PerkExecutionType.ForceAbility ||
                    executionType == PerkExecutionType.CombatAbility ||
                    executionType == PerkExecutionType.Stance ||
                    executionType == PerkExecutionType.ConcentrationAbility)
                {
                    // Run the impact script.
                    perk.OnImpact(activator, target, pcPerkLevel, spellTier);

                    // If an animation is specified for this perk, play it now.
                    if (dbPerk.CastAnimationID != null && dbPerk.CastAnimationID > 0)
                    {
                        activator.AssignCommand(() => { _.ActionPlayAnimation((int) dbPerk.CastAnimationID, 1f, 1f); });
                    }

                    // If the target is an NPC, assign enmity towards this creature for that NPC.
                    if (target.IsNPC)
                    {
                        AbilityService.ApplyEnmity(activator, target.Object, dbPerk);
                    }
                }

                // Adjust creature's current FP, if necessary.
                // Adjust FP only if spell cost > 0
                PerkFeat perkFeat = DataService.Single<PerkFeat>(x => x.PerkID == perkID && x.PerkLevelUnlocked == spellTier);
                int fpCost = perk.FPCost(activator, perkFeat.BaseFPCost, spellTier);

                if (fpCost > 0)
                {
                    int currentFP = AbilityService.GetCurrentFP(activator);
                    int maxFP = AbilityService.GetMaxFP(activator);
                    currentFP -= fpCost;
                    AbilityService.SetCurrentFP(activator, currentFP);
                    activator.SendMessage(ColorTokenService.Custom("FP: " + currentFP + " / " + maxFP, 32, 223, 219));
                }

                // Notify activator of concentration ability change and also update it in the DB.
                if (executionType == PerkExecutionType.ConcentrationAbility)
                {
                    AbilityService.StartConcentrationEffect(activator, perkID, spellTier);
                    activator.SendMessage("Concentration ability activated: " + dbPerk.Name);

                    // The Skill Increase effect icon and name has been overwritten. Apply the effect to the player now.
                    // This doesn't do anything - it simply gives a visual cue that the player has an active concentration effect.
                    _.ApplyEffectToObject(_.DURATION_TYPE_PERMANENT, _.EffectSkillIncrease(_.SKILL_USE_MAGIC_DEVICE, 1), activator);
                }
                
                // Handle applying cooldowns, if necessary.
                if (cooldown != null)
                {
                    AbilityService.ApplyCooldown(activator, cooldown, perk, spellTier, armorPenalty);
                }
                
                // Mark the creature as no longer busy.
                activator.IsBusy = false;

                // Mark the spell cast as complete.
                activator.SetLocalInt(spellUUID, (int) SpellStatusType.Completed);
                
                return true;
            }
        }
    }
}
