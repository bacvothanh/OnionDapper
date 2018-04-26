using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Dapper.Data;
using Dapper.Web.Helpers;
using Owin;

[assembly: OwinStartup(typeof(Dapper.Web.Startup))]
namespace Dapper.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                ExpireTimeSpan = TimeSpan.FromDays(10),
                LogoutPath = new PathString("/auth/logout")
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

        }
    }
}