using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AutomationBase.Utility
{
    public class ExcelReadWrite
	{

		#region Properties

		public static string ExcelFilePath { get; set; }
        public static string SheetName { get; set; }
        public static Worksheet xlSheet { get; set; }
		public static Application xlApp { get; set; }
		public static Workbook xlBook { get; set; }
		public static Sheets xlSheets { get; set; }
		public static List<int> lstOpenExcelId = new List<int>();

		#endregion

        public static void SetExcelSheet()
        {
            //xlApp = new Microsoft.Office.Interop.Excel.Application();
            //xlBook = xlApp.Workbooks.Open(ExcelFilePath);
            xlSheets = xlBook.Worksheets;
            xlSheet = xlSheets.get_Item(SheetName);
        }

        private static int GetRowLength_ByHeaderLength()
        {
            int Length = 0;
            int StartColumn = 1;
            while (xlSheet.Cells[1, StartColumn].Value != null)
            {
                StartColumn++;
            }
            Length = StartColumn - 1;
            return Length;
        }
		private static int GetRowCount_ByFirstColumn()
		{
			int Length = 0;
			int StartRow = 1;
			while (xlSheet.Cells[StartRow, 1].Value != null)
			{
				StartRow++;
			}
			Length = StartRow - 1;
			return Length;
		}
        public static int FindRow(string ColumnHeader, string ValueToFind)
        {
            string column = "";
            string value = null;
            int counter = 1;
            int StopColumn = GetRowLength_ByHeaderLength();
			int StopRow = GetRowCount_ByFirstColumn();			
            for (int i = 1; i <= StopColumn; i++)
            {
                column = xlSheet.Cells[1, i].Value.ToString();
                if (column == ColumnHeader)
                {
					for (int j = 1; j <= StopRow; j++)
					{
						value = xlSheet.Cells[counter, i].Value;
						if (value == ValueToFind)
							return counter;
						else
							counter++;
						if (j == counter)
							return -1;
					}						
                }                
            }
            return counter;
        }
        public static Dictionary<string, string> RowAsDictionary_HeaderAsKeys(int DataRow)
        {
            Dictionary<string, string> dic_Data = new Dictionary<string, string>();
            int StopColumn = GetRowLength_ByHeaderLength();
            string Key = "";
            string Value = "";
            int HeaderRow = 1;
            for (int i = 1; i <= StopColumn; i++)
            {
                Key = xlSheet.Cells[HeaderRow, i].Value.ToString();
                if (xlSheet.Cells[DataRow, i].Value != null)
                    Value = xlSheet.Cells[DataRow, i].Value.ToString();
                else
                    Value = "";
                dic_Data.Add(Key, Value);
            }
            return dic_Data;
        }


		public static void getOpenExcelId()
		{
			lstOpenExcelId.Clear();
			foreach (Process proc in Process.GetProcesses())
			{
				if (proc.ProcessName.Equals("EXCEL"))
				{
					lstOpenExcelId.Add(proc.Id);
				}
			}
		}

		[DllImport("user32.dll")]
		private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
		public static bool TryKillProcessByMainWindowHwnd(int hWnd)
		{
			uint processID;
			GetWindowThreadProcessId((IntPtr)hWnd, out processID);
			if (processID == 0) return false;
			try
			{
				Process.GetProcessById((int)processID).Kill();
			}
			catch (ArgumentException)
			{
				return false;
			}
			catch (System.ComponentModel.Win32Exception)
			{
				return false;
			}
			catch (NotSupportedException)
			{
				return false;
			}
			catch (InvalidOperationException)
			{
				return false;
			}
			return true;
		}

        public static void closeExcel()
        {
			
			xlBook.Close(true, Type.Missing, Type.Missing);
			
			xlApp.Quit();
			xlApp = null;
			
			GC.Collect();
			GC.WaitForPendingFinalizers();
			
			System.Threading.Thread.Sleep(250);	
			
			
        }

    }
}
