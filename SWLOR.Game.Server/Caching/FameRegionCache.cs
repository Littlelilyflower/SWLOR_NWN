using SWLOR.Game.Server.Data.Entity;

namespace SWLOR.Game.Server.Caching
{
    public class FameRegionCache: CacheBase<FameRegion>
    {
        public FameRegionCache() 
            : base("FameRegion")
        {
        }

        protected override void OnCacheObjectSet(FameRegion entity)
        {
        }

        protected override void OnCacheObjectRemoved(FameRegion entity)
        {
        }

        protected override void OnSubscribeEvents()
        {
        }

        public FameRegion GetByID(int id)
        {
            return ByID(id);
        }
    }
}
