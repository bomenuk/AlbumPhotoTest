using System;
using System.Collections.Generic;
using AlbumPhotoService.Contracts.Entities;

namespace Caching.Contracts
{
    public interface ICacheClient
    {
        IList<T> GetTopicData<T>(string topicName, Func<string, IList<T>> cacheMissLoadFunc, IEvictionPolicy evictionPolicy) where T: baseServiceEntity;
        void InvalidTopicCache(string topicName);
    }
}
