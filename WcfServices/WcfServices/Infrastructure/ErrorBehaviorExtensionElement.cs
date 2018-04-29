using System;
using System.ServiceModel.Configuration;

namespace WcfServices.Infrastructure
{
	public class ErrorBehaviorExtensionElement : BehaviorExtensionElement
	{
		public override Type BehaviorType
		{
			get
			{
				return typeof(ErrorServiceBehavior);
			}
		}

		protected override object CreateBehavior()
		{
			return new ErrorServiceBehavior();
		}
	}
}