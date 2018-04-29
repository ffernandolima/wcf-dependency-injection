using System;
using System.ComponentModel;
using System.Linq;

namespace WcfServices.Wcf.Utils
{
	internal static class EnumExtensions
	{
		public static string GetDescriptionFromValue<TEnum>(this TEnum tenum) where TEnum : struct
		{
			var type = typeof(TEnum);

			if (type.IsEnum)
			{
				var memberInfos = type.GetMember(tenum.ToString());

				if (memberInfos != null && memberInfos.Length > 0)
				{
					var attributes = memberInfos.First().GetCustomAttributes(typeof(DescriptionAttribute), false);

					if (attributes != null && attributes.Length > 0)
					{
						return ((DescriptionAttribute)attributes.First()).Description;
					}
				}
			}

			return tenum.ToString();
		}

		public static int Count<TEnum>() where TEnum : struct
		{
			return Enum.GetNames(typeof(TEnum)).Length;
		}
	}
}
