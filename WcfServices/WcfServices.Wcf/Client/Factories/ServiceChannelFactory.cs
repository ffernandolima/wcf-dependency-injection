using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfServices.Wcf.Client.Factories
{
	internal class ServiceChannelFactory<TClient> : AbsFactory<TClient>
	{
		protected override string ExtractTypeName()
		{
			return this.Client.FullName;
		}

		protected override TClient InstantiateService(Binding binding, EndpointAddress address)
		{
			return ChannelFactory<TClient>.CreateChannel(binding, address);
		}
	}
}
