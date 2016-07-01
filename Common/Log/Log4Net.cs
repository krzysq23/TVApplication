using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common.Log
{
    public class Log4Net : ILogger
    {
        private log4net.ILog log;

        public static ILogger logger = new Log4Net();

        public Log4Net()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger("Log4");
        }

        public void Information(string message)
        {
            log.Info(message);
        }

        public void Information(string fmt, params object[] vars)
        {
            log.InfoFormat(fmt, vars);
        }

        public void Information(Exception exception, string fmt, params object[] vars)
        {
            log.Info(FormatExceptionMessage(exception, fmt, vars));
        }

        public void Warning(string message)
        {
            log.Warn(message);
        }

        public void Warning(string fmt, params object[] vars)
        {
            log.WarnFormat(fmt, vars);
        }

        public void Warning(Exception exception, string fmt, params object[] vars)
        {
            log.Warn(FormatExceptionMessage(exception, fmt, vars));
        }

        public void Error(string message)
        {
            log.Error(message);
        }

        public void Error(string fmt, params object[] vars)
        {
            log.ErrorFormat(fmt, vars);
        }

        public void Error(Exception exception, string fmt, params object[] vars)
        {
            log.Error(FormatExceptionMessage(exception, fmt, vars));
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan)
        {
            TraceApi(componentName, method, timespan, "");
        }

        public void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            TraceApi(componentName, method, timespan, string.Format(fmt, vars));
        }
        public void TraceApi(string componentName, string method, TimeSpan timespan, string properties)
        {
            string message = String.Concat("Component:", componentName, ";Method:", method, ";Timespan:", timespan.ToString(), ";Properties:", properties);
            log.Info(message);
        }

        private static string FormatExceptionMessage(Exception exception, string fmt, object[] vars)
        {
            // Simple exception formatting: for a more comprehensive version see 
            // http://code.msdn.microsoft.com/windowsazure/Fix-It-app-for-Building-cdd80df4
            var sb = new StringBuilder();
            sb.Append(string.Format(fmt, vars));
            sb.Append(" Exception: ");
            sb.Append(exception.ToString());
            return sb.ToString();
        }
    }
}