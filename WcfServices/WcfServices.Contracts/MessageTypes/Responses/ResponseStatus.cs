﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfServices.Contracts.MessageTypes.Responses
{
	[DataContract]
	public class ResponseStatus
	{
		public ResponseStatus()
		{
		}

		public ResponseStatus(string errorCode)
		{
			this.ErrorCode = errorCode;
		}

		public ResponseStatus(string errorCode, string message)
			: this(errorCode)
		{
			this.Message = message;
		}

		[DataMember]
		public string ErrorCode { get; set; }

		[DataMember]
		public IList<ResponseError> Errors { get; set; }

		[DataMember]
		public string Message { get; set; }

		[DataMember]
		public string StackTrace { get; set; }

		public void AddError(string fieldName, string message)
		{
			if (this.Errors == null)
			{
				this.Errors = new List<ResponseError>();
			}

			var responseError = new ResponseError
			{
				Message = message,
				FieldName = fieldName
			};

			this.Errors.Add(responseError);
		}
	}
}
