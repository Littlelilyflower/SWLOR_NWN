using SWLOR.Game.Server.Data.Entity;

namespace SWLOR.Game.Server.Caching
{
    public class GameTopicCategoryCache: CacheBase<GameTopicCategory>
    {
        public GameTopicCategoryCache() 
            : base("GameTopicCategory")
        {
        }

        protected override void OnCacheObjectSet(GameTopicCategory entity)
        {
        }

        protected override void OnCacheObjectRemoved(GameTopicCategory entity)
        {
        }

        protected override void OnSubscribeEvents()
        {
        }

        public GameTopicCategory GetByID(int id)
        {
            return ByID(id);
        }
    }
}
