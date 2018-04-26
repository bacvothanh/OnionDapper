using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dapper.Web.Helpers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params string[] roles)
        {
            if (roles != null)
                this.Roles = string.Join(",", roles);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result =
                   new RedirectResult(new UrlHelper(filterContext.RequestContext).Action("Login", "Auth", new { ReturnUrl = filterContext.HttpContext.Request?.Url?.PathAndQuery }));
        }
    }
}