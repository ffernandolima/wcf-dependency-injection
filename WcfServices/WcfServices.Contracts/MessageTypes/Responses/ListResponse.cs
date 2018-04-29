using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfServices.Contracts.MessageTypes.Responses
{
	[DataContract, Serializable]
	public class ListResponse<T> : Response
	{
		[DataMember]
		public IList<T> Records { get; set; }

		[DataMember]
		public int RecordCount { get; set; }
	}
}
