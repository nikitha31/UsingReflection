using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using UsingReflection.Models;

namespace UsingReflection
{
    public class Authorizing : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //Check Session is Empty Then set as Result is HttpUnauthorizedResult 
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["User"])))
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            var user = (Userss)(filterContext.HttpContext.Session["User"]);
            ReflectionsEntities db = new ReflectionsEntities();
            var authorizedUrls = db.URLMappingToRoles.Where(a => a.RoleName.Equals(user.RoleName)).ToList();
            var currentURL = $"{filterContext.RouteData.Values["Controller"]}/{filterContext.RouteData.Values["Action"]}";
            var currentURLId = db.URLs.Where(a => a.URL1.Equals(currentURL)).ToList();
            foreach (var url in authorizedUrls)
            {
                if (url.URLID.Equals(currentURLId[0].Id))
                    return;
            }
            filterContext.Result = new HttpUnauthorizedResult();

        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "NoAccess"
                };
            }
        }
    }
}
