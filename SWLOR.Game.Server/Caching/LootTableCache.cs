using SWLOR.Game.Server.Data.Entity;

namespace SWLOR.Game.Server.Caching
{
    public class LootTableCache: CacheBase<LootTable>
    {
        protected override void OnCacheObjectSet(LootTable entity)
        {
        }

        protected override void OnCacheObjectRemoved(LootTable entity)
        {
        }

        protected override void OnSubscribeEvents()
        {
        }

        public LootTable GetByID(int id)
        {
            return (LootTable)ByID[id].Clone();
        }

        public LootTable GetByIDOrDefault(int id)
        {
            if (ByID.ContainsKey(id))
                return (LootTable)ByID[id].Clone();
            else return default(LootTable);
        }
    }
}
