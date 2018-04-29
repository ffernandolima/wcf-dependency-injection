using System;
using System.Runtime.Serialization;

namespace WcfServices.Contracts.MessageTypes.Responses
{
	[DataContract, Serializable]
	public class Response<T> : Response
	{
		[DataMember]
		public T Item { get; set; }
	}
}
