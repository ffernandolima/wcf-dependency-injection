using log4net.Config;
using System.Globalization;
using System.Threading;
using WcfServices.DependencyResolution;
using WcfServices.Logging;

namespace WcfServices.App_Code
{
	public static class Application
	{
		/// <summary>
		/// The Global.asax doesn't work for non-HTTP protocols that is supported by the Windows Activation Service (WAS)
		/// This method always is invoked when the AppDomain is started
		/// Don't change the name and signature
		/// </summary>
		/// <remarks>
		/// src: http://blogs.msdn.com/b/wenlong/archive/2006/01/11/511514.aspx
		/// </remarks>
		public static void AppInitialize()
		{
			// structuremap
			IoC.Instance.PerformStartup();

			// Log4net
			Log4NetHelper.LoggerPrefix = Common.Constants.AssemblyName;

			// log4net
			XmlConfigurator.Configure();

			// Invariantculture
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		}
	}
}