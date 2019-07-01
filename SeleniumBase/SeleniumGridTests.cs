using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using NUnit.Framework;

namespace SeleniumBase
{
	[TestFixture]	
	public class SeleniumGridTests
	{
		ChromeOptions Options = null;

		private string hubURL = "http://ip:port/wd/hub";

		[SetUp]
		public void Setup()
		{
			ChromeOptions Options = new ChromeOptions();
			Options.AddArgument("--no-sandbox"); // Bypass OS security model, MUST BE THE VERY FIRST OPTION			
			Options.AddArgument("start-maximized"); // open Browser in maximized mode
			Options.AddArgument("disable-infobars"); // disabling infobars
			Options.AddArgument("--disable-extensions"); // disabling extensions
			Options.AddArgument("--disable-gpu"); // applicable to windows os only
			Options.AddArgument("--disable-dev-shm-usage"); // overcome limited resource problems
			this.Options = Options;
		}



		[Test, Parallelizable]
		public void TestMethod1()
		{
			RemoteWebDriver driver = new RemoteWebDriver(new Uri(hubURL), Options);			
			driver.Navigate().GoToUrl("http://www.google.com");
			Thread.Sleep(1000);
			driver.Quit();
		}

		[Test, Parallelizable]
		public void TestMethod2()
		{
			RemoteWebDriver driver = new RemoteWebDriver(new Uri(hubURL), Options);
			driver.Navigate().GoToUrl("http://www.google.com");
			Thread.Sleep(1000);
			driver.Quit();
		}

		[Test, Parallelizable]
		public void TestMethod3()
		{
			RemoteWebDriver driver = new RemoteWebDriver(new Uri(hubURL), Options);
			driver.Navigate().GoToUrl("http://www.google.com");
			Thread.Sleep(1000);
			driver.Quit();
		}

		[Test, Parallelizable]
		public void TestMethod4()
		{
			RemoteWebDriver driver = new RemoteWebDriver(new Uri(hubURL), Options);
			driver.Navigate().GoToUrl("http://www.google.com");
			Thread.Sleep(1000);
			driver.Quit();
		}

	}
}
