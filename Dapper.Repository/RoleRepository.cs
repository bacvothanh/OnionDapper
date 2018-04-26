using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Data.Models;
using Dapper.Infrastructure;
using Dapper.Repository.Interfaces;

namespace Dapper.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public List<Role> GetByAccountId(int accountId)
        {
            var query = @"SELECT R.* FROM [dbo].[Roles] (NOLOCK) R
                        LEFT JOIN[dbo].[AccountRoles] (NOLOCK) AR ON R.Id = AR.RoleId
                        LEFT JOIN[dbo].[Accounts] (NOLOCK) A ON A.Id = AR.AccountId
                        WHERE A.Id=@id";
            var roles = Connection.Query<Role>(query, new {id = accountId }).ToList();
            return roles;
        }

        public Role GetByName(string roleName)
        {
            var query = @"SELECT * FROM [dbo].[Roles] (NOLOCK) R
                        WHERE R.Name = @name";
            var result = Connection.Query<Role>(query, new { name = roleName }).FirstOrDefault();
            return result;
        }

        public ApplicationResult InsertRoleForAccount(int accountId, string roleName)
        {
            var queryInsert = @"DECLARE @roleId INT = (SELECT Id FROM [dbo].[Roles] (NOLOCK) WHERE Name=@roleName)
                                IF(NOT EXISTS(
                                SELECT 1 FROM [dbo].[AccountRoles] (NOLOCK) AR WHERE AR.AccountId =@accountId AND RoleId=@roleId))
                                BEGIN
	                                INSERT INTO [dbo].[AccountRoles]
                                           ([AccountId]
                                           ,[RoleId])
                                     VALUES
                                           (@accountId,@roleId)
	                                SELECT 1
                                END
                                ELSE 
                                BEGIN 
	                                SELECT 0
                                END";
            var result = Connection.Execute(queryInsert, new {accountId, roleName});
            if (result > 0)
                return ApplicationResult.Ok();
            return ApplicationResult.Fail("Không thể cấp quyền cho tài khoản này");
        }

    }
}
