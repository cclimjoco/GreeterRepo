using CH.Domain.Greeter.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace  CH.Domain.Greeter.DomainServices
{
    public class GreeterRepository : IGreeterRepository
    {
        private readonly IDBConnectionService _dbConnectionService;
        private readonly ILog _logger;
        private static  string greeterRepoSelectCacheName = "greeterRepoSelect";

        public GreeterRepository(IDBConnectionService dbConnectionService,ILog logger)
        {
            _dbConnectionService = dbConnectionService;
            _logger = logger;
        }

        private IEnumerable<GreetingDataStore> SelectGreetings()
        {
            IList<GreetingDataStore> greetings = new List<GreetingDataStore>();
            greetings.Add(new GreetingDataStore { ID = 101, Message = "Hello World", Language = "English" });
            greetings.Add(new GreetingDataStore { ID = 102, Message = "Ciao mondo", Language = "Italian" });
            greetings.Add(new GreetingDataStore { ID = 103, Message = "Hola mundo", Language = "Spanish" });
            return greetings;
            //TODO connect to database for future use
            //return _dbConnectionService.Query<GreetingDataStore>("[greeter].[GetGreetings] ");
        }

        public IEnumerable<GreetingDataStore> GetGreetings()
        {
          return CacheHelper.GetCachedData(ref greeterRepoSelectCacheName, SelectGreetings);
          
        }

        public int CreateGreeting(GreetingDataStore greeting)
        {
            int maxId=0;
            try
            {
                List<GreetingDataStore> greetings = GetGreetings().ToList();
                maxId = greetings.Max(g => g.ID);
                greeting.ID = maxId + 1;
                greetings.Add(greeting);
                CacheHelper.UpdateCachedData(ref greeterRepoSelectCacheName, greetings);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message, ex.StackTrace);
            }
           
            return maxId;
            //TODO connect to database for future use
            //return _dbConnectionService.QueryFirst("[greeter].[CreateGreeting] ");
        }

        public void UpdateGreeting(GreetingDataStore greeting)
        {
            try
            {
                //This is an in memory update for testing and differs from an actual dbContext update
                var greetings = GetGreetings().ToList();
                if (greetings != null && greetings.Any())
                {
                    var greetingMatch = GetGreetings().FirstOrDefault(g => g.ID == greeting.ID);
                    if (greetingMatch != null)
                    {
                        var index = greetings.IndexOf(greetingMatch);
                        greetings.RemoveAt(index);
                        greetingMatch.Language = greeting.Language;
                        greetingMatch.Message = greeting.Message;
                        greetings.Insert(index, greetingMatch);
                        CacheHelper.UpdateCachedData(ref greeterRepoSelectCacheName, greetings);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message, ex.StackTrace);
            }
          
        } 
    }
}
