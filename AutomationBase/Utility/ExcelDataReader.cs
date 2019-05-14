using Excel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace AutomationBase.Utility
{
    /// <summary>
    /// Eases the creation of NUnit´s data driven tests from excel spreadsheets.
    /// Generates NUnit´s testcases from excel (xls or xlsx) files.
    /// Very useful when using with nunit´s parameterized tests capabilities and the
    /// <code>TestCaseSource</code> attribute.
    /// </summary>
    public class ExcelDataReader
    {
        protected ReadFileFrom ReadFileFrom { get; set; }

        protected string ExcelFile { get; set; }

        protected List<string> Sheets { get; set; }

        private IExcelDataReader GetExcelReader(string excelFile)
        {
            var stream = GetStream(excelFile);

            IExcelDataReader excelReader;
            var extensao = excelFile.Trim().Substring(excelFile.LastIndexOf("."));
            switch (extensao.ToUpper())
            {
                case ".XLS":
                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    break;
                case ".XLSX":
                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    break;
                default:
                    throw new ArgumentException("Unknown file extension.");
            }
            return excelReader;
        }

        private Stream GetStream(string excelFile)
        {
            Stream stream = null;

            switch (ReadFileFrom)
            {
                case ReadFileFrom.CurrentAssembly:
                    stream = GetExcelFileStreamFromEmbeddedResource(excelFile);
                    break;

                case ReadFileFrom.Assembly:
                    stream = GetEmbeddedFile(AssemblyName, excelFile);
                    break;

                case ReadFileFrom.Stream:
                    stream = ExcelFileStream;
                    break;

                case ReadFileFrom.FileSystem:
                    stream = GetExcelFileStreamFromFileSystem(excelFile);
                    break;
            }

            if (stream == null)
            {
                throw new FileNotFoundException(excelFile);
            }
            return stream;
        }

        protected virtual Stream GetExcelFileStreamFromEmbeddedResource(string excelFile)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(excelFile);
            return stream;
        }

        protected virtual Stream GetExcelFileStreamFromFileSystem(string excelFilePath)
        {
            var fileStream = new FileStream(excelFilePath, FileMode.Open, FileAccess.Read);
            return fileStream;
        }

        /// <summary>
        /// Returns a new instance of ExcelTestCaseDataReader
        /// </summary>
        /// <returns></returns>
        public static ExcelDataReader New()
        {
            return new ExcelDataReader();
        }

        /// <summary>
        /// Loads the excel file from the current assembly.
        /// Don´t forget to include it as an embedded resource on your project
        /// </summary>
        /// <param name="excelFile">excel file location within the current assembly</param>
        /// <returns></returns>
        public ExcelDataReader FromEmbeddedResource(string excelFile)
        {
            ExcelFile = excelFile;
            ReadFileFrom = ReadFileFrom.CurrentAssembly;
            return this;
        }

        /// <summary>
        /// Loads the embedded excel file from a given assembly.
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="excelFile"></param>
        /// <returns></returns>
        public ExcelDataReader FromEmbeddedResource(string assemblyName, string excelFile)
        {
            ExcelFile = excelFile;
            AssemblyName = assemblyName;
            ReadFileFrom = ReadFileFrom.Assembly;
            return this;
        }

        public ExcelDataReader From(Stream stream, string excelFile)
        {
            ExcelFile = excelFile;
            ExcelFileStream = stream;
            ReadFileFrom = ReadFileFrom.Stream;
            return this;
        }

        protected Stream ExcelFileStream { get; set; }

        protected string AssemblyName { get; set; }

        /// <summary>
        /// Loads the excel file from the file system
        /// </summary>
        /// <param name="excelFilePath">full absolute path to the excel file</param>
        /// <returns></returns>
        public ExcelDataReader FromFileSystem(string excelFilePath)
        {
            ExcelFile = excelFilePath;
            ReadFileFrom = ReadFileFrom.FileSystem;
            return this;
        }

        /// <summary>
        /// Add an existing sheet to the 
        /// </summary>
        /// <param name="sheetName">workbookk´s sheet name</param>
        /// <returns></returns>
        public ExcelDataReader AddSheet(string sheet)
        {
            if (Sheets == null)
            {
                Sheets = new List<string>();
            }
            Sheets.Add(sheet);

            return this;
        }

        /// <summary>
        /// Iterates over all included spreadsheets rows 
        /// and delegates de TestCaseData creation to the testCaseDataCreator.
        /// The resulting TestCaseData list should be used with in TestCaseSource´s attribute.
        /// </summary>
        /// <param name="testCaseDataCreator">delegate responsible for TestCasData creation</param>
        /// <returns>list of NUnit´s TestCaseData</returns>
        public List<TestCaseData> GetTestCases(Func<string, DataRow, int, TestCaseData> testCaseDataCreator)
        {
            var testDataList = new List<TestCaseData>();
            var excelReader = GetExcelReader(ExcelFile);
            excelReader.IsFirstRowAsColumnNames = true;
            var result = excelReader.AsDataSet();
            foreach (var sheet in Sheets)
            {
                var sheetTable = result.Tables[sheet];
                int i = 0;
                foreach (DataRow dr in sheetTable.Rows)
                {
                    testDataList.Add(testCaseDataCreator(sheet, dr, i));
                    i++;
                }
            }

            excelReader.Close();
            return testDataList;

        }

        #region Utility methods

        /// <summary>
        /// Extracts an embedded file out of a given assembly.
        /// </summary>
        /// <param name="assemblyName">The namespace of you assembly.</param>
        /// <param name="fileName">The name of the file to extract.</param>
        /// <returns>A stream containing the file data.</returns>
        public static Stream GetEmbeddedFile(string assemblyName, string fileName)
        {
            try
            {
                var a = Assembly.Load(assemblyName);
                if (a == null)
                    throw new ArgumentException("Could not locate assembly: " + assemblyName);

                var str = a.GetManifestResourceStream(assemblyName + "." + fileName);
                if (str == null)
                    throw new ArgumentException("Could not locate embedded resource '" + fileName + "' in assembly '" + assemblyName + "'");

                return str;
            }
            catch (Exception e)
            {
                throw new Exception(assemblyName + ": " + e.Message);
            }
        }

        public static Stream GetEmbeddedFile(Assembly assembly, string fileName)
        {
            var assemblyName = assembly.GetName().Name;
            return GetEmbeddedFile(assemblyName, fileName);
        }

        public static Stream GetEmbeddedFile(Type type, string fileName)
        {
            var assemblyName = type.Assembly.GetName().Name;
            return GetEmbeddedFile(assemblyName, fileName);
        }

        #endregion

    }

    public enum ReadFileFrom
    {
        CurrentAssembly,
        FileSystem,
        Assembly,
        Stream
    }
}
