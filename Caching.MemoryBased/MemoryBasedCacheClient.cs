using System;
using System.Collections.Generic;
using System.Linq;
using AlbumPhotoService.Contracts.Entities;
using Caching.Contracts;

namespace Caching.MemoryBased
{
    public class MemoryBasedCacheClient : ICacheClient
    {
        private Dictionary<string, (IList<baseServiceEntity> data, IEvictionPolicy evictionPolicy)> _cachedData =
            new Dictionary<string, (IList<baseServiceEntity> data, IEvictionPolicy evictionPolicy)>();

        public IList<T> GetTopicData<T>(string topicName, Func<string, IList<T>> cacheMissLoadFunc, IEvictionPolicy evictionPolicy) where T: baseServiceEntity
        {
            if (_cachedData.ContainsKey(topicName))
            {
                if (DateTime.Now > _cachedData[topicName].evictionPolicy.TimeToExpire)
                {
                    _cachedData.Remove(topicName);
                    InitializeTopicData(topicName, cacheMissLoadFunc(topicName), evictionPolicy);
                }
                return _cachedData[topicName].data.Cast<T>().ToList();
            }
            if (InitializeTopicData(topicName, cacheMissLoadFunc(topicName), evictionPolicy))
            {
                return _cachedData[topicName].data.Cast<T>().ToList();
            }

            return null;
        }

        private bool InitializeTopicData<T>(string topicName, IList<T> data, IEvictionPolicy evictionPolicy) where T: baseServiceEntity
        {
            bool success = false;
            try
            {
                if (!_cachedData.ContainsKey(topicName))
                {
                    _cachedData.Add(topicName, (data.Cast<baseServiceEntity>().ToList(), evictionPolicy));
                }
                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return success;
        }

        public void InvalidTopicCache(string topicName)
        {
            if (_cachedData.ContainsKey(topicName))
            {
                _cachedData.Remove(topicName);
            }
        }
    }
}
