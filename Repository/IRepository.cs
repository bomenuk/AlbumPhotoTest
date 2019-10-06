using System.Collections.Generic;
using Repository.Contracts.DTOs;

namespace Repository.Contracts
{
    public interface IRepository<T> where T : baseEntity
    {
        IList<T> GetAll();
        T GetById(int id);
    }
}
