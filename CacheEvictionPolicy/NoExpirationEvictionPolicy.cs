using System;
using Caching.Contracts;

namespace CacheEvictionPolicy
{
    public class NoExpirationEvictionPolicy: IEvictionPolicy
    {
        public string Name
        {
            get { return "NoExpirationEvictionPolicy"; }
        }
        public DateTime? TimeToExpire { get { return null; } }
        public bool IsExpired() { return false; }
    }
}
