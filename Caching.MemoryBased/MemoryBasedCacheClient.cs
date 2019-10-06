using System;
using System.Collections.Generic;
using System.Linq;
using Caching.Contracts;

namespace Caching.MemoryBased
{
    public class MemoryBasedCacheClient<T> : ICacheClient<T>
    {
        private Dictionary<string, (IList<T> data, IEvictionPolicy evictionPolicy)> _cachedData =
            new Dictionary<string, (IList<T> data, IEvictionPolicy evictionPolicy)>();

        public int NumberOfTopics { get { return _cachedData.Count(); } }

        public IList<T> GetTopicData(string topicName, Func<IList<T>> cacheMissLoadFunc, IEvictionPolicy evictionPolicy)
        {
            if (_cachedData.ContainsKey(topicName))
            {
                if (_cachedData[topicName].evictionPolicy.IsExpired())
                {
                    _cachedData.Remove(topicName);
                    InitializeTopicData(topicName, cacheMissLoadFunc(), evictionPolicy);
                }
                return _cachedData[topicName].data.Cast<T>().ToList();
            }
            if (InitializeTopicData(topicName, cacheMissLoadFunc(), evictionPolicy))
            {
                return _cachedData[topicName].data.Cast<T>().ToList();
            }

            return null;
        }

        private bool InitializeTopicData(string topicName, IList<T> data, IEvictionPolicy evictionPolicy)
        {
            bool success = false;
            try
            {
                if (!_cachedData.ContainsKey(topicName))
                {
                    _cachedData.Add(topicName, (data, evictionPolicy));
                }
                success = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return success;
        }
    }
}
