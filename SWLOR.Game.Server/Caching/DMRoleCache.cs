using SWLOR.Game.Server.Data.Entity;

namespace SWLOR.Game.Server.Caching
{
    public class DMRoleCache: CacheBase<DMRole>
    {
        public DMRoleCache() 
            : base("DMRole")
        {
        }

        protected override void OnCacheObjectSet(DMRole entity)
        {
        }

        protected override void OnCacheObjectRemoved(DMRole entity)
        {
        }

        protected override void OnSubscribeEvents()
        {
        }

        public DMRole GetByID(int id)
        {
            return ByID(id);
        }
    }
}
