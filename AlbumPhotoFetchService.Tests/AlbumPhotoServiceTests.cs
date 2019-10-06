using AlbumPhotoFetchService.Contracts;
using AlbumPhotoFetchService.Contracts.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AlbumPhotoFetchService.Tests
{
    [TestClass]
    public class AlbumPhotoServiceTests
    {
        private class DummyAlbumPhotoService : IAlbumPhotoService
        {
            public IList<Album> Albums;

            public Album GetAlbum(int albumId)
            {
                return Albums.FirstOrDefault(a => a.Id == albumId);
            }

            public IList<Album> GetAlbumsByUserId(int userId)
            {
                return Albums.Where(a => a.UserId == userId).ToList();
            }

            public IList<Album> GetAllAlbums()
            {
                return Albums;
            }
        }

        private DummyAlbumPhotoService dummyService;
        private IList<Album> albums;

        [TestInitialize]
        public void Initialize()
        {
            dummyService = new DummyAlbumPhotoService();
            albums = new List<Album>(){
                new Album { Id = 1, UserId = 1, Title = "Album1",
                Photos = new List<Photo>()
                { new Photo { Id = 1, Title = "Photo1", AlbumId = 1, Url = "a" } } },
                new Album { Id = 2, UserId = 2, Title = "Album2",
                Photos = new List<Photo>()
                { { new Photo { Id = 2, Title = "Photo2", AlbumId = 2, Url = "b" } },
                { new Photo { Id = 3, Title = "Photo3", AlbumId = 2, Url = "b" } } } },
                new Album { Id = 3, UserId = 2, Title = "Album3",
                Photos = new List<Photo>()
                { new Photo { Id = 4, Title = "Photo4", AlbumId = 3, Url = "c" } } }
            };

            dummyService.Albums = albums;
        }

        [TestMethod]
        public void TestMethod1()
        {

        }
    }
}
