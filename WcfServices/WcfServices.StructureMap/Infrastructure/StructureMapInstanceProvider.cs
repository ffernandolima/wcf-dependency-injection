using StructureMap;
using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WcfServices.StructureMap.Infrastructure
{
	public class StructureMapInstanceProvider : IInstanceProvider
	{
		private readonly Type _serviceType;

		public StructureMapInstanceProvider(Type serviceType)
		{
			this._serviceType = serviceType;
		}

		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			var serviceInterfaces = this._serviceType.GetInterfaces();

			if (serviceInterfaces == null || !serviceInterfaces.Any())
			{
				throw new ArgumentNullException("serviceInterfaces", "Service interfaces cannot be null or empty.");
			}

			var serviceInterface = serviceInterfaces.Single(x => x.Name.Contains(this._serviceType.Name));

			var container = new Container(_ =>
			{
				_.Scan(x =>
				{
					x.AssembliesFromApplicationBaseDirectory(assembly => assembly.FullName.StartsWith(Common.Constants.AssemblyName, StringComparison.OrdinalIgnoreCase));
					x.LookForRegistries();
					x.WithDefaultConventions();
				});
			});
#if DEBUG
			Debug.WriteLine(container.WhatDidIScan());
#endif
			return container.GetInstance(serviceInterface);
		}

		public object GetInstance(InstanceContext instanceContext)
		{
			return this.GetInstance(instanceContext, null);
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			// Calls the service dispose for doesn't wait GC
			IDisposable disposableInstance;

			if ((disposableInstance = instance as IDisposable) != null)
			{
				disposableInstance.Dispose();
			}
		}
	}
}
