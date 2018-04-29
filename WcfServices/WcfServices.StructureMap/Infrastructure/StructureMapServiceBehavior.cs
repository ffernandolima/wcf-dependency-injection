using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WcfServices.StructureMap.Infrastructure
{
	public class StructureMapServiceBehavior : IServiceBehavior
	{
		public void ApplyDispatchBehavior(ServiceDescription desc, ServiceHostBase host)
		{
			foreach (var channelDispatcherBase in host.ChannelDispatchers)
			{
				if (channelDispatcherBase is ChannelDispatcher channelDispatcher)
				{
					foreach (var endpointDispatcher in channelDispatcher.Endpoints)
					{
						endpointDispatcher.DispatchRuntime.InstanceProvider = new StructureMapInstanceProvider(desc.ServiceType);
					}
				}
			}
		}

		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
		}

		public void Validate(ServiceDescription desc, ServiceHostBase host)
		{
		}
	}
}
