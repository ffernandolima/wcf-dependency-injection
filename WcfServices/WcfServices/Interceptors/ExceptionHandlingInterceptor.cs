using Castle.DynamicProxy;
using System;
using WcfServices.Contracts.MessageTypes.Responses;

namespace WcfServices.Interceptors
{
	public class ExceptionHandlingInterceptor : IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			try
			{
				invocation.Proceed();
			}
			catch (Exception exception)
			{
				HandleException(exception, invocation);
			}
		}

		private static void HandleException(Exception exception, IInvocation invocation)
		{
			if (invocation.Method.ReturnType == typeof(Response) || invocation.Method.ReturnType.BaseType == typeof(Response))
			{
				var response = Activator.CreateInstance(invocation.Method.ReturnType);

				dynamic ex = exception;

				Handle(ex, response as Response);

				invocation.ReturnValue = response;
			}
		}

		private static void Handle(Exception exception, Response response)
		{
			ExtractExceptionInfo(exception, response);
			response.Code = ResponseCode.Fatal;
		}

		private static void ExtractExceptionInfo(Exception exception, Response response)
		{
			response.ResponseStatus.ErrorCode = exception.HResult.ToString();
			response.ResponseStatus.Message = exception.Message;
			response.ResponseStatus.StackTrace = exception.ToString();
		}
	}
}