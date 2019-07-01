using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Appium;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;

namespace AppiumBase.Tests
{
	[TestFixture]
	public class SampleTestClass
	{

		
		

		[Test]
		public void SauceLabsTest()
		{
			string USERNAME = "hetzermj";
			string ACCESS_KEY = "5112ddb0-a042-4723-94ef-c126e590991d";
			string URL = "https://" + USERNAME + ":" + ACCESS_KEY + "@ondemand.saucelabs.com:443/wd/hub";

			AndroidDriver<RemoteWebElement> _driver = null;

			DesiredCapabilities cap = new DesiredCapabilities();

			cap.SetCapability("platformName", "Android");
			cap.SetCapability("deviceName", "Samsung Galaxy S9 HD GoogleAPI Emulator");
			cap.SetCapability("platformVersion", "8.0");
			cap.SetCapability("app", "sauce-storage:selendroid-test-app.apk");
			cap.SetCapability("browserName", "");
			cap.SetCapability("deviceOrientation", "portrait");

			_driver = new AndroidDriver<RemoteWebElement>(new Uri(URL), cap);


			_driver.FindElementByAccessibilityId("visibleButtonTestCD").Click();

			var el = _driver.FindElementById("io.selendroid.testapp:id/visibleTextView");

			Console.WriteLine(el.Text);

			Assert.AreEqual("Text is sometimes displayed", el.Text);

			Console.WriteLine(TestContext.CurrentContext.Result.Outcome.Status.ToString());
			string result = TestContext.CurrentContext.Result.Outcome.Status.ToString() == "Passed" ? "passed" : "failed";

			IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
			js.ExecuteScript($"sauce:job-result=passed");

			_driver.Quit();


		}


		[Test]
		public void TestObjectTest()
		{
			string USERNAME = "tlemmonds";
			string ACCESS_KEY = "2D8834256B8F4C9FB0F67850E050BEB4";
			string URL = "https://" + USERNAME + ":" + ACCESS_KEY + "@us1.appium.testobject.com/wd/hub";
			string url2 = "https://us1.appium.testobject.com/wd/hub";

			AndroidDriver<RemoteWebElement> _driver = null;

			DesiredCapabilities cap = new DesiredCapabilities();

			//cap.SetCapability("", "");
			cap.SetCapability("deviceName", "Samsung Galaxy S7");
			cap.SetCapability("platformName", "Android");
			cap.SetCapability("platformVersion", "8.0.0");
			cap.SetCapability("testobject_app_id", "2");
			//cap.SetCapability("app", "sauce-storage:selendroid-test-app.apk");
			cap.SetCapability("testobject_api_key", "2D8834256B8F4C9FB0F67850E050BEB4");
			cap.SetCapability("tunnelIdentifier", "93c7c5c8-fe64-406f-8f94-f018d1b71b4b");
			cap.SetCapability("browserName", "");
			cap.SetCapability("automationName", "Appium");
			cap.SetCapability("deviceOrientation", "portrait");
			cap.SetCapability("name", "My Test 1!");
			cap.SetCapability("privateDevicesOnly", "false");

			_driver = new AndroidDriver<RemoteWebElement>(new Uri(URL), cap);


			_driver.FindElementByAccessibilityId("visibleButtonTestCD").Click();

			var el = _driver.FindElementById("io.selendroid.testapp:id/visibleTextView");

			Console.WriteLine(el.Text);

			Assert.AreEqual("Text is sometimes displayed", el.Text);

			Console.WriteLine(TestContext.CurrentContext.Result.Outcome.Status.ToString());
			string result = TestContext.CurrentContext.Result.Outcome.Status.ToString() == "Passed" ? "passed" : "failed";

			//IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
			//js.ExecuteScript($"sauce:job-result={result}");

			_driver.Quit();
		}

	}
}
