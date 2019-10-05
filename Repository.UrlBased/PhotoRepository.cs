using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repository.DTOs;

namespace Repository.UrlBased
{
    public class PhotoRepository: baseRepository<Photo>
    {
        public IList<Photo> GetPhotosByAlbum(int albumId)
        {
            return GetAll().Where(p => p.AlbumnId == albumId).ToList();
        }
    }
}
