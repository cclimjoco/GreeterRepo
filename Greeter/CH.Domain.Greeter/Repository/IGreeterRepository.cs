using CH.Domain.Greeter.Entities;
using System.Collections.Generic;

namespace CH.Domain.Greeter.DomainServices
{
    public interface IGreeterRepository
    {
        IEnumerable<GreetingDataStore> GetGreetings();
        int CreateGreeting(GreetingDataStore greeting);
        void UpdateGreeting(GreetingDataStore greeting);
    }
}