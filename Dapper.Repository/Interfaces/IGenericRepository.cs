using System.Collections.Generic;

namespace Dapper.Repository.Interfaces
{
    public interface IGenericRepository<T>
    {
        T GetById(object id);
        List<T> Gets(object whereConditions);
        List<T> Gets(string whereConditions);
        T Insert(T value);
        int Update(T value);
        int DeleteById(object id);
    }
}
