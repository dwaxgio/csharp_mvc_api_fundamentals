using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MVC_API_FUNDAMENTALS
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
                routeTemplate: "api/{controller}/{action}/{id}", // 2. Se agrega tributo que indica la acción
                defaults: new { id = RouteParameter.Optional }
            );

            // 3. Linea para enviar todos los controllers en json serializado
            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
    }
}
