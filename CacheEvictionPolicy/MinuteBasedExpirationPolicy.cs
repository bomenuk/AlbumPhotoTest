using System;
using Caching.Contracts;

namespace CacheEvictionPolicy
{
    public class MinuteBasedExpirationPolicy: IEvictionPolicy
    {
        private const int MIN_MINUTE_TIME = 1;
        private readonly int _minutesToExpire;
        private readonly DateTimeAdapter _dateTimeAdapter;
        
        public string Name
        {
            get { return "TimeBasedExpirationEvictionPolicy"; }
        }
        public DateTime? TimeToExpire { get; private set; }

        public MinuteBasedExpirationPolicy(int minutesToExpire, DateTimeAdapter dateTimeAdapter = null)
        {
            if(dateTimeAdapter == null)
            {
                dateTimeAdapter = new DateTimeAdapter();
            }
            _dateTimeAdapter = dateTimeAdapter;
            _minutesToExpire = minutesToExpire;
            SetTimeToExpire(_minutesToExpire);
        }

        private void SetTimeToExpire(int minutesToExpire)
        {
            if (minutesToExpire > 0)
            {
                TimeToExpire = DateTime.Now.AddMinutes(minutesToExpire);
            }
            else
            {
                TimeToExpire = DateTime.Now.AddMinutes(MIN_MINUTE_TIME);
            }            
        }

        public bool IsExpired() { return  _dateTimeAdapter.GetCurrentTime() > TimeToExpire; }

        
    }
}
