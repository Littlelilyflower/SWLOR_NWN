using SWLOR.Game.Server.Data.Entity;

namespace SWLOR.Game.Server.Caching
{
    public class AttributeCache: CacheBase<Attribute>
    {
        public AttributeCache() 
            : base("Attribute")
        {
        }

        protected override void OnCacheObjectSet(Attribute entity)
        {
        }

        protected override void OnCacheObjectRemoved(Attribute entity)
        {
        }

        protected override void OnSubscribeEvents()
        {
        }

        public Attribute GetByID(int id)
        {
            return ByID(id);
        }
    }
}
