using AlbumPhotoFetchService.Contracts.Entities;
using System.Collections.Generic;
using System.Text;

namespace AlbumPhotoTest.ViewModels
{
    public class AlbumPhotosViewModel
    {
        private IEnumerable<Album> _albums;

        public AlbumPhotosViewModel(IEnumerable<Album> albums)
        {
            _albums = albums;
        }

        public string ConstructAlbumPhotosInformation()
        {
            if (_albums != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var album in _albums)
                {
                    sb.AppendLine($"AlbumID: {album.Id}, UserID: {album.UserId}, Title: {album.Title}");
                    sb.AppendLine("Photos:");
                    foreach (var photo in album.Photos)
                    {
                        sb.AppendLine($"PhotoID: {photo.Id}, Title: {photo.Title}, Url: {photo.Url}");
                    }
                    sb.AppendLine();
                }
                return sb.ToString();
            }
            return string.Empty;
        }
    }
}
