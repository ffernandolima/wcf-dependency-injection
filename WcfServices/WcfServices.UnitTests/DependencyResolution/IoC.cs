using StructureMap;
using System;
using System.Diagnostics;
using WcfServices.Contracts;

namespace WcfServices.UnitTests.DependencyResolution
{
	public class IoC
	{
		#region Private Fields

		private static readonly Lazy<IoC> _instance = new Lazy<IoC>(valueFactory: () => new IoC(), isThreadSafe: true);
		private bool _startupComplete;
		private readonly object _startupLocker;
		private IContainer _container;

		#endregion Private Fields

		#region Public Properties

		public IContainer Container
		{
			get
			{
				if (this._container == null)
				{
					this.PerformStartup();
				}

				return this._container;
			}
		}

		public static IoC Instance { get { return _instance.Value; } }

		#endregion Public Properties

		#region Ctor

		private IoC()
		{
			this._startupComplete = false;
			this._startupLocker = new object();
			this._container = null;
		}

		#endregion Ctor

		#region Public Methods

		public void PerformStartup()
		{
			if (!this._startupComplete)
			{
				lock (this._startupLocker)
				{
					if (!this._startupComplete)
					{
						this.InitializeContainer();

						this._startupComplete = true;
					}
				}
			}
		}

		#endregion Public Methods

		#region Private Methods

		private void InitializeContainer()
		{
			if (this._container == null)
			{
				this._container = new Container(_ =>
				{
					_.For<IWcfService>().Use(ServiceProxyFactory.Create<IWcfService>()).AlwaysUnique();

					_.Scan(x =>
					{
						x.AssembliesFromApplicationBaseDirectory(assembly => assembly.FullName.StartsWith(Common.Constants.AssemblyName, StringComparison.OrdinalIgnoreCase));
						x.LookForRegistries();
						x.WithDefaultConventions();
					});
				});
#if DEBUG
				Debug.WriteLine(this._container.WhatDidIScan());
#endif
			}
		}

		#endregion Private Methods
	}
}
