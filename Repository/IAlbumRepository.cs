using System;
using System.Collections.Generic;
using System.Text;
using Repository.DTOs;

namespace Repository
{
    public interface IAlbumRepository:IRepository<Album>
    {
        IList<Album> GetAlbumsByUserId(int userId);
    }
}
