using SWLOR.Game.Server;
using SWLOR.Game.Server.Event.Conversation.Quest.QuestIsDone;
using static SWLOR.Game.Server.NWScript._;

// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    internal class quest_done_3
#pragma warning restore IDE1006 // Naming Styles
    {
        public int Main()
        {
            return QuestIsDone.Check(3) ? 1 : 0;
        }
    }
}
