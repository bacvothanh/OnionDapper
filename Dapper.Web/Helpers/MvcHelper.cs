using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Dapper.Data.Models;
using Dapper.Infrastructure.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Dapper.Data.Enums;

namespace Dapper.Web.Helpers
{
    public static class MvcHelper
    {
        public static CustomPrincipal GetCurrentUser()
        {
            var claimPrincipal = HttpContext.Current.User as ClaimsPrincipal;
            if (claimPrincipal != null)
                return new CustomPrincipal(claimPrincipal.Identity);
            return null;
        }

        public static string HandleUrl(string url)
        {
            var domain = HttpContext.Current?.Request.Url.GetLeftPart(UriPartial.Authority);
            if (url.StartsWith("http"))
            {
                return url;
            }

            return $"{domain}{url}";
        }

        public static string HandleUrl(dynamic url)
        {
            return HandleUrl(url.ToString());
        }

        public static List<SelectListItem> GetSelectListForGender()
        {
            var result = new List<SelectListItem>();
            var values = Enum.GetValues(typeof(Gender));
            foreach (Gender value in values)
            {
                result.Add(new SelectListItem
                {
                    Text = value.GetDescription(),
                    Value = ((int)value).ToString()
                });
            }

            return result;
        }
        
    }

    public static class HtmlCustomerHelper
    {
        public static string IsSelected(this HtmlHelper html, string controllers = "", string actions = "", string cssClass = "selected")
        {
            var viewContext = html.ViewContext;
            var isChildAction = viewContext.Controller.ControllerContext.IsChildAction;

            if (isChildAction)
                viewContext = html.ViewContext.ParentActionViewContext;

            var routeValues = viewContext.RouteData.Values;
            var currentAction = routeValues["action"].ToString();
            var currentController = routeValues["controller"].ToString();

            if (string.IsNullOrEmpty(actions))
                actions = currentAction;

            if (string.IsNullOrEmpty(controllers))
                controllers = currentController;

            var acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
            var acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController) ?
                cssClass : string.Empty;
        }

        public static MvcHtmlString ToJson(this object obj)
        {
            if (obj == null)
                return new MvcHtmlString("{}");
            return
                MvcHtmlString.Create(JsonConvert.SerializeObject(obj,
                    new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }
    }

    public class CookieHelper
    {
        public static T Get<T>(string name, T defaultValue = default(T))
        {
            var cookie = HttpContext.Current.Request.Cookies.Get(name);
            if (cookie != null)
            {
                var value = cookie.Value.ConvertBase64ToString();
                return JsonConvert.DeserializeObject<T>(value);
            }

            return defaultValue;
        }

        public static void Set(string name, object value)
        {
            var valueString = JsonConvert.SerializeObject(value).ConvertStringToBase64();
            var cookie = new HttpCookie(name, valueString) { Expires = DateTime.Now.AddMonths(3) };
            HttpContext.Current.Response.Cookies.Set(cookie);
        }
    }
}