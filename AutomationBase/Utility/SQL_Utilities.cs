using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationBase.Utility
{
	public class SQL_Utilities
	{
		public const string CONNECTION_STRING = @"Server=ServerNameHere; Database=DatabaseNameHere; Trusted_Connection=Yes";

        public static String GetStringOrNull(Object obj)
        {
            if (obj == DBNull.Value)
            {
                return null;
            }

            return obj.ToString();
        }


		public static void DoSomethingWithTableData()
		{
			SqlConnection sqlConn = new SqlConnection(SQL_Utilities.CONNECTION_STRING);
			SqlCommand cmd = new SqlCommand();
			SqlDataReader data;

			string select = "WhatYouWantToSelect";
			string tableName = "";
			string columnName = "";
			string match = "";

			//	Getting the Seed Loan ID from the TestInformation Table
			cmd.CommandText =
				$"SELECT {select} " +
				$"FROM {tableName} " +
				$"WHERE {columnName}='{match}';"
				;

			cmd.CommandType = CommandType.Text;
			cmd.Connection = sqlConn;

			sqlConn.Open();

			data = cmd.ExecuteReader();
			data.Read();

			string dummyData = data["ColumnName"].ToString();

			sqlConn.Close();

		}

	}
}
