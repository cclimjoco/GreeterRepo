using CH.Application.Greeter.Service;
using CH.Domain.Greeter.DomainServices;
using Ninject;
using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;

namespace CH.Api.Greeter
{
    public static class NinjectConfig
    {
        public static Lazy<IKernel> CreateKernel = new Lazy<IKernel>(() =>
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            RegisterServices(kernel);

            return kernel;
        });

        private static void RegisterServices(KernelBase kernel)
        {
            //Initialize Domain Bindings
            kernel.Load("CH.Domain.Greeter.dll");
            //Initialize Service Bindings Here
            kernel.Bind<IGreetingService>().To<GreetingAppService>();
        }

      
    }
}
