using SWLOR.Game.Server;
using SWLOR.Game.Server.Event.Conversation.Quest.FinishQuest;
using static SWLOR.Game.Server.NWScript._;

// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    internal class fin_qst2_rule8
#pragma warning restore IDE1006 // Naming Styles
    {
        public int Main()
        {
            return QuestComplete.Check(2, 8) ? 1 : 0;
        }
    }
}
