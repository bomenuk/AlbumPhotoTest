using System;

namespace Caching.Contracts
{
    public interface IEvictionPolicy
    {
        string Name { get; }
        DateTime? TimeToExpire { get; }
        bool IsExpired();
    }
}
