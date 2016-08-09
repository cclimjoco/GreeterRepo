
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CH.Api.Greeter.Controllers;
using Ninject;
using CH.Application.Greeter.Service;
using System.Web.Http.Results;
using Moq;
using CH.Application.Greeter.Datatransfer;
using System.Collections.Generic;
using System.Web.Http;
using System.Net.Http;

namespace CH.Greeter.Tests
{
    [TestClass]
    public class GreeterApiTest
    {
        
        private static IKernel GetKernel()
        {
            IKernel kernel = new StandardKernel(new NinjectSetup());
            //TODO Set bindings from other libraries when necessary here
            //kernel.Load("CH.Domain.Greeter.dll");
            return kernel;
        }

        private static IGreetingService GetGreetingServiceMock()
        {
            var greetingServiceMock = new Mock<IGreetingService>();
            IList<Greeting> greetings = new List<Greeting>();
            greetings.Add(new Greeting { ID = 101, Message = "Hello World", Language = "English" });
            greetings.Add(new Greeting { ID = 102, Message = "Ciao mondo", Language = "Italian" });
            greetings.Add(new Greeting { ID = 103, Message = "Hola mundo", Language = "Spanish" });
            greetingServiceMock.Setup(m => m.GetGreetings()).Returns(greetings);
            greetingServiceMock.Setup(m => m.UpdateGreeting(It.IsAny<Greeting>()));
            greetingServiceMock.Setup(m => m.CreateGreeting(It.IsAny<Greeting>()));
            return greetingServiceMock.Object;
        }

        private static GreeterController GetGreetingController()
        {
            var greetService = GetGreetingServiceMock();
            var controller = new GreeterController(greetService);
            HttpConfiguration configuration = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            controller.Request = request;
            controller.Request.Properties["MS_HttpConfiguration"] = configuration;
            return controller;
        }
        private string CreateTextWithStringLength(int num)
        {
            string text = "";
            for (int i = 0; i <= num; i++)
            {
                text += "0";
            }
            return text;
        }

        [TestMethod]
        public void GetGreeting_WhenIDNoMatch_NotFoundResult()
        {
            //Arrange
            var controller = GetGreetingController();
            //Act
            var greeting = controller.GetGreeting(360);
            //Assert           
            Assert.IsInstanceOfType(greeting, typeof(NotFoundResult));
        }

        [TestMethod]
        public void CreateGreeting_WhenNullObject_InvalidModelState()
        {
            //Arrange
            var controller = GetGreetingController();
            //Act
            var greeting = controller.Post(null);
            //Assert
            Assert.IsInstanceOfType(greeting, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public void CreateGreeting_WhenMessageLengthOver_InvalidModelState()
        {
            //Arrange
            var controller = GetGreetingController();
            //Act
            var greeting = controller.Post(new Greeting {ID=123, Language="English" ,Message= CreateTextWithStringLength(251) });
            //Assert
            Assert.IsInstanceOfType(greeting, typeof(InvalidModelStateResult));
        }

       
        [TestMethod]
        public void UpdateGreeting_WhenIDZero_NotFoundResult()
        {
            //Arrange
            var controller = GetGreetingController();
            //Act
            var greeting = controller.Put(new Greeting { ID = 0});
            //Assert
            Assert.IsInstanceOfType(greeting, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public void UpdateGreeting_WhenNullObject_InvalidModelState()
        {
            //Arrange
            var controller = GetGreetingController();
            //Act
            var greeting = controller.Put(null);
            //Assert
            Assert.IsInstanceOfType(greeting, typeof(InvalidModelStateResult));
        }


        [TestMethod]
        public void UpdateGreeting_WhenMessageLengthOver_InvalidModelState()
        {
            //Arrange
            var controller = GetGreetingController();
            //Act
            var greeting = controller.Put(new Greeting { ID = 123, Language = "English", Message = CreateTextWithStringLength(251) });
            //Assert
            Assert.IsInstanceOfType(greeting, typeof(InvalidModelStateResult));
        }

    }
}
