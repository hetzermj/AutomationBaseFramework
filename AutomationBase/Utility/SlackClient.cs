//using Newtonsoft.Json;
//using System;
//using System.Collections.Specialized;
//using System.Net;
//using System.Text;

//namespace AutomationBase.Utility
//{
//	//A simple C# class to post messages to a Slack channel
//	//Note: This class uses the Newtonsoft Json.NET serializer available via NuGet
//	public class SlackClient
//	{
//		// Example of sending message from test class
//		//string message = string.Format("@{0}\nTest Fixture Finished: {1}", MasterData.UserConfig.SlackUsername, className);
//		//new SlackClient().PostMessage(message);

//		private readonly Uri _uri;
//		private readonly Encoding _encoding = new UTF8Encoding();
//		public const string SLACKHOOK = @""; // hook url goes here

//		public SlackClient()
//		{
//			_uri = new Uri(SLACKHOOK);
//			Console.WriteLine(SLACKHOOK);
//		}

//		//Post a message using simple strings
//		public void PostMessage(string text)
//		{
//			Payload payload = new Payload()
//			{
//				Channel = "#",
//				Username = "",
//				Text = text,
//				LinkNames = 1,
//				Parse = "full",
//				AsUser = false
//			};

//			PostMessage(payload);
//		}

//		//Post a message using a Payload object
//		public void PostMessage(Payload payload)
//		{
//			string payloadJson = JsonConvert.SerializeObject(payload);

//			using (WebClient client = new WebClient())
//			{
//				NameValueCollection data = new NameValueCollection();
//				data["payload"] = payloadJson;

//				var response = client.UploadValues(_uri, "POST", data);

//				//The response text is usually "ok"
//				string responseText = _encoding.GetString(response);
//			}
//		}
//	}

//	//This class serializes into the Json payload required by Slack Incoming WebHooks
//	public class Payload
//	{
//		[JsonProperty("channel")]
//		public string Channel { get; set; }

//		[JsonProperty("username")]
//		public string Username { get; set; }

//		[JsonProperty("text")]
//		public string Text { get; set; }

//		[JsonProperty("link_names")]
//		public int LinkNames { get; set; }

//		[JsonProperty("parse")]
//		public string Parse { get; set; }

//		[JsonProperty("as_user")]
//		public bool AsUser { get; set; }
//	}
//}


