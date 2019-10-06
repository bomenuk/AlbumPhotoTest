using AlbumPhotoFetchService.Contracts;
using Repository.Contracts;
using Repository.Contracts.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Caching.Contracts;
using AlbumPhotoFetchService.Contracts.Entities;
using Caching.MemoryBased;

namespace AlbumPhotoFetchService.Tests
{
    [TestClass]
    public class AlbumPhotoServiceTests
    {
        private class DummyAlbumRepository : IAlbumRepository
        {
            public DummyAlbumRepository() { }

            public DummyAlbumRepository(IList<Repository.Contracts.DTOs.Album> albums)
            {
                _albums = albums;
            }

            private IList<Repository.Contracts.DTOs.Album> _albums = new List<Repository.Contracts.DTOs.Album>();

            public IList<Repository.Contracts.DTOs.Album> GetAlbumsByUserId(int userId)
            {
                return _albums.Where(a => a.UserId == userId).ToList();
            }

            public IList<Repository.Contracts.DTOs.Album> GetAll()
            {
                return _albums;
            }

            public Repository.Contracts.DTOs.Album GetById(int id)
            {
                return _albums.FirstOrDefault(a => a.Id == id);
            }
        }

        private class DummyPhotoRepository : IPhotoRepository
        {
            public DummyPhotoRepository() { }

            public DummyPhotoRepository(IList<Repository.Contracts.DTOs.Photo> photos)
            {
                _photos = photos;
            }

            private IList<Repository.Contracts.DTOs.Photo> _photos = new List<Repository.Contracts.DTOs.Photo>();

            public IList<Repository.Contracts.DTOs.Photo> GetAll()
            {
                return _photos;
            }

            public Repository.Contracts.DTOs.Photo GetById(int id)
            {
                return _photos.FirstOrDefault(p => p.Id == id);
            }

            public IList<Repository.Contracts.DTOs.Photo> GetPhotosByAlbumId(int albumId)
            {
                return _photos.Where(p => p.AlbumId == albumId).ToList();
            }
        }

        private ICacheClient<baseServiceEntity> _cacheClient = new MemoryBasedCacheClient<baseServiceEntity>();
        private IAlbumRepository _albumRepository;
        private IPhotoRepository _photoRepository;

        private void Initialize(bool initializeWithData)
        {
            if (initializeWithData)
            {
                var albums = new List<Repository.Contracts.DTOs.Album>{
                new Repository.Contracts.DTOs.Album{Id = 1, UserId = 1, Title ="Album1" },
                new Repository.Contracts.DTOs.Album{Id = 2, UserId = 2, Title ="Album2" },
                new Repository.Contracts.DTOs.Album{Id = 3, UserId = 2, Title ="Album3" }
                };

                var photos = new List<Repository.Contracts.DTOs.Photo> {
                new Repository.Contracts.DTOs.Photo { Id = 1, Title = "Photo1", AlbumId = 1, Url = "a", ThumbnailUrl="a" },
                new Repository.Contracts.DTOs.Photo { Id = 2, Title = "Photo2", AlbumId = 2, Url = "b", ThumbnailUrl="b" },
                new Repository.Contracts.DTOs.Photo { Id = 3, Title = "Photo3", AlbumId = 2, Url = "b", ThumbnailUrl="c" },
                new Repository.Contracts.DTOs.Photo { Id = 4, Title = "Photo4", AlbumId = 3, Url = "c", ThumbnailUrl="c" }
                };

                _albumRepository = new DummyAlbumRepository(albums);
                _photoRepository = new DummyPhotoRepository(photos);
            }
            else
            {
                _albumRepository = new DummyAlbumRepository();
                _photoRepository = new DummyPhotoRepository();
            }
        }

        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void GetAlbum_Should_Return_Correct_Result(bool initializeWithData)
        {
            Initialize(initializeWithData);
            var service = new AlbumPhotoService(_cacheClient, _albumRepository, _photoRepository);
            var result = service.GetAlbum(1);
            if (initializeWithData)
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Id, 1);
                Assert.AreEqual(result.UserId, 1);
                Assert.AreEqual(result.Title, "Album1");

                Assert.AreEqual(result.Photos.Count, 1);
                Assert.AreEqual(result.Photos[0].Id, 1);
                Assert.AreEqual(result.Photos[0].AlbumId, 1);
                Assert.AreEqual(result.Photos[0].Title, "Photo1");
                Assert.AreEqual(result.Photos[0].Url, "a");
            }
            else
            {
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void GetAllAlbums_Should_Return_Correct_Result(bool initializeWithData)
        {
            Initialize(initializeWithData);
            var service = new AlbumPhotoService(_cacheClient, _albumRepository, _photoRepository);
            var result = service.GetAllAlbums();
            if (initializeWithData)
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Count, 3);

                //Check Album1
                Assert.AreEqual(result[0].Id, 1);
                Assert.AreEqual(result[0].UserId, 1);
                Assert.AreEqual(result[0].Title, "Album1");

                Assert.AreEqual(result[0].Photos.Count, 1);
                Assert.AreEqual(result[0].Photos[0].Id, 1);
                Assert.AreEqual(result[0].Photos[0].AlbumId, 1);
                Assert.AreEqual(result[0].Photos[0].Title, "Photo1");
                Assert.AreEqual(result[0].Photos[0].Url, "a");

                //Check Album2
                Assert.AreEqual(result[1].Id, 2);
                Assert.AreEqual(result[1].UserId, 2);
                Assert.AreEqual(result[1].Title, "Album2");

                Assert.AreEqual(result[1].Photos.Count, 2);
                Assert.AreEqual(result[1].Photos[0].Id, 2);
                Assert.AreEqual(result[1].Photos[0].AlbumId, 2);
                Assert.AreEqual(result[1].Photos[0].Title, "Photo2");
                Assert.AreEqual(result[1].Photos[0].Url, "b");
                Assert.AreEqual(result[1].Photos[1].Id, 3);
                Assert.AreEqual(result[1].Photos[1].AlbumId, 2);
                Assert.AreEqual(result[1].Photos[1].Title, "Photo3");
                Assert.AreEqual(result[1].Photos[1].Url, "b");

                //Check Album3
                Assert.AreEqual(result[2].Id, 3);
                Assert.AreEqual(result[2].UserId, 2);
                Assert.AreEqual(result[2].Title, "Album3");

                Assert.AreEqual(result[2].Photos.Count, 1);
                Assert.AreEqual(result[2].Photos[0].Id, 4);
                Assert.AreEqual(result[2].Photos[0].AlbumId, 3);
                Assert.AreEqual(result[2].Photos[0].Title, "Photo4");
                Assert.AreEqual(result[2].Photos[0].Url, "c");
            }
            else
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Count, 0);
            }
        }

        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void GetAlbumsByUserId_Should_Return_Correct_Result(bool initializeWithData)
        {
            Initialize(initializeWithData);
            var service = new AlbumPhotoService(_cacheClient, _albumRepository, _photoRepository);
            var result = service.GetAlbumsByUserId(1);
            if (initializeWithData)
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Count, 1);

                //Check Album1
                Assert.AreEqual(result[0].Id, 1);
                Assert.AreEqual(result[0].UserId, 1);
                Assert.AreEqual(result[0].Title, "Album1");

                Assert.AreEqual(result[0].Photos.Count, 1);
                Assert.AreEqual(result[0].Photos[0].Id, 1);
                Assert.AreEqual(result[0].Photos[0].AlbumId, 1);
                Assert.AreEqual(result[0].Photos[0].Title, "Photo1");
                Assert.AreEqual(result[0].Photos[0].Url, "a");
            }
            else
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Count, 0);
            }
        }

        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void GetPhoto_Should_Return_Correct_Result(bool initializeWithData)
        {
            Initialize(initializeWithData);
            var service = new AlbumPhotoService(_cacheClient, _albumRepository, _photoRepository);
            var result = service.GetPhoto(1);
            if (initializeWithData)
            {
                Assert.IsNotNull(result);                
                Assert.AreEqual(result.Id, 1);
                Assert.AreEqual(result.AlbumId, 1);
                Assert.AreEqual(result.Title, "Photo1");
                Assert.AreEqual(result.Url, "a");
            }
            else
            {
                Assert.IsNull(result);
            }
        }

        [TestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void GetAllPhotos_Should_Return_Correct_Result(bool initializeWithData)
        {
            Initialize(initializeWithData);
            var service = new AlbumPhotoService(_cacheClient, _albumRepository, _photoRepository);
            var result = service.GetAllPhotos();
            if (initializeWithData)
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Count, 4);                
                
                //Check Photo1
                Assert.AreEqual(result[0].Id, 1);
                Assert.AreEqual(result[0].AlbumId, 1);
                Assert.AreEqual(result[0].Title, "Photo1");
                Assert.AreEqual(result[0].Url, "a");

                //Check Photo2
                Assert.AreEqual(result[1].Id, 2);
                Assert.AreEqual(result[1].AlbumId, 2);
                Assert.AreEqual(result[1].Title, "Photo2");
                Assert.AreEqual(result[1].Url, "b");

                //Check Photo3
                Assert.AreEqual(result[2].Id, 3);
                Assert.AreEqual(result[2].AlbumId, 2);
                Assert.AreEqual(result[2].Title, "Photo3");
                Assert.AreEqual(result[2].Url, "b");

                //Check Photo4
                Assert.AreEqual(result[3].Id, 4);
                Assert.AreEqual(result[3].AlbumId, 3);
                Assert.AreEqual(result[3].Title, "Photo4");
                Assert.AreEqual(result[3].Url, "c");
            }
            else
            {
                Assert.IsNotNull(result);
                Assert.AreEqual(result.Count, 0);
            }
        }
    }
}
