using SWLOR.Game.Server;
using SWLOR.Game.Server.Event.Conversation.Quest.CanAcceptQuest;
using static NWN._;

// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    internal class can_accept_6
#pragma warning restore IDE1006 // Naming Styles
    {
        public static int Main()
        {
            return QuestCanAccept.Check(6) ? true : false;
        }
    }
}
