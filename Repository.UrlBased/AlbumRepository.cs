using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using Repository.DTOs;

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
