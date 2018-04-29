using System;
using System.Linq;
using System.Runtime.Serialization;

namespace WcfServices.Contracts.MessageTypes.Responses
{
	[DataContract, Serializable]
	public class Response
	{
		public static readonly Response Success = new Response();

		public Response()
		{
			this.ResponseStatus = new ResponseStatus();
		}

		[DataMember]
		public ResponseStatus ResponseStatus { get; set; }

		[DataMember]
		public ResponseCode Code { get; set; }

		public bool HasErrors()
		{
			switch (this.Code)
			{
				case ResponseCode.Success:
					return false;

				case ResponseCode.Error:
				case ResponseCode.Fatal:
					return true;

				case ResponseCode.Undefined:
				default:
					return !string.IsNullOrEmpty(this.ResponseStatus.ErrorCode) ||
						   !string.IsNullOrEmpty(this.ResponseStatus.Message) ||
						   !string.IsNullOrEmpty(this.ResponseStatus.StackTrace) ||
						   (this.ResponseStatus.Errors != null && this.ResponseStatus.Errors.Any());
			}
		}
	}
}
