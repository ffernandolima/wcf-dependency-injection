using Castle.DynamicProxy;
using StructureMap;
using WcfServices.Contracts;
using WcfServices.Interceptors;

namespace WcfServices.DependencyResolution
{
	public class WcfServicesRegistry : Registry
	{
		private static readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

		public WcfServicesRegistry()
		{
			this.For<IWcfService>()
				.Use<WcfService>()
				.DecorateWith(x => proxyGenerator.CreateInterfaceProxyWithTarget(x, new IInterceptor[] { new ExceptionHandlingInterceptor(), new ExceptionLoggingInterceptor() }));
		}
	}
}