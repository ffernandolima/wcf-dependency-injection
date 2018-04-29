using System;
using System.Runtime.Serialization;

namespace WcfServices.Contracts.MessageTypes.Requests
{
	[DataContract, Serializable]
	public class PagedRequest<T> : Request<T>
	{
		[DataMember]
		public int PageIndex { get; set; }

		[DataMember]
		public int PageSize { get; set; }
	}
}
