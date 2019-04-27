using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TMDT
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("{*botdetect}",
                  new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "TMDT.Controllers" }
            );
            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "TMDT.Controllers" }
            );
            routes.MapRoute(
                name: "Check Username",
                url: "check-username",
                defaults: new { controller = "User", action = "CheckUsername", id = UrlParameter.Optional },
                namespaces: new[] { "TMDT.Controllers" }
            );
            routes.MapRoute(
                name: "About",
                url: "gioi-thieu",
                defaults: new { controller = "About", action = "About", id = UrlParameter.Optional },
                namespaces: new[] { "TMDT.Controllers" }
            );
            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Contact", id = UrlParameter.Optional },
                namespaces: new[] { "TMDT.Controllers" }
            );
            routes.MapRoute(
                name: "News",
                url: "tin-tuc/{metatitle}-{id}",
                defaults: new { controller = "Content", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "TMDT.Controllers" }
            );
            routes.MapRoute(
                name: "Content Detail",
                url: "tin-tuc",
                defaults: new { controller = "Content", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TMDT.Controllers" }
                );
            routes.MapRoute(
                name: "Tag",
                url: "tag/{tagId}",
                defaults: new { controller = "Content", action = "Tag", id = UrlParameter.Optional },
                namespaces: new[] { "TMDT.Controllers" }
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TMDT.Controllers" }
            );
        }
    }
}
