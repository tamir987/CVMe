using CVMe.Common.Settings.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;


namespace CVMe
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = DependencyResolver.Current;

            var applicationSettings = container.GetService<IApplicationSettings>();
            // Web API configuration and services
            // Web API routes
            config.MapHttpAttributeRoutes();
          
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
