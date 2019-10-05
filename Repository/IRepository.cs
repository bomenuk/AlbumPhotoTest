using System.Collections.Generic;
using Repository.Contracts.DTOs;

namespace Repository
{
    public interface IRepository<T> where T:baseEntity
    {
        IList<T> GetAll();
        T GetById(int id);
    }
}
