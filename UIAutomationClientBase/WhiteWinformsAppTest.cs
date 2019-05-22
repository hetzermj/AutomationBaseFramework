using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomationClientBase.Elements;
using NUnit.Framework;
using System.Threading;

namespace UIAutomationClientBase
{
	[TestFixture]
	public class WhiteWinformsAppTest
	{

		[Test]
		public void BeginnerTest()
		{
			Application app = new Application(DesktopApp.WhiteWinFormsTestApp);

			app.LaunchApplication();


			SearchCondition mainWindow = new SearchCondition(By.Name, "MainWindow", LocalizedControlType.Window);
			SearchCondition btn_GetMultiple = new SearchCondition(By.Name, "Get Multiple", LocalizedControlType.Button);

			Element root = new Element(new UIAutomationClient.CUIAutomationClass().GetRootElement());
			WindowElement window = root.GetControl(UIAutomationClient.TreeScope.TreeScope_Children, mainWindow).WindowElement();

			ControlElement button = window.GetControl(UIAutomationClient.TreeScope.TreeScope_Descendants, btn_GetMultiple);
			button.Click();

			Thread.Sleep(3000);

			DesktopApp.WhiteWinFormsTestApp.KillAllInstancesOfProcess();

		}



	}
}
