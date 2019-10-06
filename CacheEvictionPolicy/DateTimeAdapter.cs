using System;
namespace CacheEvictionPolicy
{
    public class DateTimeAdapter
    {
        public virtual DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
