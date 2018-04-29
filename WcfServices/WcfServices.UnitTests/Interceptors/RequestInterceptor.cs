using Castle.DynamicProxy;
using System;
using WcfServices.Wcf.Client;

namespace WcfServices.UnitTests.Interceptors
{
	/// <remarks>
	/// src: http://brandonzeider.me/2011/microsoft-net/building-a-reusable-service-client/
	/// </remarks>
	public class RequestInterceptor<TService> : IInterceptor where TService : class
	{
		public void Intercept(IInvocation invocation)
		{
			try
			{
				using (var serviceWrapper = ServiceFactory.Create<TService>())
				{
					invocation.ReturnValue = invocation.Method.Invoke(serviceWrapper.Client, invocation.Arguments);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
