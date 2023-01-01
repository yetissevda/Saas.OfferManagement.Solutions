using System.IO;
using System.Reflection;
using System.Xml;
using log4net;
using log4net.Repository;


namespace Saas.Core.CrossCuttingConcerns.Logging.Log4Net
{
    public class LoggerServiceBase
    {
        private readonly ILog _log;

        protected LoggerServiceBase(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(File.OpenRead("log4net.config"));
            ILoggerRepository logggeRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(),typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(logggeRepository,xmlDocument["log4net"]);
            _log = LogManager.GetLogger(logggeRepository.Name,name);
            
        }

        private bool IsInfoEnabled => _log.IsInfoEnabled;
        private bool IsDebugEnabled => _log.IsDebugEnabled;
        private bool IsWarnEnabled => _log.IsWarnEnabled;
        private bool IsFatalEnabled => _log.IsFatalEnabled;
        private bool IsErrorEnabled => _log.IsErrorEnabled;
        public void Info(LogDetail logMessage)
        {
            if (IsInfoEnabled)
                _log.Info(logMessage);

        }

        public void Debug(LogDetail logMessage)
        {
            if (IsDebugEnabled)
                _log.Debug(logMessage);

        }

        public void Warn(LogDetail logMessage)
        {
            if (IsWarnEnabled)
                _log.Warn(logMessage);

        }

        public void Fatal(LogDetail logMessage)
        {
            if (IsFatalEnabled)
                _log.Fatal(logMessage);

        }

        public void Error(LogDetailWithException logMessage)
        {
            //buraya birde mail gonderim ekleyelim.
            if (IsErrorEnabled)
                _log.Error(logMessage);



        }
    }

}
