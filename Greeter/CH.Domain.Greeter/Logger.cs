
namespace CH.Domain.Greeter
{
    public  class Logger:ILog
    {
        private log4net.ILog _logger;
        public Logger()
        {
            log4net.Config.XmlConfigurator.Configure();
            _logger = log4net.LogManager.GetLogger(GetType());
        }
        public  void Log(string message,string stacktrace)
        {
            _logger.Error(message);
            _logger.Error(stacktrace);
        }
    }
}
