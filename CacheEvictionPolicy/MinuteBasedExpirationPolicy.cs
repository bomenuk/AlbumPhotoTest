using System;
using Caching.Contracts;

namespace CacheEvictionPolicy
{
    public class MinuteBasedExpirationPolicy: IEvictionPolicy
    {
        private const int MIN_MINUTE_TIME = 1;
        private readonly int _minutesToExpire;
        public string Name
        {
            get { return "TimeBasedExpirationEvictionPolicy"; }
        }
        public DateTime? TimeToExpire { get; private set; }
        public IEvictionPolicy RefreshExpiration()
        {
            SetTimeToExpire(_minutesToExpire);
            return this;
        }

        public MinuteBasedExpirationPolicy(int minutesToExpire)
        {
            _minutesToExpire = minutesToExpire;
            SetTimeToExpire(_minutesToExpire);
        }

        private void SetTimeToExpire(int minutesToExpire)
        {
            if (minutesToExpire > 0)
            {
                TimeToExpire = DateTime.Now.AddMinutes(minutesToExpire);
            }

            TimeToExpire = DateTime.Now.AddMinutes(MIN_MINUTE_TIME);
        }
    }
}
