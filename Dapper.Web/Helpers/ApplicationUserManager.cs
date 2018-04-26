using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Dapper.Data.Models;
using Dapper.Service.Interfaces;

namespace Dapper.Web.Helpers
{
    public class ApplicationUserManager : UserManager<Account, int>
    {
        public ApplicationUserManager(IUserStore<Account, int> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var accountService = DependencyResolver.Current.GetService<IAccountService>();
            var manager = new ApplicationUserManager(accountService);
            manager.UserValidator = new UserValidator<Account, int>(manager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(15);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            return manager;
        }
    }
}