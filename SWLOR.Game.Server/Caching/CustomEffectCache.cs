using SWLOR.Game.Server.Data.Entity;

namespace SWLOR.Game.Server.Caching
{
    public class CustomEffectCache: CacheBase<Data.Entity.CustomEffect>
    {
        public CustomEffectCache() 
            : base("CustomEffect")
        {
        }

        protected override void OnCacheObjectSet(Data.Entity.CustomEffect entity)
        {
        }

        protected override void OnCacheObjectRemoved(Data.Entity.CustomEffect entity)
        {
        }

        protected override void OnSubscribeEvents()
        {
        }

        public Data.Entity.CustomEffect GetByID(int id)
        {
            return ByID(id);
        }
    }
}
