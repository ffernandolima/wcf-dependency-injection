using WcfServices.Wcf.Utils;
using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace WcfServices.Wcf.Client.Factories
{
	internal static class ServiceResolver
	{
		internal static ChannelEndpointElement Endpoint(string contractOrName, Bindings bindingType)
		{
			var clientSection = ConfigurationManager.GetSection("system.serviceModel/client") as ClientSection;

			if (clientSection == null)
			{
				throw new InvalidOperationException("Couldn't find clients at system.serviceModel configuration section.");
			}

			var bindingName = bindingType.GetDescriptionFromValue();

			var endpoint = clientSection.Endpoints
										.Cast<ChannelEndpointElement>()
										.FirstOrDefault(x =>
											(x.Contract == contractOrName || x.Name.StartsWith(contractOrName, StringComparison.OrdinalIgnoreCase))
											&& x.Binding.Equals(bindingName, StringComparison.OrdinalIgnoreCase)
										);
			return endpoint;
		}

		internal static Binding Binding(string name)
		{
			var bindingSection = ConfigurationManager.GetSection("system.serviceModel/bindings") as BindingsSection;

			if (bindingSection == null)
			{
				throw new InvalidOperationException("Couldn't find bindings at system.serviceModel configuration section.");
			}

			var match = bindingSection.BindingCollections
									  .Select(x => new
									  {
										  x.BindingType,
										  BindingElement = x.ConfiguredBindings.FirstOrDefault(y => y.Name == name)
									  })
									  .FirstOrDefault(x =>
										  x.BindingElement != null
									  );

			if (match != null)
			{
				var binding = (Binding)Activator.CreateInstance(match.BindingType);
				match.BindingElement.ApplyConfiguration(binding);
				binding.Name = match.BindingElement.Name;

				return binding;
			}

			return null;
		}
	}
}
