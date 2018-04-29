using Castle.DynamicProxy;
using System;
using WcfServices.Logging;

namespace WcfServices.Interceptors
{
	public class ExceptionLoggingInterceptor : IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			try
			{
				invocation.Proceed();
			}
			catch (Exception exception)
			{
				LogException(exception);
				throw;
			}
		}

		private static void LogException(Exception exception)
		{
			try
			{
				Log4NetHelper.All(logger => logger.Error(exception.ToString()));
			}
			catch
			{
				// ignored
			}
		}
	}
}