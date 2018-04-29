using System;
using WcfServices.Contracts;
using WcfServices.Contracts.MessageTypes.Responses;
using WcfServices.Logging;

namespace WcfServices
{
	public class WcfService : IWcfService
	{
		public Response<string> Test()
		{
			const string MSG = "WcfService::Test()";

			return new Response<string>
			{
				Item = MSG
			};
		}

		public Response<string> Log()
		{
			const string MSG = "WcfService.Log";

			Log4NetHelper.All(x => x.Fatal(MSG));

			return new Response<string>
			{
				Item = MSG
			};
		}

		public Response Exception()
		{
			throw new Exception();
		}

		#region IDisposable Members

		private bool _disposed;

		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				if (disposing)
				{

				}
			}

			this._disposed = true;
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}
