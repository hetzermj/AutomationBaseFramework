using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace SeleniumBase.PageObjects
{
	public class BasePage
	{
		public BasePage(IWebDriver driver)
		{
			this._driver = driver;
		}

		public const int IMPLICITWAITTIMEOUT = 20;

		protected IWebDriver _driver { get; set; }

		public void StopPageLoad()
		{
			IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
			js.ExecuteScript("return window.stop");
		}

		public IWebElement ExpandRootElement(IWebElement element)
		{
			IWebElement ele = (IWebElement)((IJavaScriptExecutor)_driver)
				.ExecuteScript("return arguments[0].shadowRoot", element);
			return ele;
		}

		public IWebElement GetFirstChild(IWebElement element)
		{
			IWebElement ele = (IWebElement)((IJavaScriptExecutor)_driver)
				.ExecuteScript("return arguments[0].firstChild", element);
			return ele;
		}


		private void ExecuteJavascript(string javascript)
		{
			IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
			js.ExecuteScript(javascript);
		}


		public void SetAttributeOfElement(IWebElement el, string attribute, string input)
		{
			IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
			string script = "document.getElementById(\"" + el.GetAttribute("id") + "\").setAttribute(\"" + attribute + "\", \"" + input + "\");";
			executor.ExecuteScript(script);
		}

		public void ClickCheckBox(bool desiredState, By chk_Identifier)
		{
			IWebElement cb = this._driver.FindElement(chk_Identifier);
			if (desiredState != cb.Selected)
				cb.Click();
		}

		public void WaitForPageLoadToComplete(int intTimeOutFromSeconds = 120)
		{
			var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(intTimeOutFromSeconds));
			wait.Until(d => ((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState").Equals("complete"));
		}

		public void WaitForAjax()
		{
			IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
			if ((Boolean)executor.ExecuteScript("return window.jQuery != undefined"))
			{
				while (!(Boolean)executor.ExecuteScript("return jQuery.active == 0"))
				{
					Thread.Sleep(100);
				}
			}
		}
	}
}
