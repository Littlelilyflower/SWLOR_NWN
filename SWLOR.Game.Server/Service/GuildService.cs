﻿using System;
using System.Collections.Generic;
using System.Linq;
using SWLOR.Game.Server.Data.Entity;
using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Event.Module;
using SWLOR.Game.Server.Event.SWLOR;
using SWLOR.Game.Server.Extension;
using SWLOR.Game.Server.GameObject;
using SWLOR.Game.Server.Messaging;
using SWLOR.Game.Server.Quest.Contracts;
using SWLOR.Game.Server.Quest.Reward;
using static SWLOR.Game.Server.NWScript._;

namespace SWLOR.Game.Server.Service
{
    public static class GuildService
    {
        private static readonly Dictionary<GuildType, Dictionary<int, List<int>>> _questsByGuildTypeAndRank = new Dictionary<GuildType, Dictionary<int, List<int>>>();
        private static readonly Dictionary<GuildType, HashSet<int>> _currentlyOfferedQuests = new Dictionary<GuildType, HashSet<int>>();

        public static void SubscribeEvents()
        {
            MessageHub.Instance.Subscribe<OnModuleEnter>(a => OnModuleEnter());
            MessageHub.Instance.Subscribe<OnModuleLoad>(a => OnModuleLoad());
            MessageHub.Instance.Subscribe<OnModuleHeartbeat>(a => OnModuleHeartbeat());
            MessageHub.Instance.Subscribe<OnQuestCompleted>(a => OnQuestCompleted(a.Player, a.QuestID));
            MessageHub.Instance.Subscribe<OnQuestRegistered>(a => RegisterQuest(a.Quest));
        }

        /// <summary>
        /// Registers a quest into cache for quicker retrieval later.
        /// </summary>
        /// <param name="quest">The quest to register.</param>
        private static void RegisterQuest(IQuest quest)
        {
            if (quest.Guild == GuildType.Unknown) return;

            // If the quest is assigned as a guild task, add it to the cache.
            if (!_questsByGuildTypeAndRank.ContainsKey(quest.Guild))
                _questsByGuildTypeAndRank[quest.Guild] = new Dictionary<int, List<int>>();

            if(!_questsByGuildTypeAndRank[quest.Guild].ContainsKey(quest.RequiredGuildRank))
                _questsByGuildTypeAndRank[quest.Guild][quest.RequiredGuildRank] = new List<int>();

            _questsByGuildTypeAndRank[quest.Guild][quest.RequiredGuildRank].Add(quest.QuestID);
        }

        /// <summary>
        /// Retrieves the quests currently offered for a specific guild.
        /// </summary>
        /// <param name="guild">The guild whose quests we're searching for.</param>
        /// <returns>An enumerable of quest objects</returns>
        public static IEnumerable<IQuest> GetCurrentlyOfferedQuestsByGuild(GuildType guild)
        {
            foreach (var quest in _currentlyOfferedQuests[guild])
            {
                yield return QuestService.GetQuestByID(quest);
            }
        }

        /// <summary>
        /// Retrieves all of the quests associated with a specific guild.
        /// </summary>
        /// <param name="guild">The guilde whose quests we're searching for.</param>
        /// <returns>An enumerable of quest objects</returns>
        public static IEnumerable<IQuest> GetAllQuestsByGuild(GuildType guild)
        {
            var quests = _questsByGuildTypeAndRank[guild].SelectMany(s => s.Value);
            foreach (var quest in quests)
            {
                yield return QuestService.GetQuestByID(quest);
            }
        }

        /// <summary>
        /// Handles adding any missing PCGuildPoint records for a player to the database.
        /// </summary>
        private static void OnModuleEnter()
        {
            NWPlayer player = GetEnteringObject();
            if (!player.IsPlayer) return;

            var dbPlayer = DataService.Player.GetByID(player.GlobalID);
            // If player is missing any entries for guild points, add them now.
            var guilds = Enum.GetValues(typeof(GuildType)).Cast<GuildType>();
            foreach (var guild in guilds)
            {
                if (!dbPlayer.GuildPoints.ContainsKey(guild))
                {
                    dbPlayer.GuildPoints[guild] = new PCGuildPoint();
                }
            }

            DataService.Set(dbPlayer);
        }

        private static Dictionary<int, int> _rankProgression;

        /// <summary>
        /// This dictionary tracks the GP required for a player to increase his/her rank in a guild.
        /// All guilds use the same GP progression.
        /// </summary>
        public static Dictionary<int, int> RankProgression
        {
            get
            {
                if (_rankProgression == null)
                {
                    _rankProgression = new Dictionary<int, int>
                    {
                        // Level, Points Needed
                        { 0, 1000 },
                        { 1, 5000 },
                        { 2, 15000 },
                        { 3, 30000 },
                        { 4, 45000 },
                        { 5, 60000 }
                    };
                }

                return _rankProgression;
            }
        }

        /// <summary>
        /// Gives GP to a player for a given guild.
        /// If the baseAmount is less than 1, nothing will happen.
        /// If the baseAmount is greater than 1000, the baseAmount will be set to 1000.
        /// If the player ranks up, a message will be sent to him/her and an OnPlayerGuildRankUp event will be published.
        /// </summary>
        /// <param name="player">The player to give GP.</param>
        /// <param name="guild">The guild this GP will apply to.</param>
        /// <param name="baseAmount">The baseAmount of GP to grant.</param>
        public static void GiveGuildPoints(NWPlayer player, GuildType guild, int baseAmount)
        {
            if (baseAmount <= 0) return;

            // Clamp max GP baseAmount
            if (baseAmount > 1000)
                baseAmount = 1000;

            var dbPlayer = DataService.Player.GetByID(player.GlobalID);
            var guildDetails = guild.GetAttribute<GuildType, GuildTypeAttribute>();
            var pcGP = dbPlayer.GuildPoints[guild];
            pcGP.Points += baseAmount;

            // Clamp player GP to the highest rank.
            int maxRank = RankProgression.Keys.Max();
            int maxGP = RankProgression[maxRank];
            if (pcGP.Points >= maxGP)
                pcGP.Points = maxGP-1;

            // Notify player how much GP they earned.
            player.SendMessage("You earned " + baseAmount + " " + guildDetails.Name + " guild points.");

            // Are we able to rank up?
            bool didRankUp = false;
            if (pcGP.Rank < maxRank)
            {
                // Is it time for a rank up?
                int nextRank = RankProgression[pcGP.Rank];
                if (pcGP.Points >= nextRank)
                {
                    // Let's do a rank up.
                    pcGP.Rank++;
                    player.SendMessage(ColorTokenService.Green("You've reached rank " + pcGP.Rank + " in the " + guildDetails.Name + "!"));
                    didRankUp = true;
                }
            }

            // Submit changes to the DB/cache.
            DataService.Set(dbPlayer);

            // If the player ranked up, publish an event saying so.
            if (didRankUp)
            {
                MessageHub.Instance.Publish(new OnPlayerGuildRankUp(player.GlobalID, pcGP.Rank));
            }
        }

        /// <summary>
        /// Reward GP to player if the quest awards it.
        /// </summary>
        /// <param name="player">The player who completed the quest.</param>
        /// <param name="questID">The ID of the quest</param>
        private static void OnQuestCompleted(NWPlayer player, int questID)
        {
            var quest = QuestService.GetQuestByID(questID);
            var gpRewards = quest.GetRewards().Where(x => x.GetType() == typeof(QuestGPReward)).Cast<QuestGPReward>().ToList();

            // GP rewards not specified. Bail out early.
            if (gpRewards.Count <= 0) return;
            
            foreach(var reward in gpRewards)
            {
                int gp = CalculateGPReward(player, reward.Guild, reward.Amount);
                GiveGuildPoints(player, reward.Guild, gp);
            }
        }

        public static int CalculateGPReward(NWPlayer player, GuildType guild, int baseAmount)
        {
            var dbPlayer = DataService.Player.GetByID(player.GlobalID);
            var pcGP = dbPlayer.GuildPoints[guild];
            float rankBonus = 0.25f * pcGP.Rank;

            // Grant a bonus based on the player's guild relations perk rank.
            int perkBonus = PerkService.GetCreaturePerkLevel(player, PerkType.GuildRelations);
            baseAmount = baseAmount + (perkBonus * baseAmount);

            return baseAmount + (int)(baseAmount * rankBonus);
        }

        private static void OnModuleHeartbeat()
        {
            // Check if we need to refresh the available guild tasks every 30 minutes
            var module = NWModule.Get();
            int ticks = module.GetLocalInt("GUILD_REFRESH_TICKS") + 1;
            if (ticks >= 300)
            {
                RefreshGuildTasks(false);
                ticks = 0;
            }

            module.SetLocalInt("GUILD_REFRESH_TICKS", ticks);
        }

        /// <summary>
        /// Cycle out the available guild tasks if the previous set has been available for 24 hours.
        /// </summary>
        private static void OnModuleLoad()
        {
            RefreshGuildTasks(true);
        }

        private static void RefreshGuildTasks(bool force)
        {
            var config = DataService.ServerConfiguration.Get();
            var now = DateTime.UtcNow;

            // 24 hours haven't passed since the last cycle. Bail out now.
            if (!force && now < config.LastGuildTaskUpdate.AddHours(24)) return;

            // Start by clearing existing offered tasks.
            _currentlyOfferedQuests.Clear();

            int maxRank = RankProgression.Keys.Max();

            // Active available tasks are grouped by GuildID and RequiredRank. 
            // 10 of each are randomly selected and marked as currently offered.
            // This makes them appear in the dialog menu for players.
            // If there are 10 or less available tasks, all of them will be enabled and no randomization will occur.
            var guilds = Enum.GetValues(typeof(GuildType)).Cast<GuildType>();
            foreach (var guild in guilds)
            {
                if (guild == GuildType.Unknown) continue;

                if(!_questsByGuildTypeAndRank.ContainsKey(guild))
                    _questsByGuildTypeAndRank[guild] = new Dictionary<int, List<int>>();

                for (int rank = 0; rank < maxRank; rank++)
                {
                    if(!_questsByGuildTypeAndRank[guild].ContainsKey(rank))
                        _questsByGuildTypeAndRank[guild][rank] = new List<int>();

                    var potentialTaskIDs = _questsByGuildTypeAndRank[guild][rank];
                    var potentialTasks = new List<IQuest>();
                    foreach (var taskID in potentialTaskIDs)
                    {
                        var quest = QuestService.GetQuestByID(taskID);
                        potentialTasks.Add(quest);
                    }

                    IEnumerable<IQuest> tasks;

                    // Need at least 11 tasks to randomize. We have ten or less. Simply enable all of these.
                    if (potentialTaskIDs.Count <= 10)
                    {
                        tasks = potentialTasks;
                    }
                    // Pick 10 tasks randomly out of the potential list.
                    else
                    {
                        tasks = potentialTasks.OrderBy(o => RandomService.Random()).Take(10);
                    }

                    // We've got our set of tasks. Mark them as currently offered and submit the data change.
                    foreach (var task in tasks)
                    {
                        if(!_currentlyOfferedQuests.ContainsKey(guild))
                            _currentlyOfferedQuests[guild] = new HashSet<int>();

                        _currentlyOfferedQuests[guild].Add(task.QuestID);
                    }
                }
            }

            // Update the server config and mark the timestamp.
            config.LastGuildTaskUpdate = now;
            DataService.Set(config);


        }

    }
}
