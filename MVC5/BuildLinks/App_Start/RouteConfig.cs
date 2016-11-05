using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BuildLinks
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
                name: "ForRentDistrictVN",
                url: "can-ho-cho-thue/quan-{id}",
                defaults: new { controller = "Landing", action = "Details" }
            );


            routes.MapRoute(
                name: "ForRentDistrictEN",
                url: "eng/for-rent/district-{id}",
                defaults: new { controller = "Landing", action = "Details" }
            );

            routes.MapRoute(
                name: "ForRentVN",
                url: "cho-thue",
                defaults: new { controller = "Landing", action = "Index" }
            );

            routes.MapRoute(
                name: "ForRentEN",
                url: "{for-rent}",
                defaults: new { controller = "Landing", action = "Index" }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
