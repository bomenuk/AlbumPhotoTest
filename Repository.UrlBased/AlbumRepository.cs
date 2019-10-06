using System.Collections.Generic;
using System.Linq;
using Repository.Contracts;
using Repository.Contracts.DTOs;

namespace Repository.UrlBased
{
    public class AlbumRepository:baseRepository<Album>, IAlbumRepository
    {
        public IList<Album> GetAlbumsByUserId(int userId)
        {
            return GetAll().Where(a => a.UserId == userId).ToList();
        }
    }
}
