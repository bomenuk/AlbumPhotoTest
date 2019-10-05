using System;
using System.Collections.Generic;
using System.Text;
using Repository.DTOs;

namespace Repository
{
    public interface IPhotoRepository:IRepository<Photo>
    {
        IList<Photo> GetPhotosByAlbumId(int albumId);
    }
}
