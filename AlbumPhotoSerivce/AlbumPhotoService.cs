using System;
using System.Collections.Generic;
using System.Linq;
using AlbumPhotoFetchService.Contracts;
using AlbumPhotoFetchService.Contracts.Entities;
using CacheEvictionPolicy;
using Caching.Contracts;
using Repository.Contracts;

namespace AlbumPhotoFetchService
{
    public class AlbumPhotoService: IAlbumPhotoService
    {
        private readonly ICacheClient<baseServiceEntity> _cacheClient;
        private readonly IAlbumRepository _albumRepository;
        private readonly IPhotoRepository _photoRepository;

        public AlbumPhotoService(ICacheClient<baseServiceEntity> cacheClient, IAlbumRepository albumRepository,
            IPhotoRepository photoRepository)
        {
            _cacheClient = cacheClient;
            _albumRepository = albumRepository;
            _photoRepository = photoRepository;
        }

        public Album GetAlbum(int albumId)
        {
            var albums = _cacheClient.GetTopicData(GenerateCacheTopicNameFromEntity(typeof(Album)), albumMissLoadFunc, new MinuteBasedExpirationPolicy(20)).Cast<Album>();
            return albums.FirstOrDefault(a => a.Id == albumId);
        }

        public IList<Album> GetAllAlbums()
        {
            var albums = _cacheClient.GetTopicData(GenerateCacheTopicNameFromEntity(typeof(Album)), albumMissLoadFunc, new MinuteBasedExpirationPolicy(20)).Cast<Album>().ToList();
            return albums;
        }

        public IList<Album> GetAlbumsByUserId(int userId)
        {
            var albums = _cacheClient.GetTopicData(GenerateCacheTopicNameFromEntity(typeof(Album)), albumMissLoadFunc, new MinuteBasedExpirationPolicy(20)).Cast<Album>().ToList();
            return albums.Where(a => a.UserId == userId).ToList();
        }

        public Photo GetPhoto(int photoId)
        {
            var photos = _cacheClient.GetTopicData(GenerateCacheTopicNameFromEntity(typeof(Photo)), photoMissLoadFunc, new MinuteBasedExpirationPolicy(5)).Cast<Photo>();
            return photos.FirstOrDefault(p => p.Id == photoId);
        }

        public IList<Photo> GetAllPhotos()
        {
            var photos = _cacheClient.GetTopicData(GenerateCacheTopicNameFromEntity(typeof(Photo)), photoMissLoadFunc, new MinuteBasedExpirationPolicy(5)).Cast<Photo>().ToList();
            return photos;
        }

        private IList<baseServiceEntity> albumMissLoadFunc()
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
                    Photos = FindPhotosFromAlbumId(album.Id, photoData)
                });
            }
            return albums.Cast<baseServiceEntity>().ToList();
        }

        private IList<Photo> FindPhotosFromAlbumId(int albumId, IList<Repository.Contracts.DTOs.Photo> photosData)
        {
            return photosData.Where(p => p.AlbumId == albumId)
                .Select(p => new Photo { AlbumId = p.AlbumId, Id = p.Id, Title = p.Title, Url = p.Url }).ToList();
        }

        private IList<baseServiceEntity> photoMissLoadFunc()
        {
            var photos = _photoRepository.GetAll().Select(p=>new Photo{AlbumId = p.AlbumId, Id = p.Id, Title = p.Title, Url = p.Url}).Cast<baseServiceEntity>().ToList();
            return photos;
        }        

        private string GenerateCacheTopicNameFromEntity(Type entityType)
        {
            return entityType.Name;
        }
    }
}
