using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdvWorksPL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{min}/{max}",
                defaults: new { controller = "AdvWorksMVC", action = "DisplayDeptDetailsWebAPI", min = UrlParameter.Optional , max = UrlParameter.Optional}
            );
        }
    }
}
