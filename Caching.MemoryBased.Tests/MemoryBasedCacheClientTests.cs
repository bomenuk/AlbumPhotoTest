using CacheEvictionPolicy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Caching.MemoryBased.Tests
{
    [TestClass]
    public class MemoryBasedCacheClientTests
    {
        private MemoryBasedCacheClient<int> cacheClient;

        [TestInitialize]
        public void Initialize()
        {
            cacheClient = new MemoryBasedCacheClient<int>();
        }

        [TestMethod]
        public void If_Nothing_In_Cache_NumberOfTopics_Should_Be_0()
        {
            Assert.AreEqual(0, cacheClient.NumberOfTopics);
            cacheClient.GetTopicData("IntegerTest", GetSampleIntegers, new NoExpirationEvictionPolicy());
            Assert.AreEqual(1, cacheClient.NumberOfTopics);
        }

        [TestMethod]
        public void When_1st_Called_Should_Create_Topic()
        {
            cacheClient.GetTopicData("IntegerTest", GetSampleIntegers, new NoExpirationEvictionPolicy());
            Assert.AreEqual(1, cacheClient.NumberOfTopics);
        }

        [TestMethod]
        public void When_2nd_Called_Should_Not_Create_Topic()
        {
            cacheClient.GetTopicData("IntegerTest", GetSampleIntegers, new NoExpirationEvictionPolicy());
            Assert.AreEqual(1, cacheClient.NumberOfTopics);

            cacheClient.GetTopicData("IntegerTest", GetSampleIntegers, new NoExpirationEvictionPolicy());
            Assert.AreEqual(1, cacheClient.NumberOfTopics);
        }

        private IList<int> GetSampleIntegers()
        {
            return new int[] { 1, 2, 3 };
        }
    }
}
