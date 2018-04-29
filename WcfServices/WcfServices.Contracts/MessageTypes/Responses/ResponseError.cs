using System.Runtime.Serialization;

namespace WcfServices.Contracts.MessageTypes.Responses
{
	[DataContract]
	public class ResponseError
	{
		[DataMember]
		public string ErrorCode { get; set; }

		[DataMember]
		public string FieldName { get; set; }

		[DataMember]
		public string Message { get; set; }
	}
}
