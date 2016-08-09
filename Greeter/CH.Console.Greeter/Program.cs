using CH.Application.Greeter.Datatransfer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;


namespace CH.Greeter.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            PrintGreeting();

        }

        private static void PrintGreeting()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["GreeterApiUrl"].ToString());
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();

                IEnumerable<Greeting> greetings = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Greeting>>(responseText);
                Greeting greeting = greetings.FirstOrDefault();
                if (greeting != null)
                {
                    Console.WriteLine(greeting.Message);
                }
                Console.ReadKey();
            }
        }
    }
}
