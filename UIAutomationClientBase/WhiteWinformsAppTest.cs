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

			Thread.Sleep(5000);

			app.KillApplication();

		}



	}
}
