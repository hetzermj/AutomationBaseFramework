using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AutomationBase
{
	[TestFixture]
	public class SampleTestClass
	{

		[TestCaseSource(typeof(Utility.TestDataSources), Utility.TestDataSources.srcNumberTestData)]
		public void AddTwoNumbers_DataDriven(IDictionary<string, string> data)
		{
			int firstNumber, secondNumber, sum;

			firstNumber = Convert.ToInt16(data["First Number"]);
			secondNumber = Convert.ToInt16(data["Second Number"]);
			sum = Convert.ToInt16(data["Expected Result"]);

			Assert.AreEqual(sum, firstNumber + secondNumber, "The numbers just don't add up!!!");


		}


	}
}
