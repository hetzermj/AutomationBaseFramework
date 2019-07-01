using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;

namespace AppiumBase.Tests
{
	[TestFixture]
	public class BrowserStackTest
	{

		readonly static string userName = "mikehetzer3";
		readonly static string accessKey = "j1aRpzsyYJQAT1sqZh1C";

		//public static string URL = "http://hub-cloud.browserstack.com/wd/hub";
		public static string URL = "https://" + userName + ":" + accessKey + "@hub-cloud.browserstack.com/wd/hub";
		AndroidDriver<AndroidElement> _driver = null;
		//TestContext context;

		[Test]
		public void TheTest()
		{
			//ICapabilities cap = op.ToCapabilities();
			DesiredCapabilities cap = new DesiredCapabilities();

			cap.SetCapability("osVersion", "8.0");
			cap.SetCapability("device", "Samsung Galaxy S9");
			cap.SetCapability("realMobile", "true");
			cap.SetCapability("projectName", "ProjectPOC");
			cap.SetCapability("buildName", "BuildPOC");
			cap.SetCapability("sessionName", "hetzermj_SampleTest");
			cap.SetCapability("local", "true");
			//cap.SetCapability("userName", "mikehetzer3");
			//cap.SetCapability("accessKey", "j1aRpzsyYJQAT1sqZh1C");

			//cap.SetCapability("browserstack.user", userName);
			//cap.SetCapability("browserstack.key", accessKey);


			cap.SetCapability("os", "Android");
			cap.SetCapability("automationName", "Appium");
			//cap.SetCapability("device", "Samsung Galaxy S9");
			//cap.SetCapability("platformVersion", "8.0");
			cap.SetCapability("app_url", "bs://470d029c6d86842666eb1304b23ab82aa651eb0f");
			//cap.SetCapability("browserstack.appium_version", "1.13.0");
			cap.SetCapability("browserName", "");
			cap.SetCapability("realMobile", "true");


			_driver = new AndroidDriver<AndroidElement>(new Uri(URL), cap);


			//_driver.FindElement(MobileBy.AccessibilityId("visibleButtonTestCD")).Click();

			AndroidElement searchElement = (AndroidElement)new WebDriverWait(_driver, TimeSpan.FromSeconds(30)).Until(
				ExpectedConditions.ElementToBeClickable(MobileBy.AccessibilityId("visibleButtonTestCD"))
			);
			searchElement.Click();



			var el = _driver.FindElement(MobileBy.Id("io.selendroid.testapp:id/visibleTextView"));

			Console.WriteLine(el.Text);

			Assert.AreEqual("Text is sometimes displayed", el.Text);




		}

		[TearDown]
		public void TearDown()
		{
			//Console.WriteLine(TestContext.CurrentContext.Result.Outcome.Status.ToString());
			//string result = TestContext.CurrentContext.Result.Outcome.Status.ToString() == "Passed" ? "passed" : "failed";

			//IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
			//js.ExecuteScript($"sauce:job-result={result}");

			_driver.Quit();
		}






	}
}
