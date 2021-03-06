using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Quest;

namespace SWLOR.Game.Server.Scripts.Quest.GuildTasks.EngineeringGuild
{
    public class FirstAidIII: AbstractQuest
    {
        public FirstAidIII()
        {
            CreateQuest(524, "Engineering Guild Task: 1x First Aid III", "eng_tsk_524")
                .IsRepeatable()

                .AddObjectiveCollectItem(1, "rune_faid3", 1, true)

                .AddRewardGold(430)
                .AddRewardGuildPoints(GuildType.EngineeringGuild, 90);
        }
    }
}
