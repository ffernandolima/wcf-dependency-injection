using System;
using System.ServiceModel;

namespace WcfServices.Wcf.Client.Wrapper
{
	public abstract class ServiceWrapper : IDisposable
	{
		public object Service { get; protected set; }

		protected ServiceWrapper(object service)
		{
			this.Service = service ?? throw new ArgumentNullException("service", "Service cannot be null.");
		}

		public void Dispose()
		{
			var channel = this.Service as ICommunicationObject;

			if (channel == null)
			{
				return;
			}

			var isFaulted = (channel.State == CommunicationState.Faulted);
			var isClosed = (channel.State == CommunicationState.Closed);

			try
			{
				if (!isFaulted && !isClosed)
				{
					channel.Close();
				}
			}
			// catch (ChannelTerminatedException)
			// {
			//     // The following is typically thrown on the client when a channel is terminated due to the server closing the connection.
			// }
			// catch (EndpointNotFoundException)
			// {
			//     // The following is thrown when a remote endpoint could not be found or reached. The endpoint may not be found or 
			//     // reachable because the remote endpoint is down, the remote endpoint is unreachable, or because the remote network is unreachable.
			// }
			// catch (ServerTooBusyException)
			// {
			//     // The following exception that is thrown when a server is too busy to accept a message.
			// }
			// catch (TimeoutException) 
			// {
			//     
			// }
			// catch (CommunicationException) 
			// {
			//     
			// }
			finally
			{
				if (isFaulted)
				{
					channel.Abort();
				}

				this.Service = null;
			}
		}
	}
}
