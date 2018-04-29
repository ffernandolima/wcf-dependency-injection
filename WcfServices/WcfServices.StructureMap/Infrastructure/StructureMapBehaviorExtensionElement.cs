using System;
using System.ServiceModel.Configuration;

namespace WcfServices.StructureMap.Infrastructure
{
	public class StructureMapBehaviorExtensionElement : BehaviorExtensionElement
	{
		public override Type BehaviorType
		{
			get
			{
				return typeof(StructureMapServiceBehavior);
			}
		}

		protected override object CreateBehavior()
		{
			return new StructureMapServiceBehavior();
		}
	}
}
