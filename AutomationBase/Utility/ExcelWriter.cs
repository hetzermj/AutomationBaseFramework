using System;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BaseAutomationFramework.Tests;
//using AventStack.ExtentReports;

namespace AutomationBase.Utility
{
    class ExcelWriter
    {
        //public static Application xlApp { get; set; }
        //public static Workbooks xlBooks { get; set; }

        ////public static Workbook xlBook { get; set; }
        //public static object misValue = System.Reflection.Missing.Value;
        //public static Worksheet xlSheet { get; set; }

        //public static void setupExcelSheet()
        //{
        //    xlApp = new Microsoft.Office.Interop.Excel.Application();
        //    if(xlApp == null)
        //    {
                
        //    }
        //    //xlBooks = xlApp.Workbooks;
            
        //    //xlBook = xlBooks.Open(@"C:\Users\vasav.anandjiwala\Documents\Appium Test App\test.xlsx", misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
        //    //Worksheet myExcelWorksheet = (Worksheet)xlBook.ActiveSheet

        //    BaseTest.xlBook = xlApp.Workbooks.Add(misValue);
        //}

        //public static void InsertData(String PageObjectName)
        //{
        //    xlSheet = (Worksheet)BaseTest.xlBook.Worksheets.Add(After: BaseTest.xlBook.Sheets[BaseTest.xlBook.Sheets.Count]);
        //    xlSheet.Name = PageObjectName;
        //    xlSheet.Cells[2, 1] = "YAHOO";
        //    xlSheet.get_Range("A1", misValue).Formula = "Test";
        //}

        //public static void ExcelCleanup()
        //{
        //    BaseTest.xlBook.SaveAs(@"C:\Users\vasav.anandjiwala\Documents\Appium Test App\csharp-Excel.xls", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        //    BaseTest.xlBook.Close(true, Type.Missing, Type.Missing);
        //    xlApp.Quit();
        //}
    }
}
