using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Dapper.Infrastructure;
using Dapper.Repository.Interfaces;
using Dapper.Service.Interfaces;

namespace Dapper.Service
{
    public abstract class GenericService<TEntity> : IGenericService<TEntity>
    {
        private readonly IGenericRepository<TEntity> _repository;
        protected GenericService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public TEntity Insert(TEntity value)
        {
            var result = _repository.Insert(value);
            return result;
        }

        public T GetById<T>(object id)
        {
            return AutoMapper.Mapper.Map<T>(_repository.GetById(id));
        }

        public List<T> Gets<T>()
        {
            return _repository.Gets("").AsQueryable().ProjectTo<T>().ToList();
        }

        public ApplicationResult Update(TEntity value)
        {
            var result = _repository.Update(value);
            if (result <= 0)
               return ApplicationResult.Fail("Cannot update the record in the database");
            return ApplicationResult.Ok();
        }

        public ApplicationResult Delete(object id)
        {
            var result = _repository.DeleteById(id);
            if (result <= 0)
                return ApplicationResult.Fail("Cannot delete the record in the database");
            return ApplicationResult.Ok();
        }
    }
}
