using System;
using NUnit.Framework;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.MarkupUtils;

namespace AutomationBase.Tests
{
	[TestFixture]
	public class ExtentReportTestSample
	{
		private ExtentReports ExtentReport = null;
		private ExtentHtmlReporter HtmlReport = null;
		private ExtentTest xtParentTest = null;

		[OneTimeSetUp]
		public void BeforeEachTest()
		{
			ExtentReport = new ExtentReports();
			HtmlReport = new ExtentHtmlReporter(@"C:\Automation_Data\Sample File Name.html");			

			//HtmlReport.Config.DocumentTitle = "Sample Document Title";
			//HtmlReport.Config.Encoding = Protocol.HTTPS;
			//HtmlReport.Configuration().Theme = Theme.Dark;
			//HtmlReport.Configuration().ReportName = "Sample Report Name";

			ExtentReport.AttachReporter(HtmlReport);

			xtParentTest = this.ExtentReport.CreateTest("Parent Node");
		}

		[Test]
		public void ReportGeneration()
		{
			ExtentTest xtTest = ExtentReport.CreateTest("Sample Test 2");

			// Pass Step
			xtTest.Pass("Pass Step details.");

			// Fail Step
			xtTest.Fail("Fail Step details.");

			// Step with Screenshot
			xtTest.Info("Screenshot from XX thing.", MediaEntityBuilder.CreateScreenCaptureFromPath(@"C:\Automation_Data\potatoes.jpg").Build());

			// Step with Label markup
			IMarkup m = MarkupHelper.CreateLabel("Label Text", ExtentColor.Pink);
			xtTest.Info(m);

			// Step with Code Block
			String code = "\n\t\n\t\tText\n\t\n";
			IMarkup m2 = MarkupHelper.CreateCodeBlock(code);
			xtTest.Info(m2);

			// Step with Table data inserted
			string[][] data = new string[][]
			{
				new string[] { "Header1", "Header2", "Header3" },
				new string[]{ "Content.1.1", "Content.2.1", "Content.3.1" },
				new string[]{ "Content.1.2", "Content.2.2", "Content.3.2" },
				new string[]{ "Content.1.3", "Content.2.3", "Content.3.3" },
				new string[]{ "Content.1.4", "Content.2.4", "Content.3.4" }
			};
			IMarkup m3 = MarkupHelper.CreateTable(data);
			xtTest.Info(m3);

			// Step that inserts a link to a file into the html
			xtTest.Log(Status.Info, string.Format("<a href='{0}{1}'>Knight</a>", @"file:///", @"C:\Automation_Data\Knight.txt"));
		}

		[Test]
		public void ChildTest1()
		{
			ExtentTest child = xtParentTest.CreateNode("Child 1 Test Title");
			child.Pass("Child 1 Test step passed.");
		}
		[Test]
		public void ChildTest2()
		{
			ExtentTest child = xtParentTest.CreateNode("Child 2 Test Title");
			child.Pass("Child 2 Test step passed.");
		}


		[OneTimeTearDown]
		public void AfterEachTest()
		{
			ExtentReport.Flush();
			ExtentReport = null;
			HtmlReport = null;
		}

	}
}
