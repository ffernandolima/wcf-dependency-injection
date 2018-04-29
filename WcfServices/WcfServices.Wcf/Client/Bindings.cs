using System.ComponentModel;

namespace WcfServices.Wcf.Client
{
	public enum Bindings : byte
	{
		[Description("None")]
		None = 0,

		[Description("netNamedPipeBinding")]
		netNamedPipeBinding = 1,

		[Description("netTcpBinding")]
		netTcpBinding = 2,

		[Description("basicHttpBinding")]
		basicHttpBinding = 3
	}
}
