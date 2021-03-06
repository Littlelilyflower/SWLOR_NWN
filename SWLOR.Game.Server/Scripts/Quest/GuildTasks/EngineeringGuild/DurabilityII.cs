using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Quest;

namespace SWLOR.Game.Server.Scripts.Quest.GuildTasks.EngineeringGuild
{
    public class DurabilityII: AbstractQuest
    {
        public DurabilityII()
        {
            CreateQuest(463, "Engineering Guild Task: 1x Durability II", "eng_tsk_463")
                .IsRepeatable()

                .AddObjectiveCollectItem(1, "rune_dur2", 1, true)

                .AddRewardGold(310)
                .AddRewardGuildPoints(GuildType.EngineeringGuild, 65);
        }
    }
}
