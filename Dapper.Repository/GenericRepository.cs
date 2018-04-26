using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNet.Identity;
using Dapper.Data.Interfaces;
using Dapper.Infrastructure.Extensions;
using Dapper.Repository.Interfaces;

namespace Dapper.Repository
{
    public abstract class GenericRepository<T> :  IDisposable, IGenericRepository<T>
    {
        private readonly string _connectionString = ConfigurationManager.
    ConnectionStrings["DefaultConnection"].ConnectionString;
        private SqlConnection _connection;
        protected SqlConnection Connection => _connection ?? (_connection = GetOpenConnection());

        private SqlConnection GetOpenConnection(bool mars = false)
        { 
            var cs = _connectionString;
            if (mars)
            {
                var scsb = new SqlConnectionStringBuilder(cs)
                {
                    MultipleActiveResultSets = true
                };
                cs = scsb.ConnectionString;
            }

            var connection = new SqlConnection(cs);
            connection.Open();
            return connection;
        }

        private SqlConnection GetClosedConnection()
        {
            var conn = new SqlConnection(_connectionString);
            if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");
            return conn;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public T GetById(object id)
        {
            var table = typeof(T).Name.ConvertToPlural();
            var sql = $"SELECT * FROM {table} WHERE Id=@id";
            return Connection.Query<T>(sql, new {id = id}).FirstOrDefault();
        }
        

        public List<T> Gets(object whereConditions)
        {
            return Connection.GetList<T>(whereConditions).ToList();
        }

        /// <summary>
        /// </summary>
        /// <param name="whereConditions">ex : where age = 10 or Name like '%Smith%'</param>
        /// <returns></returns>
        public List<T> Gets(string whereConditions)
        {
            return Connection.GetList<T>(whereConditions).ToList();
        }

        private void UpdateTracking(T value, bool isInsert)
        {
            var identityId = Thread.CurrentPrincipal.Identity.GetUserId().ParseToInt();
            var timeNow = DateTimeOffset.Now;
            var auditEntity = value as IAuditEntity;
            if (auditEntity !=null)
            {
                if (isInsert)
                {
                    auditEntity.CreateBy = identityId;
                    auditEntity.TimeCreatedOffset = timeNow;
                }

                auditEntity.ModifyBy = identityId;
                auditEntity.TimeModifyOffset = timeNow;
            }

        }
        
        public T Insert(T value)
        {
            UpdateTracking(value, true);
            var newId = Connection.Insert(value);
            if (newId.HasValue == false)
            {
                throw new Exception("Cannot insert record to the database");
            }

            var returnValue = value as IEntity<int>;
            if (returnValue !=null)
            {
                returnValue.Id = newId.Value;
                return (T) returnValue;
            }
            return value;
        }

        public int Update(T value)
        {
            UpdateTracking(value, false);
            var numberRecordAffected = Connection.Update(value);
            return numberRecordAffected;
        }

        public int DeleteById(object id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                var deleteEntity = entity as IDelete;
                if (deleteEntity != null)
                {
                    var identityId = Thread.CurrentPrincipal.Identity.GetUserId().ParseToInt();
                    var timeNow = DateTimeOffset.Now;
                    deleteEntity.IsDeleted = true;
                    deleteEntity.DeleteBy = identityId;
                    deleteEntity.TimeDeleteOffset = timeNow;
                    var updateResult = Connection.Update(entity);
                    return updateResult;
                }

                var numberRecordAffected = Connection.Delete<T>(entity);
                return numberRecordAffected;
            }

            return 0;
        }
    }
}
