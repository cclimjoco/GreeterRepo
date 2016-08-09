
using System.Collections.Generic;
using System.Web.Http;
using CH.Application.Greeter.Service;
using CH.Application.Greeter.Datatransfer;
using System.Linq;

namespace CH.Api.Greeter.Controllers
{

    public class GreeterController : ApiController
    {

        private readonly IGreetingService _greetingService;
     
         public GreeterController(IGreetingService greetingService)
          {
              _greetingService = greetingService;
          }

        //Get all greeting records
        public IHttpActionResult Get()
        {
            IEnumerable<Greeting> greetings = _greetingService.GetGreetings();
            greetings = _greetingService.GetGreetings();
            return Ok(greetings);
        }

        //Find a specific greeting by ID
        public IHttpActionResult GetGreeting(int greetingID)
        {
            IEnumerable<Greeting> greetings = _greetingService.GetGreetings();
            var greetingMatch = greetings.FirstOrDefault(g => g.ID == greetingID);
            if (greetingMatch == null)
            {
                return NotFound();
            }
            return Ok(greetingMatch);
        }


        public IHttpActionResult Post(Greeting greeting)
        {
            this.Validate(greeting);
            if (greeting == null ||!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int Id = _greetingService.CreateGreeting(greeting);
            return Ok(Id);
        }

        public IHttpActionResult Put(Greeting greeting)
        {
            this.Validate(greeting);
            if (greeting==null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            _greetingService.UpdateGreeting(greeting);
            return Ok(greeting);

        }
  
    }
}
