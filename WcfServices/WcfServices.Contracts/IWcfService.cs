using System;
using System.ServiceModel;
using WcfServices.Contracts.MessageTypes.Responses;

namespace WcfServices.Contracts
{
	[ServiceContract]
	public interface IWcfService : IDisposable
	{
		[OperationContract]
		Response<string> Test();

		[OperationContract]
		Response<string> Log();

		[OperationContract]
		Response Exception();
	}
}
