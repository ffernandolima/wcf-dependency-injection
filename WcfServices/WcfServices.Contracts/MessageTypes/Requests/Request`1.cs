using System;
using System.Runtime.Serialization;

namespace WcfServices.Contracts.MessageTypes.Requests
{
	[DataContract, Serializable]
	public class Request<T> : Request
	{
		[DataMember]
		public T Item { get; set; }
	}
}
