using SWLOR.Game.Server;
using SWLOR.Game.Server.Event.Conversation.Quest.AdvanceQuest;
using static NWN._;

// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    internal class next_state_4
#pragma warning restore IDE1006 // Naming Styles
    {
        public int Main()
        {
            return QuestAdvance.Check(4) ? 1 : 0;
        }
    }
}
