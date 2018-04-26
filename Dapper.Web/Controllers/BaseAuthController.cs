using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Dapper.Data.Models;
using Dapper.Web.Helpers;

namespace Dapper.Web.Controllers
{
    [CustomAuthorize]
    public class BaseAuthController : BaseController
    {
        private CustomPrincipal _account;
        protected CustomPrincipal CurrentUser
        {
            get
            {
                if (_account == null)
                {
                    var claimPrincipal = System.Web.HttpContext.Current.User as ClaimsPrincipal;
                    if (claimPrincipal != null)
                        _account = new CustomPrincipal(claimPrincipal.Identity);
                }
                return _account;
            }
        }

        private ApplicationUserManager _userManager;

        public ApplicationUserManager ApplicationUserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
                return _userManager;
            }
        }
    }
}