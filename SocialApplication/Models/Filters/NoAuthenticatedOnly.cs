using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace SocialApplication.Models.Filters
{
    public class NoAuthenticatedOnly:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return ;
            }

            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary {
                {"action","Index" },
                {"controller","Home" }
            });
        }
    }
}