﻿using SWLOR.Game.Server;
using SWLOR.Game.Server.Event.Conversation.Skill;
using static SWLOR.Game.Server.NWScript._;

// ReSharper disable once CheckNamespace
namespace NWN.Scripts
{
#pragma warning disable IDE1006 // Naming Styles
    internal class has_skill_or_6
#pragma warning restore IDE1006 // Naming Styles
    {
        public int Main()
        {
            return HasSkillRank.Check(6, "OR") ? 1 : 0;
        }
    }
}
