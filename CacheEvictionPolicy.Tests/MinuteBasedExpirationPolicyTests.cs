using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CacheEvictionPolicy.Tests
{
    [TestClass]
    public class MinuteBasedExpirationPolicyTests
    {
        [TestMethod]
        public void After_Initialized_With_1_Minute_Check_Name_And_Expire_Time()
        {
            var timeOfPolicyInitialization = DateTime.Now;
            var policy = new MinuteBasedExpirationPolicy(1);

            Assert.AreEqual(policy.Name, "TimeBasedExpirationEvictionPolicy");
            Assert.AreEqual(timeOfPolicyInitialization.AddMinutes(1).ToString(), policy.TimeToExpire.ToString());
            Assert.IsFalse(policy.IsExpired());
        }

        [TestMethod]
        public void After_Initialized_With_1_Minute_And_Wait_1_Minute_Plus_1_Second_It_Should_Expire()
        {
            var timeOfPolicyInitialization = DateTime.Now;
            var adapter = new PastDateTimeAdapter();
            var policy = new MinuteBasedExpirationPolicy(1, adapter);

            Assert.AreEqual(policy.Name, "TimeBasedExpirationEvictionPolicy");
            Assert.AreEqual(timeOfPolicyInitialization.AddMinutes(1).ToString(), policy.TimeToExpire.ToString());
            
            Assert.IsTrue(policy.IsExpired());
        }

        private class PastDateTimeAdapter: DateTimeAdapter
        {
            public override DateTime GetCurrentTime()
            {
                return DateTime.MaxValue;
            }
        }
    }
}
