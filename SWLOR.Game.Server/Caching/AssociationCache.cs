using SWLOR.Game.Server.Data.Entity;

namespace SWLOR.Game.Server.Caching
{
    public class AssociationCache: CacheBase<Association>
    {
        public AssociationCache() 
            : base("Association")
        {
        }

        protected override void OnCacheObjectSet(Association entity)
        {
        }

        protected override void OnCacheObjectRemoved(Association entity)
        {
        }

        protected override void OnSubscribeEvents()
        {
        }

        public Association GetByID(int id)
        {
            return ByID(id);
        }
    }
}
