using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Dapper.Web.Helpers;
using Microsoft.AspNet.Identity.Owin;

namespace Dapper.Web.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("vi");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return base.BeginExecuteCore(callback, state);
        }

        protected UrlHelper ActionHelper => new UrlHelper(HttpContext.Request.RequestContext);
        public string Domain => HttpContext?.Request?.Url?.GetLeftPart(UriPartial.Authority);

        public void HandleFailResult(List<string> errors)
        {
            TempData["Errors"] = errors;
        }

        public void HandleSuccessResult(string message)
        {
            TempData["Success"] = new List<string> { message };
        }

        public void HandleExeption(Exception ex)
        {
            var errors = new List<string> { ex.Message };
            TempData["Errors"] = errors;
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (TempData["ModelState"] != null && !ModelState.Equals(TempData["ModelState"]))
                ModelState.Merge((ModelStateDictionary)TempData["ModelState"]);

            base.OnActionExecuted(filterContext);
        }

        protected void SaveModelState()
        {
            TempData["ModelState"] = ModelState;
        }

        protected List<string> GetModalStateErrors()
        {
            return (from modalState in ModelState.Values
                    from error in modalState.Errors
                    select !string.IsNullOrEmpty(error.ErrorMessage) ? error.ErrorMessage : error.Exception.Message).ToList();
        }

        public ActionResult PreviousPage()
        {
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.AbsoluteUri);
            return RedirectToAction("Index", "Patient");
        }

        public ActionResult CurrentPage()
        {
            if (Request.Url != null)
                return Redirect(Request.Url.AbsoluteUri);
            return RedirectToAction("Index", "Patient");
        }


        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("/");
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager ApplicationSignInManager
        {
            get
            {
                if (_signInManager == null)
                {
                    _signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                }
                return _signInManager;
            }
        }
    }
}