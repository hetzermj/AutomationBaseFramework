using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AutomationBase.Utility
{
	public class EmailUtilities
	{
		const string FROM_ADDRESS = "";
		const string FROM_PASSWORD = "";

		public static void SendEmail(Email Email)
		{
			SmtpClient smtp = new SmtpClient();
			smtp.Host = "smtp.gmail.com";
			smtp.Port = 587;
			smtp.EnableSsl = true;
			smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtp.Credentials = new NetworkCredential(FROM_ADDRESS, FROM_PASSWORD);
			smtp.Timeout = 20000;

			smtp.Send(Email.Message);
		}



		public class Email
		{
			/// <summary>
			/// The subject line of the Email.
			/// </summary>
			public string Subject { get; set; }
			/// <summary>
			/// The body text of the Email.
			/// </summary>
			public string Body { get; set; }
			/// <summary>
			/// <para>Email Address(es) to send to.</para>
			/// <para>If more than one, separate by comma. No Whitespace.</para>
			/// </summary>
			public string ToAddresses { get; set; }

			public MailMessage Message { get; set; }


			public Email(string Subject, string Body, string ToAddresses)
			{
				Message = new MailMessage();
				Message.Subject = Subject;
				Message.Body = Body;
				Message.From = new MailAddress(FROM_ADDRESS);
				Message.To.Add(ToAddresses);
			}
		}


	}
}
