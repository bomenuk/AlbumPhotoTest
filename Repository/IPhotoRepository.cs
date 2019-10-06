using System.Collections.Generic;
using Repository.Contracts.DTOs;

namespace Repository.Contracts
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        IList<Photo> GetPhotosByAlbumId(int albumId);
    }
}
