using System.Collections.Generic;
using AlbumPhotoFetchService.Contracts.Entities;
using AlbumPhotoTest.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlbumPhotoTest.Tests
{
    [TestClass]
    public class AlbumPhotosViewModelTests
    {        
        private IList<Album> albums;

        [TestInitialize]
        public void Initialize()
        {
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
        }

        [TestMethod]
        public void Check_If_AlbumPhotosViewModel_Can_Produce_Correct_Output_String()
        {
            var correctResult =
                @"AlbumID: 1, UserID: 1, Title: Album1
Photos:
PhotoID: 1, Title: Photo1, Url: a

AlbumID: 2, UserID: 2, Title: Album2
Photos:
PhotoID: 2, Title: Photo2, Url: b
PhotoID: 3, Title: Photo3, Url: b

AlbumID: 3, UserID: 2, Title: Album3
Photos:
PhotoID: 4, Title: Photo4, Url: c

";

            var viewModel = new AlbumPhotosViewModel(albums);
            var result = viewModel.ConstructAlbumPhotosInformation();
            Assert.AreEqual(correctResult, result);
        }
    }
}
