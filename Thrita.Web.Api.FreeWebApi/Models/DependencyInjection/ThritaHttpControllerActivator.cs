using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Thrita.Web.Api.FreeWebApi.Controllers;

namespace Thrita.Web.Api.FreeWebApi.Models.DependencyInjection
{
    public class ThritaHttpControllerActivator : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            string defaultRepositoryTypeName = ConfigurationManager.AppSettings["Thrita.DefaultRepositoryTypeName"];
            var defaultRepositoryType = Type.GetType(defaultRepositoryTypeName, true);
            var defaultRepository = (IRepository)Activator.CreateInstance(defaultRepositoryType);

            // ********************************************
            //TODO: replace following lines with an IoC container
            //      http://blog.ploeh.dk/2012/10/03/DependencyInjectionInASPNETWebAPIWithCastleWindsor.aspx
            // ********************************************
            if (controllerType.Name == typeof(ProductsController).Name)
                return new ProductsController(defaultRepository);

            return (IHttpController)Activator.CreateInstance(controllerType);
        }
    }
}