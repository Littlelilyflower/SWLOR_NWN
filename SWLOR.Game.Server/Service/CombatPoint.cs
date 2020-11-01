﻿using System;
using System.Collections.Generic;
using System.Linq;
using SWLOR.Game.Server.Core;
using SWLOR.Game.Server.Core.NWScript.Enum;
using SWLOR.Game.Server.Core.NWScript.Enum.Item;
using SWLOR.Game.Server.Entity;
using SWLOR.Game.Server.Enumeration;

namespace SWLOR.Game.Server.Service
{
    public static class CombatPoint
    {
        /// <summary>
        /// Tracks the combat points earned by players during combat.
        /// </summary>
        private static readonly Dictionary<uint, Dictionary<uint, Dictionary<SkillType, int>>> _creatureCombatPointTracker = new Dictionary<uint, Dictionary<uint, Dictionary<SkillType, int>>>();

        /// <summary>
        /// Tracks the combat point lists associated with a player back to a creature.
        /// </summary>
        private static readonly Dictionary<uint, HashSet<uint>> _playerToCreatureTracker = new Dictionary<uint, HashSet<uint>>();

        /// <summary>
        /// Tracks the mapping of right hand weapon + armor to a specific skill.
        /// </summary>
        private static readonly Dictionary<Tuple<BaseItem, ArmorType>, SkillType> _weaponAndArmorSkillMapping = new Dictionary<Tuple<BaseItem, ArmorType>, SkillType>();

        /// <summary>
        /// Adds mappings for a right hand weapon + armor to a specific skill.
        /// These skills receive XP in a similar manner to armor upon killing enemies.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void MapWeaponAndArmorToSkills()
        {
            // Knight: Longsword + Heavy = Chivalry
            _weaponAndArmorSkillMapping[new Tuple<BaseItem, ArmorType>(BaseItem.Longsword, ArmorType.Heavy)] = SkillType.Chivalry;
            // Monk: Knuckles + Light = Chi
            _weaponAndArmorSkillMapping[new Tuple<BaseItem, ArmorType>(BaseItem.Gloves, ArmorType.Light)] = SkillType.Chi;
            // Thief: Dagger + Light = Thievery
            _weaponAndArmorSkillMapping[new Tuple<BaseItem, ArmorType>(BaseItem.Dagger, ArmorType.Light)] = SkillType.Thievery;
            // Black Mage: Staff + Mystic = Black Magic
            _weaponAndArmorSkillMapping[new Tuple<BaseItem, ArmorType>(BaseItem.QuarterStaff, ArmorType.Mystic)] = SkillType.BlackMagic;
            // White Mage: Rod + Mystic = White Magic
            _weaponAndArmorSkillMapping[new Tuple<BaseItem, ArmorType>(BaseItem.LightMace, ArmorType.Mystic)] = SkillType.WhiteMagic;
            // Red Mage: Rapier + Mystic = Red Magic
            _weaponAndArmorSkillMapping[new Tuple<BaseItem, ArmorType>(BaseItem.Rapier, ArmorType.Mystic)] = SkillType.RedMagic;
            // Ninja: Katana + Light = Ninjitsu
            _weaponAndArmorSkillMapping[new Tuple<BaseItem, ArmorType>(BaseItem.Katana, ArmorType.Light)] = SkillType.Ninjitsu;
            // Specialist: Gunblade + Heavy = Swordplay
            _weaponAndArmorSkillMapping[new Tuple<BaseItem, ArmorType>(BaseItem.Gunblade, ArmorType.Heavy)] = SkillType.Swordplay;
            // Sniper: Rifle + Light = Marksmanship
            _weaponAndArmorSkillMapping[new Tuple<BaseItem, ArmorType>(BaseItem.Rifle, ArmorType.Light)] = SkillType.Marksmanship;
            // Dark Knight: Greatsword + Heavy = Darkness
            _weaponAndArmorSkillMapping[new Tuple<BaseItem, ArmorType>(BaseItem.GreatSword, ArmorType.Heavy)] = SkillType.Darkness;
        }

        /// <summary>
        /// Adds a combat point to a given NPC creature for a given player and skill type.
        /// </summary>
        [NWNEventHandler("item_on_hit")]
        public static void OnHitCastSpell()
        {
            var player = OBJECT_SELF;
            if (!GetIsPC(player) || GetIsDM(player) || !GetIsObjectValid(player)) return;

            var item = GetSpellCastItem();
            var baseItemType = GetBaseItemType(item);
            var target = GetSpellTargetObject();
            if (GetIsPC(target) || GetIsDM(target)) return;

            var skill = Skill.GetSkillTypeByBaseItem(baseItemType);
            if (skill == SkillType.Invalid) return;

            AddCombatPoint(player, target, skill);
        }

        /// <summary>
        /// When a creature dies, skill XP is given to all players who contributed during battle.
        /// Then, those combat points are cleared out.
        /// </summary>
        [NWNEventHandler("crea_death")]
        public static void OnCreatureDeath()
        {
            // Clears the combat point cache information for an NPC and all player associated.
            static void CleanUpCombatPoints()
            {
                var npc = OBJECT_SELF;

                if (_creatureCombatPointTracker.ContainsKey(npc))
                {
                    // Remove references from the player-to-npc cache.
                    foreach (var playerId in _creatureCombatPointTracker[npc].Keys)
                    {
                        RemovePlayerToNPCReferenceFromCache(playerId, npc);
                    }

                    _creatureCombatPointTracker.Remove(npc);
                }
            }

            // When a creature is killed, XP is calculated based on the combat points earned by each player.
            // This XP is distributed into skills which received the highest usage during the battle.
            static void DistributeSkillXP()
            {
                // Calculates a rank range penalty. If there's a level difference greater than 10, a penalty is applied.
                static float CalculateRankRangePenalty(int highestSkillRank, int skillRank)
                {
                    int levelDifference = highestSkillRank - skillRank;
                    float levelDifferencePenalty = 1.0f;
                    if (levelDifference > 10)
                    {
                        levelDifferencePenalty = 1.0f - 0.05f * (levelDifference - 10);
                        if (levelDifferencePenalty < 0.20f) levelDifferencePenalty = 0.20f;
                    }

                    return levelDifferencePenalty;
                }

                // Calculates the number of armor points based on what the player currently has equipped.
                static (int, int, int) CalculateArmorPoints(uint player)
                {
                    var lightArmorPoints = 0;
                    var heavyArmorPoints = 0;
                    var mysticArmorPoints = 0;
                    for (int slot = 0; slot < NumberOfInventorySlots; slot++)
                    {
                        var item = GetItemInSlot((InventorySlot)slot, player);

                        for (var ip = GetFirstItemProperty(item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(item))
                        {
                            if (GetItemPropertyType(ip) != ItemPropertyType.ArmorType) continue;

                            var armorType = (ArmorType)GetItemPropertySubType(ip);
                            if (armorType == ArmorType.Light)
                                lightArmorPoints++;
                            else if (armorType == ArmorType.Heavy)
                                heavyArmorPoints++;
                            else if (armorType == ArmorType.Mystic)
                                mysticArmorPoints++;
                        }
                    }

                    return (lightArmorPoints, heavyArmorPoints, mysticArmorPoints);
                }

                // Applies an individual armor skill's XP portion.
                static int CalculateAdjustedXP(int highestRank, int baseXP, SkillType skillType, float totalPoints, int points, Dictionary<SkillType, PlayerSkill> playerSkills)
                {
                    var percentage = points / totalPoints;
                    var skillRank = playerSkills[skillType].Rank;
                    var rangePenalty = CalculateRankRangePenalty(highestRank, skillRank);
                    var adjustedXP = baseXP * percentage * rangePenalty;
                    return (int)adjustedXP;
                }

                // Applies an XP bonus to a support skill if the proper weapon and armor are chosen.
                static void ApplyJobBonusSkillXP(uint player, int highestRank, int baseXP, Dictionary<SkillType, PlayerSkill> playerSkills)
                {
                    // Get item in right hand or gloves, if empty.
                    var weapon = GetItemInSlot(InventorySlot.RightHand, player);
                    if (!GetIsObjectValid(weapon))
                        weapon = GetItemInSlot(InventorySlot.Arms, player);
                    var armor = GetItemInSlot(InventorySlot.Chest, player);
                    if (!GetIsObjectValid(armor)) return;

                    var armorType = ArmorType.Invalid;
                    for (var ip = GetFirstItemProperty(armor); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(armor))
                    {
                        if (GetItemPropertyType(ip) != ItemPropertyType.ArmorType) continue;

                        armorType = (ArmorType)GetItemPropertySubType(ip);
                        break;
                    }

                    var baseItem = GetBaseItemType(weapon);
                    var key = new Tuple<BaseItem, ArmorType>(baseItem, armorType);
                    if (!_weaponAndArmorSkillMapping.ContainsKey(key)) return;

                    var skill = _weaponAndArmorSkillMapping[key];
                    var xp = CalculateAdjustedXP(highestRank, baseXP, skill, 2, 1, playerSkills);
                    Skill.GiveSkillXP(player, skill, xp);
                }

                var npc = OBJECT_SELF;
                var combatPoints = _creatureCombatPointTracker.ContainsKey(npc) ? _creatureCombatPointTracker[npc] : null;
                if (combatPoints == null) return;

                var npcLevel = (int)(GetChallengeRating(npc) * 5);

                foreach (var (player, cpList) in combatPoints)
                {
                    if (!GetIsObjectValid(player) ||
                        !GetIsPC(player) ||
                        GetIsDM(player) ||
                        GetDistanceBetween(player, npc) > 40.0f ||
                        GetArea(player) != GetArea(npc))
                        continue;

                    var playerId = GetObjectUUID(player);
                    var dbPlayer = DB.Get<Player>(playerId);

                    // Filter the skills list down to just those with combat points (CP)
                    var skillsWithCP = dbPlayer.Skills.Where(x => cpList.ContainsKey(x.Key)).ToDictionary(x => x.Key, y => y.Value);
                    var highestRank = skillsWithCP
                        .OrderByDescending(o => o.Value.Rank)
                        .Select(s => s.Value.Rank)
                        .First();

                    // Base amount of XP is determined by the player's highest-leveled skill rank versus the creature's level.
                    var delta = npcLevel - highestRank;
                    var baseXP = Skill.GetDeltaXP(delta);
                    var totalPoints = (float)cpList.Sum(s => s.Value);

                    // Each skill used during combat receives a percentage of the baseXP amount depending on the number of combat points earned.
                    foreach (var (skillType, cp) in cpList)
                    {
                        var percentage = cp / totalPoints;
                        var skillRangePenalty = CalculateRankRangePenalty(highestRank, dbPlayer.Skills[skillType].Rank);
                        var adjustedXP = baseXP * percentage * skillRangePenalty;

                        Skill.GiveSkillXP(player, skillType, (int)adjustedXP);
                    }

                    // Each armor skill receives a static portion of XP based on how many pieces of each category are equipped.
                    var (lightArmorPoints, heavyArmorPoints, mysticArmorPoints) = CalculateArmorPoints(player);
                    totalPoints = lightArmorPoints + heavyArmorPoints + mysticArmorPoints;
                    if (totalPoints <= 0) continue;

                    var xp = CalculateAdjustedXP(highestRank, baseXP, SkillType.HeavyArmor, totalPoints, heavyArmorPoints, dbPlayer.Skills);
                    Skill.GiveSkillXP(player, SkillType.HeavyArmor, xp);

                    xp = CalculateAdjustedXP(highestRank, baseXP, SkillType.LightArmor, totalPoints, lightArmorPoints, dbPlayer.Skills);
                    Skill.GiveSkillXP(player, SkillType.LightArmor, xp);

                    xp = CalculateAdjustedXP(highestRank, baseXP, SkillType.MysticArmor, totalPoints, mysticArmorPoints, dbPlayer.Skills);
                    Skill.GiveSkillXP(player, SkillType.MysticArmor, xp);

                    ApplyJobBonusSkillXP(player, highestRank, baseXP, dbPlayer.Skills);
                }

            }

            DistributeSkillXP();
            CleanUpCombatPoints();
        }

        /// <summary>
        /// When a player leaves an area or the server, we need to remove all combat points
        /// that may be referenced to their character.
        /// </summary>
        [NWNEventHandler("mod_exit")]
        [NWNEventHandler("area_exit")]
        public static void OnPlayerExit()
        {
            var player = GetExitingObject();
            if (!GetIsPC(player) || GetIsDM(player)) return;

            ClearPlayerCombatPoints(player);
        }

        /// <summary>
        /// Removes all combat points for a player as well as all other cache references.
        /// </summary>
        /// <param name="player">The player whose cache data we're removing</param>
        private static void ClearPlayerCombatPoints(uint player)
        {
            if (!_playerToCreatureTracker.ContainsKey(player)) return;

            // Loop over all npcIds the player has linked to them.
            var npcIds = _playerToCreatureTracker[player];
            foreach (var npcId in npcIds)
            {
                if (!_creatureCombatPointTracker.ContainsKey(npcId)) continue;

                if (!_creatureCombatPointTracker[npcId].ContainsKey(player)) continue;
                _creatureCombatPointTracker[npcId].Remove(player);
            }

            _playerToCreatureTracker.Remove(player);
        }

        /// <summary>
        /// Adds a combat point for a player to an NPC on a skill.
        /// </summary>
        /// <param name="player">The player receiving the point</param>
        /// <param name="creature">The creature to associate this point with.</param>
        /// <param name="skill">The skill to associate with the point.</param>
        /// <param name="amount">The number of points to add.</param>
        public static void AddCombatPoint(uint player, uint creature, SkillType skill, int amount = 1)
        {
            if (!GetIsPC(player) || GetIsDM(player)) return;
            if (GetIsPC(creature) || GetIsDM(creature)) return;

            var tracker = _creatureCombatPointTracker.ContainsKey(creature) ?
                _creatureCombatPointTracker[creature] :
                new Dictionary<uint, Dictionary<SkillType, int>>();

            // Add an entry for this player if it doesn't exist.
            if (!tracker.ContainsKey(player))
            {
                tracker[player] = new Dictionary<SkillType, int>();
                AddPlayerToNPCReferenceToCache(player, creature);
            }

            // Add an entry for this skill if it doesn't exist.
            if (!tracker[player].ContainsKey(skill))
            {
                tracker[player][skill] = 0;
            }

            // Increment points for this player and skill.
            tracker[player][skill] += amount;

            _creatureCombatPointTracker[creature] = tracker;
        }

        /// <summary>
        /// Adds a combat point for a player to all NPCs s/he is currently tagged on.
        /// </summary>
        /// <param name="player">The player to receiving the point.</param>
        /// <param name="skill">The skill to associate with the point.</param>
        /// <param name="amount">The number of points to add.</param>
        public static void AddCombatPointToAllTagged(uint player, SkillType skill, int amount = 1)
        {
            if (!_playerToCreatureTracker.ContainsKey(player)) return;

            foreach (var creature in _playerToCreatureTracker[player])
            {
                AddCombatPoint(player, creature, skill, amount);
            }
        }

        /// <summary>
        /// Adds a player-to-npc reference to the cache.
        /// </summary>
        /// <param name="player">The player whose reference we're attaching.</param>
        /// <param name="creature">The creature we're referencing</param>
        private static void AddPlayerToNPCReferenceToCache(uint player, uint creature)
        {
            if (!_playerToCreatureTracker.ContainsKey(player))
            {
                _playerToCreatureTracker[player] = new HashSet<uint>();
            }

            if (!_playerToCreatureTracker[player].Contains(creature))
            {
                _playerToCreatureTracker[player].Add(creature);
            }
        }

        /// <summary>
        /// Removes a player-to-npc reference from the cache.
        /// </summary>
        /// <param name="player">The player whose reference we're removing</param>
        /// <param name="npc">The creature we're referencing</param>
        private static void RemovePlayerToNPCReferenceFromCache(uint player, uint npc)
        {
            if (!_playerToCreatureTracker.ContainsKey(player)) return;

            if (_playerToCreatureTracker[player].Contains(npc))
            {
                _playerToCreatureTracker[player].Remove(npc);
            }
        }
    }
}
