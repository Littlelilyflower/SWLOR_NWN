using SWLOR.Game.Server.Enumeration;
using SWLOR.Game.Server.Quest;

namespace SWLOR.Game.Server.Scripts.Quest.GuildTasks.WeaponsmithGuild
{
    public class BatonMS2: AbstractQuest
    {
        public BatonMS2()
        {
            CreateQuest(276, "Weaponsmith Guild Task: 1x Baton MS2", "wpn_tsk_276")
                .IsRepeatable()

                .AddObjectiveCollectItem(1, "morningstar_2", 1, true)

                .AddRewardGold(185)
                .AddRewardGuildPoints(GuildType.WeaponsmithGuild, 39);
        }
    }
}
