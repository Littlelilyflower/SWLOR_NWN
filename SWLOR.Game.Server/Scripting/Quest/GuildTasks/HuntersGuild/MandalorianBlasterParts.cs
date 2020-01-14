using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Quest;

namespace SWLOR.Game.Server.Scripting.Quest.GuildTasks.HuntersGuild
{
    public class MandalorianBlasterParts: AbstractQuest
    {
        public MandalorianBlasterParts()
        {
            CreateQuest(594, "Hunter's Guild Task: 6x Mandalorian Blaster Parts", "hun_tsk_594")
                .IsRepeatable()
				.IsGuildTask(GuildType.HuntersGuild, 1)


                .AddObjectiveCollectItem(1, "m_blast_parts", 6, false)

                .AddRewardGold(83)
                .AddRewardGuildPoints(GuildType.HuntersGuild, 25);
        }
    }
}
