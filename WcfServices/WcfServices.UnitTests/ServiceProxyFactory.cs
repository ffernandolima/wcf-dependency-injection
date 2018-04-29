using Castle.DynamicProxy;
using WcfServices.UnitTests.Interceptors;

namespace WcfServices.UnitTests
{
	public static class ServiceProxyFactory
	{
		private static readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

		public static TService Create<TService>() where TService : class
		{
			var proxy = proxyGenerator.CreateInterfaceProxyWithoutTarget<TService>(new RequestInterceptor<TService>());

			return proxy;
		}
	}
}
