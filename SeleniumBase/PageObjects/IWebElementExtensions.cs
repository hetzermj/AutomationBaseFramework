using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace SeleniumBase.PageObjects
{
	public static class IWebElementExtensions
	{
		public static Dictionary<string, object> GetAllAttributes(this IWebElement element, IWebDriver driver)
		{
			SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element);
			SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(element, "");

			string js =
				"var items = {};" +
				"for (i = 0; i < arguments[0].attributes.length; ++i) {" +
				"   if (arguments[0].attributes[i].value != undefined) {" +
				"       items[arguments[0].attributes[i].name] = arguments[0].attributes[i].value;" +
				"   }" +
				"}" +
				"return items;";

			Dictionary<string, object> attributes = null;

			try
			{
				attributes = (Dictionary<string, object>)((IJavaScriptExecutor)driver).ExecuteScript(js, element);
			}
			catch (Exception)
			{
			}

			return attributes;
		}

		public static void xtWaitUntilStale(this IWebElement element, IWebDriver driver, int timeoutInSeconds = 30)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
		}

		public static void xtWaitForTextToBePresent(this IWebElement element, IWebDriver driver, string str, int timeoutInSeconds = 30)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(element, str));
		}
	}
}
