using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Data.Models;
using Dapper.Infrastructure;

namespace Dapper.Repository.Interfaces
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        List<Role> GetByAccountId(int accountId);
        ApplicationResult InsertRoleForAccount(int accountId, string roleName);
        Role GetByName(string roleName);
    }
}
