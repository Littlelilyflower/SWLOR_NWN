using System.Collections.Generic;
using SWLOR.Game.Server.Data.Entity;

namespace SWLOR.Game.Server.Caching
{
    public class GuildCache: CacheBase<Guild>
    {
        public GuildCache() 
            : base("Guild")
        {
        }

        protected override void OnCacheObjectSet(Guild entity)
        {
        }

        protected override void OnCacheObjectRemoved(Guild entity)
        {
        }

        protected override void OnSubscribeEvents()
        {
        }

        public Guild GetByID(int id)
        {
            return ByID(id);
        }
    }
}
