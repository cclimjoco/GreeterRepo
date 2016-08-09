
using System.Configuration;
using Ninject.Modules;
using System.Data;
using System.Data.SqlClient;

namespace CH.Domain.Greeter.DomainServices
{
    public class DomainInjectionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbConnection>().ToMethod(context => new SqlConnection(ConfigurationManager.ConnectionStrings["GreeterDB"].ConnectionString));
            Bind<IDBConnectionService>().To<DBConnectionServiceAgent>();
            Bind<IGreeterRepository>().To<GreeterRepository>();
            Bind<IGreetingDomainService>().To<GreetingDomainService>();
            Bind<ILog>().To<Logger>();
        }

    }

}


