using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationBase.Tests
{
	// One time setup and teardown for ALL test fixtures under in the AutomationBase.Tests namespace
	[SetUpFixture]
	public class BaseSetupFixture
	{

		[OneTimeSetUp]
		public void RunBeforeAnyTests()
		{
			Console.WriteLine("Set up for entire fixture");
			
		}

		[OneTimeTearDown]
		public void RunAfterAnyTests()
		{
			
		}

	}
}
