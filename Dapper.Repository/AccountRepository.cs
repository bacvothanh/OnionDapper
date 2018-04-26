using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Data.Models;
using Dapper.Repository.Interfaces;

namespace Dapper.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
    }
}
