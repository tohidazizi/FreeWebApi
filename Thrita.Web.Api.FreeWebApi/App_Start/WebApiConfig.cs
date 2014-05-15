using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Thrita.Web.Api.FreeWebApi.Models.DependencyInjection;

namespace Thrita.Web.Api.FreeWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Note: A simple Dependency Injection Solution by replacing DefaultHTTPControllerActivator
            config.Services.Replace(
                typeof(IHttpControllerActivator),
                new ThritaHttpControllerActivator());
        }
    }
}
