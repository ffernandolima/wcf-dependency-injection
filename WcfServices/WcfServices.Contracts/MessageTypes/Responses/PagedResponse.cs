using System;
using System.Runtime.Serialization;

namespace WcfServices.Contracts.MessageTypes.Responses
{
	[DataContract, Serializable]
	public class PagedResponse<T> : ListResponse<T>
	{
		[DataMember]
		public int PageIndex { get; set; }
	}
}
