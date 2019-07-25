using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APITestBase.DataObjects;
using NUnit.Framework;
using RestSharp;

namespace APITestBase
{
    [TestFixture]
    public class SampleTestClass : DummyEmployeeAPIBase
    {
		public SampleTestClass() : base()
		{

		}

        [Test]
        public void Test1()
        {
            var client = new RestClient("http://dummy.restapiexample.com/");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("http://dummy.restapiexample.com/api/v1/employee/34972", Method.GET, DataFormat.Json);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource            

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            return;

        }

        [Test]
        public void Test2()
        {
			//var client = new RestClient("http://dummy.restapiexample.com/");
			var request = new RestRequest("http://dummy.restapiexample.com/api/v1/employee/40216", Method.GET, DataFormat.Json);

			//IRestResponse response = client.Execute(request);
			//var content = response.Content; // raw content as string

			Employee emp = new Employee();

			var thing = Execute<Employee>(request);

			//emp = GetEmployee("34973");

			return;


		}


    }
}
