using Ninject.Web.WebApi.OwinHost;
using Ninject.Web.Common.OwinHost;
using Owin;
using System.Web.Http;
using CH.Api.Greeter;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;

namespace Greeter
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
 
            var config = new HttpConfiguration();       
            appBuilder.UseNinjectMiddleware(() => NinjectConfig.CreateKernel.Value);
            appBuilder.UseNinjectWebApi(config);
            WebApiConfig.Register(config);
        }

      
   
    }
}