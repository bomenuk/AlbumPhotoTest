using System.Collections.Generic;
using AlbumPhotoFetchService.Contracts.Entities;

namespace AlbumPhotoFetchService.Contracts
{
    public interface IAlbumPhotoService
    {
        IList<Album> GetAllAlbums();
        Album GetAlbum(int albumId);
        IList<Album> GetAlbumsByUserId(int userId);
    }
}
