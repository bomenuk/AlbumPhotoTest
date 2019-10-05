using System;
using System.Collections.Generic;
using System.Linq;
using AlbumPhotoService.Contracts;
using AlbumPhotoService.Contracts.Entities;
using CacheEvictionPolicy;
using Caching.Contracts;
using Repository;

namespace AlbumPhotoFetchService
{
    public class AlbumPhotoService: IAlbumPhotoService
    {
        private readonly ICacheClient _cacheClient;
        private readonly IAlbumRepository _albumRepository;
        private readonly IPhotoRepository _photoRepository;

        public AlbumPhotoService(ICacheClient cacheClient, IAlbumRepository albumRepository,
            IPhotoRepository photoRepository)
        {
            _cacheClient = cacheClient;
            _albumRepository = albumRepository;
            _photoRepository = photoRepository;
        }

        public Album GetAlbum(int albumId)
        {
            var albums = _cacheClient.GetTopicData(GenerateCacheTopicNameFromEntity(typeof(Album)), albumMissLoadFunc, new MinuteBasedExpirationPolicy(20));
            return albums.FirstOrDefault(a => a.Id == albumId);
        }

        public IList<Album> GetAllAlbums()
        {
            var albums = _cacheClient.GetTopicData(GenerateCacheTopicNameFromEntity(typeof(Album)), albumMissLoadFunc, new MinuteBasedExpirationPolicy(20));
            return albums;
        }

        public IList<Album> GetAlbumsByUserId(int userId)
        {
            var albums = _cacheClient.GetTopicData(GenerateCacheTopicNameFromEntity(typeof(Album)), albumMissLoadFunc, new MinuteBasedExpirationPolicy(20));
            return albums.Where(a => a.UserId == userId).ToList();
        }

        public Photo GetPhoto(int photoId)
        {
            var photos = _cacheClient.GetTopicData(GenerateCacheTopicNameFromEntity(typeof(Photo)), photoMissLoadFunc, new MinuteBasedExpirationPolicy(3));
            return photos.FirstOrDefault(p => p.Id == photoId);
        }

        public IList<Photo> GetAllPoPhotos()
        {
            var photos = _cacheClient.GetTopicData(GenerateCacheTopicNameFromEntity(typeof(Photo)), photoMissLoadFunc, new MinuteBasedExpirationPolicy(3));
            return photos;
        }

        private IList<Album> albumMissLoadFunc(string topicName)
        {
            var albums = new List<Album>();
            var albumData = _albumRepository.GetAll();
            var photoData = _photoRepository.GetAll();

            foreach (var album in albumData)
            {
                albums.Add(new Album
                {
                    Id = album.Id,
                    Title = album.Title,
                    UserId = album.UserId,
                });
            }

            albums.ForEach(a => a.Photos = photoData.Where(p => p.AlbumnId == a.Id)
                .Select(p => new Photo {AlbumId = p.AlbumnId, Id = p.Id, Title = p.Title, Url = p.Url}).ToList());
            return albums;
        }

        private IList<Photo> photoMissLoadFunc(string topicName)
        {
            var photos = _photoRepository.GetAll().Select(p=>new Photo{AlbumId = p.AlbumnId, Id = p.Id, Title = p.Title, Url = p.Url}).ToList();
            return photos;
        }

        public IList<Album> GetAlbumsForUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        private string GenerateCacheTopicNameFromEntity(Type entityType)
        {
            return entityType.Name;
        }
    }
}
