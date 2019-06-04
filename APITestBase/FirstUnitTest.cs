using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;

namespace APITestBase
{
    [TestFixture]
    public class FirstUnitTest
    {

        [Test]
        public void Test1()
        {
            var client = new RestClient("http://dummy.restapiexample.com/");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("http://dummy.restapiexample.com/api/v1/employees", Method.GET, DataFormat.Json);
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
            
        }


    }
}
