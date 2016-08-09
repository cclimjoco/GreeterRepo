using CH.Application.Greeter.Datatransfer;
using System.Collections.Generic;


namespace CH.Application.Greeter.Service
{
    public interface IGreetingService
    {
        IEnumerable<Greeting> GetGreetings();
        int CreateGreeting(Greeting greeting);
        void UpdateGreeting(Greeting greeting);
    }
}
