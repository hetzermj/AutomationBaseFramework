using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationBase.Utility
{
	public class TestDataSources
	{
		public const string srcNumberTestData = "NumberTestData";

		private static IEnumerable<TestCaseData> NumberTestData()
		{
			IEnumerable<TestCaseData> testcases = GetData(@"C:\Automation_Data\" + "NumberTest.xlsx", "Number Test");
			return testcases;
		}

		private static IEnumerable<TestCaseData> GetData(string path, string sheetName)
		{
			var testcases = ExcelDataReader.New().FromFileSystem(path).AddSheet(sheetName).GetTestCases(delegate (string sheet, System.Data.DataRow row, int rowNum)
			{
				var testDataArgs = new Dictionary<string, string>();
				foreach (System.Data.DataColumn column in row.Table.Columns)
				{
					testDataArgs[column.ColumnName] = Convert.ToString(row[column]);
				}
				string testName = sheet + " - " + row.ItemArray[0];
				return new TestCaseData(testDataArgs).SetName(testName);
			});

			foreach (TestCaseData testCaseData in testcases)
			{
				yield return testCaseData;
			}
		}

	}
}
