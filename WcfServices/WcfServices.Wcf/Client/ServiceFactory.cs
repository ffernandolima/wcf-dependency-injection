using WcfServices.Wcf.Client.Factories;
using WcfServices.Wcf.Client.Wrapper;
using WcfServices.Wcf.Utils;
using System;

namespace WcfServices.Wcf.Client
{
	public static class ServiceFactory
	{
		static readonly Type BASE_CLIENT_TYPE = typeof(IBaseClient);

		public static ServiceWrapper CreateDynamic(Type clientType, Bindings binding = Bindings.None)
		{
			var srctype = typeof(ServiceFactory);
			var method = srctype.GetMethod(
							StringExtensions.Of(
								() => Create<object>(binding)
							)
						);

			var generic = method.MakeGenericMethod(clientType);
			var result = generic.Invoke(null, new object[] { binding });

			return (ServiceWrapper)result;
		}

		public static ServiceWrapper<T> Create<T>(Bindings binding = Bindings.None)
		{
			var factory = ChooseFactory<T>();
			var channel = factory.CreateService(binding);
			var wrapper = new ServiceWrapper<T>(channel);
			return wrapper;
		}

		private static AbsFactory<T> ChooseFactory<T>()
		{
			var clientType = typeof(T);

			if (clientType.IsInterface)
			{
				return new ServiceChannelFactory<T>();
			}

			if (BASE_CLIENT_TYPE.IsAssignableFrom(clientType))
			{
				return new ServiceClientFactory<T>();
			}

			throw new InvalidOperationException("Unable to detect client factory.");
		}
	}
}
