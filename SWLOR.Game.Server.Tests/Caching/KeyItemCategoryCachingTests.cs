using System.Collections.Generic;
using NUnit.Framework;
using SWLOR.Game.Server.Caching;
using SWLOR.Game.Server.Data.Entity;
using SWLOR.Game.Server.Event.SWLOR;
using SWLOR.Game.Server.Messaging;

namespace SWLOR.Game.Server.Tests.Caching
{
    public class KeyItemCategoryCacheTests
    {
        private KeyItemCategoryCache _cache;

        [SetUp]
        public void Setup()
        {
            _cache = new KeyItemCategoryCache();
        }

        [TearDown]
        public void TearDown()
        {
            _cache = null;
        }


        [Test]
        public void GetByID_OneItem_ReturnsKeyItemCategory()
        {
            // Arrange
            KeyItemCategory entity = new KeyItemCategory {ID = 1};
            
            // Act
            MessageHub.Instance.Publish(new OnCacheObjectSet<KeyItemCategory>(entity));

            // Assert
            Assert.AreNotSame(entity, _cache.GetByID(1));
        }

        [Test]
        public void GetByID_TwoItems_ReturnsCorrectObject()
        {
            // Arrange
            KeyItemCategory entity1 = new KeyItemCategory { ID = 1};
            KeyItemCategory entity2 = new KeyItemCategory { ID = 2};

            // Act
            MessageHub.Instance.Publish(new OnCacheObjectSet<KeyItemCategory>(entity1));
            MessageHub.Instance.Publish(new OnCacheObjectSet<KeyItemCategory>(entity2));

            // Assert
            Assert.AreNotSame(entity1, _cache.GetByID(1));
            Assert.AreNotSame(entity2, _cache.GetByID(2));
        }

        [Test]
        public void GetByID_RemovedItem_ReturnsCorrectObject()
        {
            // Arrange
            KeyItemCategory entity1 = new KeyItemCategory { ID = 1};
            KeyItemCategory entity2 = new KeyItemCategory { ID = 2};

            // Act
            MessageHub.Instance.Publish(new OnCacheObjectSet<KeyItemCategory>(entity1));
            MessageHub.Instance.Publish(new OnCacheObjectSet<KeyItemCategory>(entity2));
            MessageHub.Instance.Publish(new OnCacheObjectDeleted<KeyItemCategory>(entity1));

            // Assert
            Assert.Throws<KeyNotFoundException>(() => { _cache.GetByID(1); });
            Assert.AreNotSame(entity2, _cache.GetByID(2));
        }

        [Test]
        public void GetByID_NoItems_ThrowsKeyNotFoundException()
        {
            // Arrange
            KeyItemCategory entity1 = new KeyItemCategory { ID = 1};
            KeyItemCategory entity2 = new KeyItemCategory { ID = 2};

            // Act
            MessageHub.Instance.Publish(new OnCacheObjectSet<KeyItemCategory>(entity1));
            MessageHub.Instance.Publish(new OnCacheObjectSet<KeyItemCategory>(entity2));
            MessageHub.Instance.Publish(new OnCacheObjectDeleted<KeyItemCategory>(entity1));
            MessageHub.Instance.Publish(new OnCacheObjectDeleted<KeyItemCategory>(entity2));

            // Assert
            Assert.Throws<KeyNotFoundException>(() => { _cache.GetByID(1); });
            Assert.Throws<KeyNotFoundException>(() => { _cache.GetByID(2); });

        }
    }
}
