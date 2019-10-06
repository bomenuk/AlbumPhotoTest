using AlbumPhotoFetchService.Contracts;
using AlbumPhotoTest.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AlbumPhotoTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumPhotoService _albumPhotoService;        

        public AlbumController(IAlbumPhotoService albumPhotoService)
        {
            _albumPhotoService = albumPhotoService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var viewModel = new AlbumPhotosViewModel(_albumPhotoService.GetAllAlbums());
            return viewModel.ConstructAlbumPhotosInformation();
        }

        // GET api/values/5
        [HttpGet("user/{id}")]
        public ActionResult<string> Get(int id)
        {
            var viewModel = new AlbumPhotosViewModel(_albumPhotoService.GetAlbumsByUserId(id));
            return viewModel.ConstructAlbumPhotosInformation();
        }
    }
}
