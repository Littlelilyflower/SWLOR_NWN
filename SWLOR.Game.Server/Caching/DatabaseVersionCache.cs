using System;
using SWLOR.Game.Server.Data.Entity;

namespace SWLOR.Game.Server.Caching
{
    public class DatabaseVersionCache: CacheBase<DatabaseVersion>
    {
        public DatabaseVersionCache() 
            : base("DatabaseVersion")
        {
        }

        protected override void OnCacheObjectSet(DatabaseVersion entity)
        {
        }

        protected override void OnCacheObjectRemoved(DatabaseVersion entity)
        {
        }

        protected override void OnSubscribeEvents()
        {
        }

        public DatabaseVersion GetByID(Guid id)
        {
            return ByID(id);
        }
    }
}
