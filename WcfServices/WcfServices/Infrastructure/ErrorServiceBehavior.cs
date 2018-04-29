using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WcfServices.Infrastructure
{
	public class ErrorServiceBehavior : IServiceBehavior
	{
		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			var errorHandler = new ErrorHandler();

			foreach (ChannelDispatcher dispatcher in serviceHostBase.ChannelDispatchers)
			{
				dispatcher.ErrorHandlers.Add(errorHandler);
			}
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}
	}
}