using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Net.Http;

namespace CH.Api.Greeter
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "GetGreetings",
                routeTemplate: "greetings",
                defaults: new { controller = "Greeter", action = "Get" }
                );

            config.Routes.MapHttpRoute(
               name: "AddGreeting",
               routeTemplate: "addgreeting",
               defaults: new { controller = "Greeter", action = "Post" }
               );

            config.Routes.MapHttpRoute(
             name: "UpdateGreeting",
             routeTemplate: "updategreeting",
             defaults: new { controller = "Greeter", action = "Put" }
             );

            config.Formatters.Clear();
            var jsonFormatter = new JsonMediaTypeFormatter();
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));
            
            
        }
          

    }
    public class JsonContentNegotiator : IContentNegotiator
    {
        private readonly JsonMediaTypeFormatter _jsonFormatter;

        public JsonContentNegotiator(JsonMediaTypeFormatter formatter)
        {
            _jsonFormatter = formatter;
        }



        public ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
        {
            var result = new ContentNegotiationResult(_jsonFormatter, new MediaTypeHeaderValue("application/json"));
            return result;
        }
    }
}