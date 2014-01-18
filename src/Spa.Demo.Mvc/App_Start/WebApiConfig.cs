using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Spa.Demo.Mvc.App_Start
{
    public class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var jsonSettings = new JsonSerializerSettings
                                   {
                                       ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                       DefaultValueHandling = DefaultValueHandling.Ignore,
                                       NullValueHandling = NullValueHandling.Ignore,
                                       ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                   };
            jsonSettings.Converters.Add(new StringEnumConverter {CamelCaseText = true});
            config.Formatters.JsonFormatter.SerializerSettings = jsonSettings;

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );
        }
    }
}