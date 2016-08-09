
using CH.Domain.Greeter.Entities;
using System.Collections.Generic;

namespace CH.Domain.Greeter.DomainServices
{
    public class GreetingDomainService: IGreetingDomainService
    {       
        private readonly IGreeterRepository _greeterRepo;
        public GreetingDomainService(IGreeterRepository greeterRepo)
        {
            _greeterRepo = greeterRepo;
        }

        public IEnumerable<GreetingDataStore> GetGreetings()
        {
            return _greeterRepo.GetGreetings();
        }

        public int CreateGreeting(GreetingDataStore greeting)
        {
            return _greeterRepo.CreateGreeting(greeting);
        }

        public void UpdateGreeting(GreetingDataStore greeting)
        {
            _greeterRepo.UpdateGreeting(greeting);
        }

    }

}


