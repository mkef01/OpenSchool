using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OpenSchool
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ChatRoom",
                url: "chat",
                defaults: new { controller = "Chat", action = "Index" }
            );
            routes.MapRoute(
                name: "SendMessage",
                url: "send_message",
                defaults: new { controller = "Chat", action = "SendMessage" }
        );
            routes.MapRoute(
                name: "ChatContact",
                url: "conversation/conversation/{contact}",
                defaults: new { controller = "Chat", action = "ConversationWithContact", contact= UrlParameter.Optional }
            );
            routes.MapRoute(
        name: "PusherAuth",
        url: "pusher/auth",
        defaults: new { controller = "Auth", action = "AuthForChannel" }
    );

        }
    }
}
