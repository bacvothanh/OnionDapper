using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Infrastructure;

namespace Dapper.Service.Interfaces
{
    public interface IGenericService<TEntity>
    {
        TEntity Insert(TEntity value);
        T GetById<T>(object id);
        List<T> Gets<T>();
        ApplicationResult Update(TEntity value);
        ApplicationResult Delete(object id);
    }
}
