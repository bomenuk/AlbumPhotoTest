using System.Collections.Generic;
using Repository.Contracts.DTOs;

namespace Repository.Contracts
{
    public interface IAlbumRepository : IRepository<Album>
    {
        IList<Album> GetAlbumsByUserId(int userId);
    }
}
