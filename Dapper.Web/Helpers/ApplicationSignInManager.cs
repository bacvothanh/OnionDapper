using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Dapper.Data.Models;

namespace Dapper.Web.Helpers
{
    public class ApplicationSignInManager : SignInManager<Account, int>
    {
        public ApplicationSignInManager(UserManager<Account, int> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}