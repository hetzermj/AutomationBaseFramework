using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumBase.PageObjects
{
	public static class ByExtensions
	{
		public static IWebElement xtGetWebElement(this By by, IWebDriver driver, int timeoutInSeconds = 30)
		{
			by.xtWaitUntilVisible(driver, timeoutInSeconds);
			return driver.FindElement(by);
		}

		public static bool xtIsElementPresent(this By by, IWebDriver driver, int timeOutInSeconds = 5)
		{
			bool results = false;
			try
			{
				driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeOutInSeconds);
				results = driver.FindElement(by).Displayed;
				return results;
			}
			catch (NoSuchElementException ex)
			{
				return false;
			}
			finally
			{
				driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(BasePage.IMPLICITWAITTIMEOUT);
			}
		}

		public static SelectElement xtGetSelectElement(this By by, IWebDriver driver)
		{
			return new SelectElement(by.xtGetWebElement(driver));
		}

		public static bool xtWaitUntilExists(this By by, IWebDriver driver, int timeoutInSeconds = 30)
		{
			try
			{
				var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public static void xtWaitForTextToBePresentInElementValue(this By by, IWebDriver driver, string text, int timeoutInSeconds = 30)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementValue(by, text));
		}

		public static void xtWaitUntilClickable(this By by, IWebDriver driver, int timeoutInSeconds = 30)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
		}

		public static void xtWaitUntilVisible(this By by, IWebDriver driver, int timeoutInSeconds = 30)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
		}

		public static void xtWaitForSelectionStateToBe(this By by, IWebDriver driver, bool selected, int timeoutInSeconds = 30)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementSelectionStateToBe(by, selected));
		}

		public static void xtWaitUntilInvisible(this By by, IWebDriver driver, int timeoutInSeconds = 30)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
		}

		public static void xtWaitForText(this By by, IWebDriver driver, string text, int timeoutInSeconds = 30)
		{
			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElementLocated(by, text));
		}
	}
}
