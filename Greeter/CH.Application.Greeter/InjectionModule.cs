
using Ninject.Modules;
using CH.Domain.Greeter.DomainServices;

namespace CH.Application.Greeter.Service
{
    public class ApplicationInjectionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGreetingDomainService>().To<GreetingDomainService>();
        }

    }

}


