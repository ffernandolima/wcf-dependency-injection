using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using WcfServices.Logging;

namespace WcfServices.Infrastructure
{
	public class ErrorHandler : IErrorHandler
	{
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
		}

		public bool HandleError(Exception error)
		{
			try
			{
				Log4NetHelper.All(logger => logger.Error(error.ToString()));
			}
			catch
			{
				// ignored
			}

			return false;
		}
	}
}