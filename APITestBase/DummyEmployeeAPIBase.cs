using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APITestBase.DataObjects;
using Newtonsoft.Json;
using RestSharp;

namespace APITestBase
{
	public class DummyEmployeeAPIBase
	{
		const string BaseUrl = "http://dummy.restapiexample.com/";

		readonly IRestClient _client;

		//string _accountSid;

		public DummyEmployeeAPIBase()
		{
			_client = new RestClient(BaseUrl);			
		}

		public T Execute<T>(RestRequest request) where T : new()
		{
			IRestResponse<T> response = _client.Execute<T>(request);

			var objectToReturn = JsonConvert.DeserializeObject<T>(response.Content);

			return objectToReturn;
		}

		public Employee GetEmployee(string employeeID)
		{
			var request = new RestRequest("http://dummy.restapiexample.com/api/v1/employee/{EmployeeID}");
			request.AddParameter("EmployeeID", employeeID, ParameterType.UrlSegment);

			return Execute<Employee>(request);
		}

	}
}
