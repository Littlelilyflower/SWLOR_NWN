using SWLOR.Game.Server;
using SWLOR.Game.Server.Event.Conversation.Quest.CanAcceptQuest;
using static SWLOR.Game.Server.NWScript._;

// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    internal class can_accept_2
#pragma warning restore IDE1006 // Naming Styles
    {
        public int Main()
        {
            return QuestCanAccept.Check(2) ? 1 : 0;
        }
    }
}
