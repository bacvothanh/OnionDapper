using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Dapper.Data.Models;
using Dapper.Infrastructure;
using Dapper.ViewModel;

namespace Dapper.Service.Interfaces
{
    public interface IAccountService : IGenericService<Account>, IUserStore<Account,int>
    {
        ApplicationResult<Account> Login(string username, string password);
        ApplicationResult ConfirmEmail(string token);
        ApplicationResult<Account> Create(RegisterBindingModel model);


    }
}
