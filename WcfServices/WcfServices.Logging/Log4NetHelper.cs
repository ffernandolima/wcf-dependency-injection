using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WcfServices.Logging
{
	public static class Log4NetHelper
	{
		private static string _loggerPrefix;

		public static string LoggerPrefix
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_loggerPrefix))
				{
					_loggerPrefix = DefaultPrefix();
				}

				return _loggerPrefix;
			}
			set
			{
				_loggerPrefix = value;
			}
		}

		public static ILog RollingFileLogger
		{
			get { return LogManager.GetLogger(LoggerPrefix + ".RollingFileLogger"); }
		}

		public static void All(Action<ILog> logAction)
		{
			var loggers = new ILog[] { RollingFileLogger };

			var parallelLoggers = loggers.Where(logger => logger != null).AsParallel();

			parallelLoggers.ForAll(logAction);
		}

		public static Log4NetContext Log4NetContext(IDictionary<string, object> threadContextProperties)
		{
			var log4netContext = new Log4NetContext(threadContextProperties);

			return log4netContext;
		}

		private static string DefaultPrefix()
		{
			var appName = AppDomain.CurrentDomain.FriendlyName;

			var domainName = Path.GetFileNameWithoutExtension(appName);
#if DEBUG
			domainName = domainName.Replace(".vshost", string.Empty);
#endif
			return domainName;
		}
	}
}
