using System.Collections.Generic;
using AlbumPhotoService.Contracts.Entities;

namespace AlbumPhotoService.Contracts
{
    public interface IAlbumPhotoService
    {
        Album GetAlbum(int albumId);
        IList<Album> GetAlbumsForUser(int userId);
    }
}
