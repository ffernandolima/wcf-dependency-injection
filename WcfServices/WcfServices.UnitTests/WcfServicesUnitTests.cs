using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Threading;
using WcfServices.Contracts;
using WcfServices.Contracts.MessageTypes.Responses;
using WcfServices.Logging;
using WcfServices.UnitTests.DependencyResolution;

namespace WcfServices.UnitTests
{
	[TestClass]
	public class WcfServicesUnitTests
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
			// structuremap
			IoC.Instance.PerformStartup();

			// Log4net
			Log4NetHelper.LoggerPrefix = Common.Constants.AssemblyName + ".UnitTests";

			// log4net
			XmlConfigurator.Configure();

			// Invariantculture
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
		}

		[TestMethod]
		public void InvokeWcfServices()
		{
			using (var service = IoC.Instance.Container.GetInstance<IWcfService>())
			{
				var responseTest = service.Test();
				var responseLog = service.Log();
				var responseException = service.Exception();

				Assert.AreEqual("WcfService::Test()", responseTest.Item);
				Assert.AreEqual("WcfService.Log", responseLog.Item);
				Assert.AreEqual(ResponseCode.Fatal, responseException.Code);
			}
		}
	}
}

