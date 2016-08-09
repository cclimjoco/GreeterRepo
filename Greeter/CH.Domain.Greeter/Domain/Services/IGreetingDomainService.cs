using CH.Domain.Greeter.Entities;
using System.Collections.Generic;


namespace CH.Domain.Greeter.DomainServices
{
    public interface IGreetingDomainService
    {
        IEnumerable<GreetingDataStore> GetGreetings();
        int CreateGreeting(GreetingDataStore greeting);
        void UpdateGreeting( GreetingDataStore greeting);
    }
}
