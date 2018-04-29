using WcfServices.Wcf.Utils;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace WcfServices.Wcf.Client.Factories
{
	internal abstract class AbsFactory<TClient>
	{
		protected abstract string ExtractTypeName();
		protected abstract TClient InstantiateService(Binding binding, EndpointAddress address);

		protected Type Client { get; private set; }

		public AbsFactory()
		{
			this.Client = typeof(TClient);
		}

		protected TClient Create(Bindings bindingType)
		{
			var contract = this.ExtractTypeName();

			var endpoint = ServiceResolver.Endpoint(contract, bindingType);
			if (endpoint == null)
			{
				return default(TClient);
			}

			var binding = ServiceResolver.Binding(endpoint.BindingConfiguration);
			if (binding == null)
			{
				return default(TClient);
			}

			var address = new EndpointAddress(endpoint.Address);

			var context = this.InstantiateService(binding, address);

			return context;
		}

		public TClient CreateService(Bindings bindings)
		{
			var context = default(TClient);
			if (bindings == Bindings.None)
			{
				for (var i = 1; context == null && i < EnumExtensions.Count<Bindings>(); i++)
				{
					context = this.Create((Bindings)i);
				}
			}
			else
			{
				context = this.Create(bindings);
			}

			if (context == null)
			{
				var contractName = this.Client.FullName;
				throw new EndpointNotFoundException(string.Format("Endpoint with the contract {0} was not found in the config file.", contractName));
			}

			return context;
		}
	}
}
