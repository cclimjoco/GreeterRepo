using CH.Application.Greeter.Datatransfer;
using CH.Domain.Greeter.DomainServices;
using CH.Domain.Greeter.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CH.Application.Greeter.Service
{
    public class GreetingAppService :IGreetingService
    {
        private readonly IGreetingDomainService _greetingService;

        public GreetingAppService(IGreetingDomainService  greetingService)
        {
            _greetingService = greetingService;
        }
        public IEnumerable<Greeting> GetGreetings()
        {
            //Map Greetings to Dto
           return GreeterServiceDtoMapper.MapToGreetingDto(_greetingService.GetGreetings());
       
        }

        public int CreateGreeting(Greeting model)
        {
            var greeting = new GreetingDataStore {
                ID =0, //auto number to be set once saved
                Language = model.Language,
                Message = model.Message
            };

            return _greetingService.CreateGreeting(greeting);
        }

        public void UpdateGreeting(Greeting greeting)
        {
             _greetingService.UpdateGreeting(GreeterServiceDomainMapper.MapToGreetingDomain(greeting));
        }
    }

    public static class GreeterServiceDtoMapper
    {
        public static IEnumerable<Greeting> MapToGreetingDto(IEnumerable<GreetingDataStore> greetingDataStore)
        {
            return greetingDataStore.Select(g => new Greeting { ID = g.ID, Language = g.Language, Message = g.Message });
        }
    }

    public static class GreeterServiceDomainMapper
    {
        public static GreetingDataStore MapToGreetingDomain(Greeting greeting)
        {
            return new GreetingDataStore { ID = greeting.ID, Language = greeting.Language, Message = greeting.Message };
        }
    }
}
