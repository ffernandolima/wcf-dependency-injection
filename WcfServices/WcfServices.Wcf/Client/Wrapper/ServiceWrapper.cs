using System;

namespace WcfServices.Wcf.Client.Wrapper
{
	/// <remarks>
	/// src: http://stackoverflow.com/q/573872/251676
	/// </remarks>
	public class ServiceWrapper<TInterface> : ServiceWrapper, IDisposable
	{
		public TInterface Client { get; protected set; }

		public ServiceWrapper(TInterface channel)
			: base(channel)
		{
			this.Client = channel;
		}

		public static implicit operator TInterface(ServiceWrapper<TInterface> wrapper)
		{
			return wrapper.Client;
		}
	}
}
