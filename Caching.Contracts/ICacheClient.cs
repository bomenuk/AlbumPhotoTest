using System;
using System.Collections.Generic;

namespace Caching.Contracts
{
    public interface ICacheClient<T>
    {
        IList<T> GetTopicData(string topicName, Func<IList<T>> cacheMissLoadFunc, IEvictionPolicy evictionPolicy);
        int NumberOfTopics { get; }
    }
}
