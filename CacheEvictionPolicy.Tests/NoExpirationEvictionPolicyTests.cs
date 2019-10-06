using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CacheEvictionPolicy.Tests
{
    [TestClass]
    public class NoExpirationEvictionPolicyTests
    {
        [TestMethod]
        public void After_Initialized_Name_Is_NoExpirationEvictionPolicy_And_Never_Expire()
        {
            var policy = new NoExpirationEvictionPolicy();
            Assert.AreEqual(policy.Name, "NoExpirationEvictionPolicy");
            Assert.AreEqual(policy.TimeToExpire, null);
            Assert.IsFalse(policy.IsExpired());
        }
    }
}
