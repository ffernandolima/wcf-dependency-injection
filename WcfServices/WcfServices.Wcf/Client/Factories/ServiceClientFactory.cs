using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfServices.Wcf.Client.Factories
{
	internal class ServiceClientFactory<TClient> : AbsFactory<TClient>
	{
		protected override string ExtractTypeName()
		{
			return this.Client.Name;
		}

		protected override TClient InstantiateService(Binding binding, EndpointAddress address)
		{
			Debug.Assert(this.Client is IBaseClient);

			return (TClient)Activator.CreateInstance(this.Client, binding, address);
		}
	}
}