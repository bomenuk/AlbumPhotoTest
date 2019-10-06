using System.Collections.Generic;
using System.Linq;
using Repository.Contracts;
using Repository.Contracts.DTOs;

namespace Repository.UrlBased
{
    public class PhotoRepository: baseRepository<Photo>, IPhotoRepository
    {
        public IList<Photo> GetPhotosByAlbumId(int albumId)
        {
            return GetAll().Where(p => p.AlbumId == albumId).ToList();
        }
    }
}
