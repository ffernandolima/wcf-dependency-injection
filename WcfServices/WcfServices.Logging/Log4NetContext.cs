using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WcfServices.Logging
{
	public class Log4NetContext : IDisposable
	{
		private IDictionary<string, object> _threadContextProperties;

		internal Log4NetContext(IDictionary<string, object> threadContextProperties)
		{
			this._threadContextProperties = threadContextProperties;

			this.SetThreadContextProperties();
		}

		private void SetThreadContextProperties()
		{
			if (this._threadContextProperties == null || !this._threadContextProperties.Any())
			{
				return;
			}

			foreach (var property in this._threadContextProperties)
			{
				ThreadContext.Properties[property.Key] = property.Value;
			}
		}

		private void ResetThreadContextProperties()
		{
			if (this._threadContextProperties == null || !this._threadContextProperties.Any())
			{
				return;
			}

			foreach (var key in this._threadContextProperties.Keys)
			{
				ThreadContext.Properties[key] = null;
			}
		}

		#region IDisposable Members

		private bool _disposed;

		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{
					this.ResetThreadContextProperties();

					if (this._threadContextProperties != null)
					{
						this._threadContextProperties.Clear();
						this._threadContextProperties = null;
					}
				}
			}

			this._disposed = true;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion IDisposable Members
	}
}
