using SWLOR.Game.Server.Data.Entity;

namespace SWLOR.Game.Server.Caching
{
    public class KeyItemCategoryCache: CacheBase<KeyItemCategory>
    {
        public KeyItemCategoryCache() 
            : base("KeyItemCategory")
        {
        }

        protected override void OnCacheObjectSet(KeyItemCategory entity)
        {
        }

        protected override void OnCacheObjectRemoved(KeyItemCategory entity)
        {
        }

        protected override void OnSubscribeEvents()
        {
        }

        public KeyItemCategory GetByID(int id)
        {
            return ByID(id);
        }
    }
}
